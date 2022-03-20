using PermissionBasedTemplate.Identity;

namespace PermissionBasedTemplate.Data.Seeds
{

    public static class DefaultPermissions
    {
        public static async Task SeedAsync(ApplicationIdentityDbContext context)
        {
            if (!context.Permissions.Any())
            {
                List<Permission> permissions = new()
                {
                    new Permission() { Name = "PERMISSION.ROLE.LIST", Code = "PERMISSION.ROLE.LIST", Entity = "ROLE" },
                    new Permission() { Name = "PERMISSION.ROLE.CREATE", Code = "PERMISSION.ROLE.CREATE", Entity = "ROLE" }
                };

                await context.Permissions.AddRangeAsync(permissions);
                await context.SaveChangesAsync();
            }
        }
    }
}
