using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PermissionBasedTemplate.Data.Configurations;
using PermissionBasedTemplate.Identity;
using System.Reflection;

namespace PermissionBasedTemplate.Data
{
    public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options)
            : base(options)
        {
        }
        public DbSet<Permission> Permissions { get; set; }

        public DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");
            builder
                .ApplyConfigurationsFromAssembly(typeof(ApplicationIdentityDbContext)
                .Assembly, x => x.Namespace== "PermissionBasedTemplate.Data.Configurations.Identity");
        }
    }
}
