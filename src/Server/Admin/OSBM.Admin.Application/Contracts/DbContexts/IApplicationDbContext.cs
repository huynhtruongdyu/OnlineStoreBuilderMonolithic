using Microsoft.EntityFrameworkCore;

using OSBM.Admin.Domain.Entities;

namespace OSBM.Admin.Application.Contracts.DbContexts;

public interface IApplicationDbContext
{
    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    DbSet<Product> Products { get; set; }
}