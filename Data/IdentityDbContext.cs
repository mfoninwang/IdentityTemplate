using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Data
{
    public class IdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");

            builder.Entity<Permission>(b =>
            {
                b.ToTable("Permission");
                // Primary key
                b.HasIndex(c => c.Code).IsUnique();

                b.HasMany(e => e.Roles)
                .WithOne(p => p.Permission)
                .HasForeignKey(ur => ur.PermissionId)
                .IsRequired();
            });

            builder.Entity<RolePermission>(b =>
            {
                b.ToTable("RolePermission");
                // Primary key
                b.HasKey(r => new { r.PermissionId, r.RoleId });

            });

            builder.Entity<IdentityRoleClaim<string>>(b =>
            {
                b.ToTable("RoleClaim");
            });

            builder.Entity<IdentityUserClaim<string>>(b =>
            {
                b.HasKey(x => new { x.Id });
                b.ToTable("UserClaim");
            });

            builder.Entity<UserRole>(b =>
            {
                // Primary key
                //b.HasKey(r => new { r.UserId, r.RoleId });
                b.ToTable("UserRole");
            });

            builder.Entity<ApplicationUser>(b =>
            {
                // Map to Table in the database
                b.ToTable("User");

                b.Ignore(x => x.FullName);

                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne()
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne()
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne()
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.Roles)
                    .WithOne()
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<ApplicationRole>(b =>
            {
                b.ToTable("Role");

                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.Users)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                b.HasMany(e => e.Permissions)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
            });

            builder.Entity<IdentityUserLogin<string>>(b =>
            {
                // Primary key
                b.HasKey(x => new { x.LoginProvider, x.ProviderKey });
                b.ToTable("UserLogIn");
            });

            builder.Entity<IdentityUserToken<string>>(b =>
            {
                // Primary key
                b.HasKey(x => new { x.LoginProvider, x.UserId, x.Name });
                b.ToTable("UserToken");
            });
        }
    }
}
