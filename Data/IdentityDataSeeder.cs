using Microsoft.AspNetCore.Identity;

namespace MyWeb.Data;

public class IdentityDataSeeder(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
{
    public async Task Seed()
    {
        if (!roleManager.Roles.Any())
        {
            await roleManager.CreateAsync(new IdentityRole(UserRole.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(UserRole.Teacher.ToString()));
        }

        if (!userManager.Users.Any())
        {
            IdentityUser user1 = new IdentityUser { UserName = "root" };
            await userManager.CreateAsync(user1, "123456");
            await userManager.AddToRoleAsync(user1, UserRole.Admin.ToString());

            IdentityUser user2 = new IdentityUser { UserName = "mfzhenglinliu" };
            await userManager.CreateAsync(user2, "123456");
            await userManager.AddToRoleAsync(user2, UserRole.Teacher.ToString());
        }
    }
}
