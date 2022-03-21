using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PermissionBasedTemplate.Identity;

namespace PermissionBasedTemplate.Data.Configurations.Identity
{
    internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("User");

            builder.Property(t => t.FirstName).HasMaxLength(50);
            builder.Property(t => t.LastName).HasMaxLength(50);
            builder.HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(uc => uc.UserId)
                .IsRequired();

            // Each User can have many UserLogins
            builder.HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(ul => ul.UserId)
                .IsRequired();

            // Each User can have many UserTokens
            builder.HasMany(e => e.Tokens)
                .WithOne()
                .HasForeignKey(ut => ut.UserId)
                .IsRequired();

            // Each User can have many entries in the UserRole join table
            builder.HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(r => r.UserId)
                .IsRequired();
        }
    }

    internal class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            // Primary key
            builder.HasKey(r => r.Id);

            builder.ToTable("Role");

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.Name).HasMaxLength(50);
            builder.Property(u => u.Code).HasMaxLength(50);

            builder.HasIndex(x=>x.Code).IsUnique(); // unique index on the code column

            // Each Role can have many entries in the UserRole
            builder.HasMany(e => e.Users)
                .WithOne()
                .HasForeignKey(r => r.RoleId)
                .IsRequired();

            // Each role can have multiple claims
            builder.HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(c => c.RoleId)
                .IsRequired();
        }
    }

    internal class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
        {
            builder.ToTable("UserRole");

            builder.HasKey("UserId", "RoleId");
        }
    }

    internal class ApplicationRoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityRoleClaim<Guid>> builder)
        {
            builder.ToTable("RoleClaim");
        }
    }

    internal class ApplicationUserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserClaim<Guid>> builder)
        {
            builder.ToTable("UserClaim");
        }
    }

    internal class ApplicationUserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserLogin<Guid>> builder)
        {
            builder.HasKey("LoginProvider", "ProviderKey", "UserId");
            builder.ToTable("UserLogin");
        }
    }

    internal class ApplicationUserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserToken<Guid>> builder)
        {
            builder.ToTable("UserToken");
            builder.HasKey("UserId", "LoginProvider", "Name");
        }
    }

    internal class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permission");

            builder.HasIndex(c => c.Code).IsUnique();

            builder.HasMany(e => e.Roles)
            .WithOne(p => p.Permission)
            .HasForeignKey(ur => ur.PermissionId)
            .IsRequired();
        }
    }

    internal class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("RolePermission");
            builder.HasKey(new string[] { "RoleId", "PermissionId" });
        }
    }

}
