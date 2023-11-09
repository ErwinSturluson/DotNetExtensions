// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Reporitories;

public interface IRepository<TEntity>
    where TEntity : class, new()
{
    public Task<int> AddAsync(TEntity entity);

    public Task UpdateAsync(TEntity entity);

    public Task DeleteAsync(int id);

    public Task<TEntity?> GetByIdAsync(int id);

    public Task<TEntity?> GetByNameAsync(string name);

    public Task<IEnumerable<TEntity>> GetAllAsync(int limit = 0, int offset = 0);
}
