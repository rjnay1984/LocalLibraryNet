using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LocalLibrary.Infrastructure.Identity
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public void AddUserAsync(ApplicationUser user)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteUserAsync(ApplicationUser user)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApplicationUser> GetUserByIdAsync(int id)
        {
            throw new System.NotImplementedException();
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