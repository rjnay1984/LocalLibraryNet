using AutoMapper;
using LocalLibrary.API.Endpoints.Account;
using LocalLibrary.Infrastructure.Identity;

namespace LocalLibrary.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto, ApplicationUser>();
        }
    }
}