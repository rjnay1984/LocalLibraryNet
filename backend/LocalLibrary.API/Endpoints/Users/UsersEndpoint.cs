using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LocalLibrary.API.Endpoints.Users.DTOs;
using LocalLibrary.Infrastructure.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LocalLibrary.API.Endpoints.Users
{
    public class Users : BaseEndpoint
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public Users(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetAllUsers()
        {
            var users = await _userRepository.ListAllUsersAsync();

            var newUsers = _mapper.Map<UserDto[]>(users);

            return Ok(newUsers);
        }
    }
}