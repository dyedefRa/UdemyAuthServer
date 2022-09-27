-------------------------------------------------
DataAnnotation un alternatifi >
1) Data > Configuration da ;
      ProductConfiguration : IEntityTypeConfiguration<Product> class olusturduk
      IEntityTypeConfiguration interfacesinden miras al�p yap�yoruz ve ilgili k�s�tlamalar� bu interfaceden miras alm�� olan classlarda belirtiyoruz.
2) Bu ayarlar� tabiki db context e de belirtmemiz gerek�yor 
    Data > AppDbContext > OnModelCreating te;
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    ekliyoruz.Tum  IEntityTypeConfiguration ten m�ras alm�s classlar� otomat�k db ye tan�t�yor

-------------------------------------------------
AutoMapper kurulumu
    Serivice > DtoMapper : Profile
    Service > ObjectMapper

-------------------------------------------------
Controllerda entity null mu de�il mi vs kontrolu yap�lmamal� !
    Servis katman�nda bu data gelmeli.
    Service > Services > GenericService

-------------------------------------------------
204 Status Code => No Content durum kodu.
    UdemyAuthServer.Data.Repositories >GenericRepository

-------------------------------------------------
Appsettingten OptionPattern ile class a veri �ekmek
1)UdemyAuthServer > appsettings  > TokenOption
2)Class � olu�tural�m ..
    SharedLibrary > Configuration > CustomTokenOption
3)** Birlerini Startup ta tan�talm
    UdemyAuthServer > Startup  > ConfigureServices
    services.Configure<CustomTokenOption>(Configuration.GetSection("TokenOption"));

 Not : Bunu almak i�in constructordan   
 IOptions<CustomTokenOption> tokenOption �ekilde �a��r�p valuesine e�le�tircez

         private readonly CustomTokenOption _tokenOption;

         ctor (IOptions<CustomTokenOption> tokenOption)
         {
         _tokenOption=tokenOption.Value;
         }
        ( TokenService dan bak)
 -------------------------------------------------