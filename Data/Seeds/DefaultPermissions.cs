﻿using WebApplication1.Models;

namespace WebApplication1.Data.Seeds
{

    public static class DefaultPermissions
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        { 
            List<Permission> permissions = new()
            {
                new Permission() { Name = "PERMISSION.ROLE.LIST", Code = "PERMISSION.ROLE.LIST", Entity="ROLE"},
                new Permission() { Name = "PERMISSION.ROLE.CREATE", Code = "PERMISSION.ROLE.CREATE", Entity="ROLE"}
            };

            await context.Permissions.AddRangeAsync(permissions);
            await context.SaveChangesAsync();
        }
    }
}
