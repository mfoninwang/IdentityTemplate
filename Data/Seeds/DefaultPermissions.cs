using PermissionBasedTemplate.Identity;

namespace PermissionBasedTemplate.Data.Seeds
{

    public static class DefaultPermissions
    {
        public static async Task SeedAsync(ApplicationIdentityDbContext context)
        {
            var arrPermissions = new string[] { "ROLE.VIEW", "ROLE.CREATE" };
            List<Permission> permissions = new();

            foreach (var item in arrPermissions)
            {
                if (!context.Permissions.Any(p => p.Code == item))
                {
                    permissions.Add(new Permission() { Name = item, Code = item, Entity = "ROLE" });
                }
            }
            await context.Permissions.AddRangeAsync(permissions);
            await context.SaveChangesAsync();

        }
    }
}
