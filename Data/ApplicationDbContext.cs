using Microsoft.EntityFrameworkCore;
using PermissionBasedTemplate.Entities;
using PermissionBasedTemplate.Entities.Commom;
using PermissionBasedTemplate.Interfaces;

namespace PermissionBasedTemplate.Data;

public class ApplicationDbContext : DbContext
{
    private readonly ICurrentUserService _currentUserService;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
                                ICurrentUserService currentUserService)
        : base(options)
    {
        _currentUserService = currentUserService;
    }

    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        ///builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationIdentityDbContext)
            .Assembly, x => x.Namespace == "PermissionBasedTemplate.Data.Configurations");
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUserService.UserId;
                    entry.Entity.CreateDate = DateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _currentUserService.UserId;
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                    break;
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }

}
