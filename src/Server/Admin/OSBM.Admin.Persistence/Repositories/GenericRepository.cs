using Microsoft.EntityFrameworkCore;

using OSBM.Admin.Application.Contracts.Repositories;
using OSBM.Admin.Domain.Common;
using OSBM.Admin.Persistence.DbContexts;

using System.Linq.Expressions;

namespace OSBM.Admin.Persistence.Repositories;

public abstract class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : BaseEntity
{
    protected readonly ApplicationDbContext DbContext;
    protected readonly DbSet<T> DbSet;

    protected GenericRepository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = dbContext.Set<T>();
    }

    #region Dispose

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                DbContext.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion Dispose

    #region Syncronous

    public void Add(T entity)
    {
        DbSet.Add(entity);
    }

    public void Delete(long id)
    {
        var foundEntity = Find(id);
        if (foundEntity != null)
        {
            foundEntity.IsDeleted = true;
            Update(foundEntity);
        }
    }

    public IEnumerable<T> GetAll()
    {
        return DbSet.ToList();
    }

    public void Update(T entity)
    {
        DbSet.Entry(entity).State = EntityState.Modified;
    }

    public int SaveChanges()
    {
        return DbContext.SaveChanges();
    }

    public T? Find(long id)
    {
        return DbSet.Find(id);
    }

    public IQueryable<T> FindAll()
    {
        return DbSet;
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>>? filter = null,
                                          Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                          params string[] includeProperties)
    {
        IQueryable<T> query = DbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeProperties.Length > 0)
        {
            query = includeProperties.Aggregate(query, (theQuery, theInclude) => theQuery.Include(theInclude));
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }
        return query;
    }

    #endregion Syncronous

    #region Asyncronous

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
    }

    public async Task<T?> FindAsync(long id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FindAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<T>> FindManyAsync(
        CancellationToken cancellationToken = default,
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        int? take = null,
        int? skip = null,
        params string[] includeProperties)
    {
        IQueryable<T> query = DbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeProperties.Length > 0)
        {
            query = includeProperties.Aggregate(query, (theQuery, theInclude) => theQuery.Include(theInclude));
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        if (skip.HasValue)
        {
            query = query.Skip(skip.Value);
        }

        if (take.HasValue)
        {
            query = query.Take(take.Value);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.SaveChangesAsync(cancellationToken);
    }

    #endregion Asyncronous
}