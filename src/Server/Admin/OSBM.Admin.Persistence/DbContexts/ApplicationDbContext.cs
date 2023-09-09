using Microsoft.EntityFrameworkCore;

using OSBM.Admin.Application.Contracts.DbContexts;
using OSBM.Admin.Domain.Aggregates.Products;
using OSBM.Admin.Domain.Common;
using OSBM.Admin.Persistence.Extensions;

namespace OSBM.Admin.Persistence.DbContexts;

public class ApplicationDbContext : DbContext, IApplicationDbContext, IDisposable
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts) : base(opts)
    {
    }

    #region Private methods

    private void UpdateUpsertDateTime()
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            var now = DateTime.UtcNow;
            entry.Entity.UpdatedAt = now;
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = now;
            }
        }
    }

    #endregion Private methods

    #region Override methods

    public override int SaveChanges()
    {
        UpdateUpsertDateTime();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateUpsertDateTime();
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.ApplySoftDeleteQueryFilter();
        //base.OnModelCreating(modelBuilder);
    }

    #endregion Override methods

    #region DbSets

    public DbSet<Product> Products { get; set; }

    #endregion DbSets
}