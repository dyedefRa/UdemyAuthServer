using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedLibrary.Configuration;
using System;
using System.Collections.Generic;
using UdemyAuthServer.Core.Configurations;
using UdemyAuthServer.Core.Models;
using UdemyAuthServer.Core.Repositories;
using UdemyAuthServer.Core.Services;
using UdemyAuthServer.Core.UnitOfWork;
using UdemyAuthServer.Data;
using UdemyAuthServer.Data.Repositories;
using UdemyAuthServer.Data.UnitOfWork;
using UdemyAuthServer.Service.Services;

namespace UdemyAuthServer.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"),
                    //Migrationi Bu assemblyde oluþtur.
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly("UdemyAuthServer.Data");
                    });
            });

            services.AddIdentity<UserApp, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<AppDbContext>() //EntityFramework kullanacagýz ve ilgili db de olacak
            .AddDefaultTokenProviders(); //Þifre sýfýrlamak için default token provider


            //-------------------------------
            //1)appsetting > TokenOption deki classý CustomTokenOption olarak tanýttýk
            services.Configure<CustomTokenOption>(Configuration.GetSection("TokenOption"));
            //2)Buradada nesnenin örneðini aldýk.
            var tokenOptions = Configuration.GetSection("TokenOption").Get<CustomTokenOption>();
            //-------------------------------

            services.Configure<List<Client>>(Configuration.GetSection("Clients"));
            services.AddControllers();

            services.AddAuthentication(options =>
            {
                //Scheme |  farklý login kýsmýn olabýlýr
                //kullanýcý olarak gir bayi olarak gir vs gibi.
                //Üyelik sistemi = Schema.                
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtOptions =>
                 {
                     jwtOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                     {
                         ValidIssuer = tokenOptions.Issuer,
                         ValidAudience = tokenOptions.Audience[0],
                         IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),
                         //Kontrol etsin mi
                         ValidateIssuerSigningKey = true, //Ýmzasýný doðrulamak zorunda
                         ValidateAudience = true,
                         ValidateIssuer = true,
                         ValidateLifetime = true,
                         ClockSkew = TimeSpan.Zero
                     };
                 });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(name: "v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V2");
            });
        }
    }
}
