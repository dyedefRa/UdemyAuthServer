-------------------------------------------------
DataAnnotation un alternatifi >
1) Data > Configuration da ;
      ProductConfiguration : IEntityTypeConfiguration<Product> class olusturduk
      IEntityTypeConfiguration interfacesinden miras alýp yapýyoruz ve ilgili kýsýtlamalarý bu interfaceden miras almýþ olan classlarda belirtiyoruz.
2) Bu ayarlarý tabiki db context e de belirtmemiz gerekýyor 
    Data > AppDbContext > OnModelCreating te;
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    ekliyoruz.Tum  IEntityTypeConfiguration ten mýras almýs classlarý otomatýk db ye tanýtýyor

-------------------------------------------------
AutoMapper kurulumu
    Serivice > DtoMapper : Profile
    Service > ObjectMapper

-------------------------------------------------
Controllerda entity null mu deðil mi vs kontrolu yapýlmamalý !
    Servis katmanýnda bu data gelmeli.
    Service > Services > GenericService

-------------------------------------------------
204 Status Code => No Content durum kodu.
    UdemyAuthServer.Data.Repositories >GenericRepository

-------------------------------------------------
Appsettingten OptionPattern ile class a veri çekmek
1)UdemyAuthServer > appsettings  > TokenOption
2)Class ý oluþturalým ..
    SharedLibrary > Configuration > CustomTokenOption
3)** Birlerini Startup ta tanýtalm
    UdemyAuthServer > Startup  > ConfigureServices
    services.Configure<CustomTokenOption>(Configuration.GetSection("TokenOption"));

 Not : Bunu almak için constructordan   
 IOptions<CustomTokenOption> tokenOption þekilde çaðýrýp valuesine eþleþtircez

         private readonly CustomTokenOption _tokenOption;

         ctor (IOptions<CustomTokenOption> tokenOption)
         {
         _tokenOption=tokenOption.Value;
         }
        ( TokenService dan bak)
 -------------------------------------------------