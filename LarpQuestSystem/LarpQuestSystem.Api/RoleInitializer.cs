using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using LarpQuestSystem.Data.Model.Security;

namespace LarpQuestSystem.Api
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "kitarchik@gmail.com";
            string password = "Morridineba755!";
            if (await roleManager.FindByNameAsync(Roles.Admin) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }
            if (await roleManager.FindByNameAsync(Roles.SuperUser) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.SuperUser));
            }
            if (await roleManager.FindByNameAsync(Roles.User) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.User));
            }
            if (await roleManager.FindByNameAsync(Roles.ScriptWriter) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.ScriptWriter));
            }
            if (await roleManager.FindByNameAsync(Roles.ScriptManager) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.ScriptManager));
            }
            if (await roleManager.FindByNameAsync(Roles.MaterialsManager) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.MaterialsManager));
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
