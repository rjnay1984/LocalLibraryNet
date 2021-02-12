using AutoMapper;
using LocalLibrary.API.Endpoints.Account.DTOs;
using LocalLibrary.Core.Interfaces;
using LocalLibrary.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace LocalLibrary.API.Endpoints.Account
{
    public class Account : BaseEndpoint
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenClaimsService _tokenClaimsService;

        public Account(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IMapper mapper, ITokenClaimsService tokenClaimsService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _tokenClaimsService = tokenClaimsService;
        }

        [HttpPost("authenticate")]
        [SwaggerOperation(
            Summary = "Authenticates a user",
            Description = "Authenticates a user",
            OperationId = "acct.authenticate",
            Tags = new[] { "AccountEndpoints" })
        ]
        public async Task<ActionResult<UserDto>> Authenticate(LoginDto loginDto)
        {
            ApplicationUser user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if (user == null) return Unauthorized("Invalid username.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized();

            return new UserDto
            {
                Token = await _tokenClaimsService.GetTokenAsync(user.UserName)
            };
        }

        [HttpPost("register")]
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
