using LocalLibrary.Core.Constants;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace LocalLibrary.Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole("Administrators"));
            await roleManager.CreateAsync(new IdentityRole("Members"));

            var defaultUser = new ApplicationUser { UserName = "demouser@microsoft.com", Email = "demouser@microsoft.com" };
            await userManager.CreateAsync(defaultUser, AuthorizationConstants.DEFAULT_PASSWORD);
            await userManager.AddToRoleAsync(defaultUser, "Members");

            string adminUserName = "admin@microsoft.com";
            var adminUser = new ApplicationUser { UserName = adminUserName, Email = adminUserName };
            await userManager.CreateAsync(adminUser, AuthorizationConstants.DEFAULT_PASSWORD);
            adminUser = await userManager.FindByNameAsync(adminUserName);
            await userManager.AddToRoleAsync(adminUser, "Administrators");
            await userManager.AddToRoleAsync(adminUser, "Members");
        }
    }
}
