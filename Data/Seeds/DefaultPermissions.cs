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
                    new Permission() { Name = "ROLE.VIEW", Code = "ROLE.VIEW", Entity = "ROLE" },
                    new Permission() { Name = "ROLE.CREATE", Code = "ROLE.CREATE", Entity = "ROLE" }
                };

                await context.Permissions.AddRangeAsync(permissions);
                await context.SaveChangesAsync();
            }
        }
    }
}
