using OSBM.Admin.Domain.Common;

using System.Linq.Expressions;

namespace OSBM.Admin.Application.Contracts.Repositories;

public interface IGenericRepository<T> where T : BaseEntity
{
    #region Syncronous

    void Add(T entity);

    void Update(T entity);

    void Delete(long id);

    T? Find(long id);

    IQueryable<T> FindAll();

    IQueryable<T> FindByCondition(Expression<Func<T, bool>>? filter = null,
                                  Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                  params string[] includeProperties);

    int SaveChanges();

    #endregion Syncronous

    #region Asynchronous

    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    Task<T?> FindAsync(long id, CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> FindManyAsync(CancellationToken cancellationToken = default,
                                      Expression<Func<T, bool>>? filter = null,
                                      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                      int? take = null,
                                      int? skip = null,
                                      params string[] includeProperties);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    #endregion Asynchronous
}