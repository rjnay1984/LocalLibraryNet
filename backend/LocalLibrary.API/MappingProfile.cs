using AutoMapper;
using LocalLibrary.API.Endpoints.Account.DTOs;
using LocalLibrary.API.Endpoints.Users.DTOs;
using LocalLibrary.Infrastructure.Identity;

namespace LocalLibrary.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto, ApplicationUser>();
            CreateMap<ApplicationUser, DetailUserDto>();
            CreateMap<NewUserDto, ApplicationUser>();
        }
    }
}