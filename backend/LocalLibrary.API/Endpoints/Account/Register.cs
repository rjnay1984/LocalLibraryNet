using System.Threading.Tasks;
using AutoMapper;
using LocalLibrary.Core.Interfaces;
using LocalLibrary.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace LocalLibrary.API.Endpoints.Account
{
    public class Register : BaseEndpoint
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenClaimsService _tokenClaimsService;
        private readonly IMapper _mapper;
        public Register(UserManager<ApplicationUser> userManager, IMapper mapper, ITokenClaimsService tokenClaimsService)
        {
            _mapper = mapper;
            _tokenClaimsService = tokenClaimsService;
            _userManager = userManager;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Registers a user",
            Description = "Registers a user",
            OperationId = "acct.register",
            Tags = new[] { "AccountEndpoints" })
        ]
        public async Task<ActionResult<UserDto>> RegisterUser(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Email)) return BadRequest("Username is taken.");

            ApplicationUser user = _mapper.Map<ApplicationUser>(registerDto);

            user.UserName = registerDto.Email.ToLower();
            user.Email = registerDto.Email.ToLower();

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            return new UserDto
            {
                Token = await _tokenClaimsService.GetTokenAsync(user.UserName)
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}