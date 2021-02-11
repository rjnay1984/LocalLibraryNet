using AutoMapper;
using LocalLibrary.Core.Interfaces;
using LocalLibrary.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace LocalLibrary.API.Endpoints.Account
{
    public class Authenticate : BaseEndpoint
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenClaimsService _tokenClaimService;

        public Authenticate(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IMapper mapper, ITokenClaimsService tokenClaimsService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _tokenClaimService = tokenClaimsService;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Authenticates a user",
            Description = "Authenticates a user",
            OperationId = "acct.authenticate",
            Tags = new[] { "AccountEndpoints" })
        ]
        public async Task<ActionResult<UserDto>> AuthenticateUser(LoginDto loginDto)
        {
            ApplicationUser user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if (user == null) return Unauthorized("Invalid username.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized();

            return new UserDto
            {
                Token = await _tokenClaimService.GetTokenAsync(user.UserName)
            };
        }
    }
}
