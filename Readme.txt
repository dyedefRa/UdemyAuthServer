-------------------------------------------------
DataAnnotation un alternatifi >
1
Data > Configuration da ;
  ProductConfiguration : IEntityTypeConfiguration<Product> class olusturduk

  IEntityTypeConfiguration interfacesinden miras al�p yap�yoruz ve ilgili k�s�tlamalar� bu interfaceden miras alm�� olan classlarda belirtiyoruz.

2 Bu ayarlar� tabiki db context e de belirtmemiz gerek�yor 

Data > AppDbContext > OnModelCreating te;

builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
ekliyoruz.Tum  IEntityTypeConfiguration ten m�ras alm�s classlar� otomat�k db ye tan�t�yor
-------------------------------------------------
AutoMapper kurulumu

Serivice > DtoMapper : Profile
Service > ObjectMapper