﻿using Microsoft.AspNetCore.Identity;
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

            // Each Role can have many entries in the UserRole join table
            builder.HasMany(e => e.Users)
                .WithOne()
                .HasForeignKey(r => r.RoleId)
                .IsRequired();

            builder.HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(c => c.RoleId)
                .IsRequired();
        }
    }

    internal class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.ToTable("UserRole");
        }
    }

    internal class ApplicationRoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
        {
            builder.ToTable("RoleClaim");
        }
    }

    internal class ApplicationUserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder)
        {
            builder.ToTable("UserClaim");
        }
    }

    internal class ApplicationUserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder)
        {
            builder.ToTable("UserLogin");
        }
    }

    internal class ApplicationUserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder)
        {
            builder.ToTable("UserToken");
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
