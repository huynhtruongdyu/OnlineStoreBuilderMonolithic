using Microsoft.EntityFrameworkCore;

using OSBM.Admin.Domain.Aggregates.Products;

namespace OSBM.Admin.Application.Contracts.DbContexts;

public interface IApplicationDbContext
{
    int SaveChanges();

    void Dispose();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    DbSet<Product> Products { get; set; }
}