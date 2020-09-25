using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MobilePhones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilePhones
{
    public class AppSeeds
    {
        public static async Task CreateDataAsync(IServiceProvider service)
        {
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = service.GetRequiredService<UserManager<User>>();

            bool existAdminRole = await roleManager.RoleExistsAsync("admin");

            if (!existAdminRole)
            {
                var adminRole = new IdentityRole();
                adminRole.Name = "admin";
                await roleManager.CreateAsync(adminRole);
            }

            bool existMemberRole = await roleManager.RoleExistsAsync("member");

            if (!existMemberRole)
            {
                var memberRole = new IdentityRole();
                memberRole.Name = "member";
                await roleManager.CreateAsync(memberRole);
            }

            string adminEmail = "admin@admin.com";
            User existingAdmin = await userManager.FindByEmailAsync(adminEmail);

            if (existingAdmin == null)
            {
                User user = new User { Email = adminEmail, UserName = "AdminUser" };
                var result = await userManager.CreateAsync(user, "1qaz@WSX29");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "admin");
                }
            }
        }
    }
}
