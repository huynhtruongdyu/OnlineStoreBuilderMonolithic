using OSBM.Admin.Application.Contracts.Repositories;
using OSBM.Admin.Domain.Aggregates.Products;
using OSBM.Admin.Persistence.DbContexts;

namespace OSBM.Admin.Persistence.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}