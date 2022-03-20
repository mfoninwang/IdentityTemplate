using Microsoft.AspNetCore.Identity;
using PermissionBasedTemplate.Enums;
using PermissionBasedTemplate.Identity;

namespace PermissionBasedTemplate.Data.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            var roles = new string[] { "SuperAdmin", "Admin", "Basic" };
            foreach (var role in roles)
            {
                if  (await roleManager.RoleExistsAsync(role)==false)
                {
                    await roleManager.CreateAsync(new ApplicationRole
                    {
                        Id = role,
                        Name = role,
                        NormalizedName = role
                    });
                }
            };
        }
    }
}
