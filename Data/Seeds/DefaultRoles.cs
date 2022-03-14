using Microsoft.AspNetCore.Identity;
using WebApplication1.Entities;
using WebApplication1.Enums;

namespace WebApplication1.Data.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            await roleManager.CreateAsync(new ApplicationRole
            {
                Id = Roles.SuperAdmin.ToString(),
                Name = Roles.SuperAdmin.ToString(),
                NormalizedName = Roles.SuperAdmin.ToString()
            });

            await roleManager.CreateAsync(new ApplicationRole
            {
                Id = Roles.Admin.ToString(),
                Name = Roles.Admin.ToString(),
                NormalizedName = Roles.Admin.ToString()
            });

            await roleManager.CreateAsync(new ApplicationRole
            {
                Id = Roles.Basic.ToString(),
                Name = Roles.Basic.ToString(),
                NormalizedName = Roles.Basic.ToString()
            });
        }
    }
}
