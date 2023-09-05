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

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
    {
        return DbSet.Where(expression);
    }

    #endregion Syncronous
}