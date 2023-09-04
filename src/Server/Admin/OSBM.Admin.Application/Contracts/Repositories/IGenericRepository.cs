﻿using OSBM.Admin.Domain.Common;

namespace OSBM.Admin.Application.Contracts.Repositories;

public interface IGenericRepository<T> where T : BaseEntity, new()
{
    #region Syncronous

    void Add(T entity);

    void Update(T entity);

    void Delete(long id);

    T? Get(long id);

    IEnumerable<T> GetAll();

    int SaveChanges();

    #endregion Syncronous

    #region Asynchronous

    //Task AddAsync(T entity, CancellationToken cancellationToken = default);

    //Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

    //Task DeleteAsync(long id, CancellationToken cancellationToken = default);

    //Task<T> GetAsync(long id, CancellationToken cancellationToken = default);

    //Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

    //Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    #endregion Asynchronous
}