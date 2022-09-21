-------------------------------------------------
DataAnnotation un alternatifi >
1
Data > Configuration da ;
  ProductConfiguration : IEntityTypeConfiguration<Product> class olusturduk

  IEntityTypeConfiguration interfacesinden miras alýp yapýyoruz ve ilgili kýsýtlamalarý bu interfaceden miras almýþ olan classlarda belirtiyoruz.

2 Bu ayarlarý tabiki db context e de belirtmemiz gerekýyor 

Data > AppDbContext > OnModelCreating te;

builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
ekliyoruz.Tum  IEntityTypeConfiguration ten mýras almýs classlarý otomatýk db ye tanýtýyor
-------------------------------------------------
AutoMapper kurulumu

Serivice > DtoMapper : Profile
Service > ObjectMapper