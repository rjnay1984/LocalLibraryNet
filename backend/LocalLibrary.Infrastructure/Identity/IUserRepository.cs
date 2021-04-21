using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalLibrary.Infrastructure.Identity
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> ListAllUsersAsync();
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<IdentityResult> AddUserAsync(ApplicationUser user, string[] roles);
        Task<IdentityResult> AddUserToRolesAsync(ApplicationUser user, string[] roles);
        void UpdateUserAsync(ApplicationUser user);
        void DeleteUserAsync(ApplicationUser user);
    }
}