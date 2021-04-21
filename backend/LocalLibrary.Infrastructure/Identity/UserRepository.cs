using LocalLibrary.Core.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalLibrary.Infrastructure.Identity
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddUserAsync(ApplicationUser user, string[] roles)
        {
            var result = await _userManager.CreateAsync(user, AuthorizationConstants.DEFAULT_PASSWORD);

            return result;
        }

        public async Task<IdentityResult> AddUserToRolesAsync(ApplicationUser user, string[] roles)
        {
            var result = await _userManager.AddToRolesAsync(user, roles ?? new[] { "Members" });

            return result;
        }

        public void DeleteUserAsync(ApplicationUser user)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return user;
        }

        public async Task<IEnumerable<ApplicationUser>> ListAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public void UpdateUserAsync(ApplicationUser user)
        {
            throw new System.NotImplementedException();
        }
    }
}