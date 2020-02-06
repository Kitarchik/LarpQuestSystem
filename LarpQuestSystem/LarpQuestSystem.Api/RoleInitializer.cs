using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace LarpQuestSystem.Api
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "kitarchik@gmail.com";
            string password = "Morridineba755!";
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (await roleManager.FindByNameAsync("SuperUser") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("SuperUser"));
            }
            if (await roleManager.FindByNameAsync("User") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                IdentityUser superUser = new IdentityUser { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(superUser, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(superUser, "Admin");
                    await userManager.AddToRoleAsync(superUser, "SuperUser");
                }
            }
        }
    }
}
