// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataSources;

public interface IDataSource<TEntity>
{
    public Task<TIDentifier> AddAsync<TIDentifier>(TEntity entity);

    public Task UpdateAsync(TEntity entity);

    public Task DeleteAsync<TIDentifier>(TIDentifier id);

    public Task<TEntity?> GetAsync<TIDentifier>(TIDentifier id);

    public Task<IEnumerable<TEntity>> GetAllAsync(int limit = 0, int offset = 0);
}
