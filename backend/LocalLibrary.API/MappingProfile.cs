using AutoMapper;
using LocalLibrary.API.Endpoints.Account.DTOs;
using LocalLibrary.Infrastructure.Identity;

namespace LocalLibrary.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto, ApplicationUser>();
            CreateMap<ApplicationUser, LocalLibrary.API.Endpoints.Users.DTOs.UserDto>();
        }
    }
}