using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        base.OnModelCreating(builder);

        builder.Entity<IdentityRoleClaim<int>>(b =>
        {
            b.ToTable("RoleClaim");
        });

        builder.Entity<ApplicationRole>(b =>
        {
            b.ToTable("Role");
        });

        builder.Entity<IdentityUserClaim<int>>(b =>
        {
            b.ToTable("UserClaim");
        });

        builder.Entity<IdentityUserRole<string>>(b =>
        {
            // Primary key
            //b.HasKey(r => new { r.UserId, r.RoleId });
            b.ToTable("UserRole");
        });

        builder.Entity<ApplicationUser>(b =>
        {
            b.ToTable("User");
        });


        builder.Entity<ApplicationUser>(b =>
        {
            // Map to Table in the database
            b.ToTable("User");

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
            b.HasMany(e => e.UserRoles)
                .WithOne()
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });


        builder.Entity<ApplicationRole>(b =>
        {
            // Each Role can have many entries in the UserRole join table
            b.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        });

    }

}
