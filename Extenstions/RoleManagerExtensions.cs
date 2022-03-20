using Microsoft.AspNetCore.Identity;
using PermissionBasedTemplate.Identity;

namespace PermissionBasedTemplate.Extenstions
{
    public static class RoleManagerExtensions
    {
        public static void AddPermissionsToRoleAsync(this RoleManager<ApplicationRole> roleManager, 
            string[] permissions)
        {
            throw new NotImplementedException();
        }
    }
}
