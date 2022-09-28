using AutoMapper;
using UdemyAuthServer.Core.Dtos;
using UdemyAuthServer.Core.Models;

namespace UdemyAuthServer.Service
{
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<UserApp, UserAppDto>().ReverseMap();
        }
    }
}
