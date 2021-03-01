using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalLibrary.Infrastructure.Identity
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> ListAllUsersAsync();
        Task<ApplicationUser> GetUserByIdAsync(string id);
        void AddUserAsync(ApplicationUser user);
        void UpdateUserAsync(ApplicationUser user);
        void DeleteUserAsync(ApplicationUser user);
    }
}