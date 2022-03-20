using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using PermissionBasedTemplate.Entities;
using PermissionBasedTemplate.Enums;
using PermissionBasedTemplate.Identity;

namespace PermissionBasedTemplate.Data.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedBasicUserAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            var defaultUser = new ApplicationUser
            {
                FirstName = "Basic",
                LastName = "User",
                UserName = "basicuser@gmail.com",
                Email = "basicuser@gmail.com",
                EmailConfirmed = true
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Iniobong123$");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                }
            }
        }

        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            var defaultUser = new ApplicationUser
            {
                FirstName = "Super",
                LastName = "Admin",
                UserName = "superadmin@gmail.com",
                Email = "superadmin@gmail.com",
                EmailConfirmed = true
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Iniobong123$!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                }
                //await roleManager.SeedClaimsForSuperAdmin();
            }
        }

        private async static Task SeedClaimsForSuperAdmin(this RoleManager<ApplicationRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("SuperAdmin");
            await roleManager.AddPermissionClaim(adminRole, "Role");
        }

        public static async Task AddPermissionClaim(this RoleManager<ApplicationRole> roleManager, ApplicationRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = new string[] { "ROLE.VIEW", "ROLE.CREATE" }; //Permissions.GeneratePermissionsForModule(module);

            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                }
            }
        }
    }

}
