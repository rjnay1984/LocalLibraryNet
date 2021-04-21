using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LocalLibrary.API.Endpoints.Users.DTOs;
using LocalLibrary.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocalLibrary.API.Endpoints.Users
{
    [Authorize(Roles = "Administrators")]
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
        public async Task<ActionResult<IEnumerable<DetailUserDto>>> GetAllUsers()
        {
            var users = await _userRepository.ListAllUsersAsync();

            return Ok(_mapper.Map<DetailUserDto[]>(users));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetailUserDto>> GetUserById(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            var response = new DetailUserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName
            };

            return response;
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(NewUserDto user)
        {
            var newUser = _mapper.Map<ApplicationUser>(user);
            newUser.Email = user.Email.ToLower();
            newUser.UserName = user.Email.ToLower();

            var result = await _userRepository.AddUserAsync(newUser, user.Roles);

            if (!result.Succeeded) return BadRequest(result.Errors);

            await _userRepository.AddUserToRolesAsync(newUser, user.Roles);

            return Ok("User created: " + user.Email);
        }
    }
}