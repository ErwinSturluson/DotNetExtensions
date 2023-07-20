﻿// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Data.Generic;

public interface IRepository<TEntity>
    where TEntity : EntityBase
{
    public Task<int> AddAsync(TEntity entity);

    public Task<TEntity> GetByIdAsync(int id);

    public Task<TEntity> GetByExternalIdAsync(Guid externalId);

    public Task<TEntity> GetAsync(Func<TEntity, bool> predicate);

    public Task<IEnumerable<TEntity>> GetListAsync(Func<TEntity, bool> predicate);

    public Task UpdateAsync(TEntity entity);

    public Task DeleteByIdAsync(int id);
}
