using WebApplication1.Models;

namespace WebApplication1.Data.Seeds
{

    public static class DefaultPermissions
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        { 
            List<Permission> permissions = new()
            {
                new Permission() { Name = "ROLE_VIEW", Code ="ROLE_VIEW",Entity="ROLE"},
                new Permission() { Name = "ROLE_CREATE", Code ="ROLE_CREATE",Entity="ROLE"}
            };

            await context.Permissions.AddRangeAsync(permissions);
            await context.SaveChangesAsync();
        }
    }
}
