// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Reporitories.Common;
using DotNetExtensions.Authorization.OAuth20.Server.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DotNetExtensions.Authorization.OAuth20.Server.InMemory.Repositories.Common;

public class InMemoryRepository<TEntity, TIdentifier> : IRepository<TEntity, TIdentifier>
    where TEntity : EntityBase<TIdentifier>, new()
{
    protected readonly OAuth20ServerDbContext _oAuth20ServerDbContext;

    public InMemoryRepository(OAuth20ServerDbContext oAuth20ServerDbContext)
    {
        _oAuth20ServerDbContext = oAuth20ServerDbContext;
    }

    public async Task<TIdentifier> AddAsync(TEntity entity)
    {
        await _oAuth20ServerDbContext.Set<TEntity>().AddAsync(entity);
        await _oAuth20ServerDbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task DeleteAsync(TIdentifier id)
    {
        var entity = await _oAuth20ServerDbContext.Set<TEntity>().FindAsync(id);
        if (entity != null)
        {
            _oAuth20ServerDbContext.Remove(entity);
            await _oAuth20ServerDbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(int limit = 0, int offset = 0)
    {
        var entities = await _oAuth20ServerDbContext.Set<TEntity>().Skip(offset).Take(limit).ToListAsync();

        return entities;
    }

    public async Task<TEntity?> GetByIdAsync(TIdentifier id)
    {
        var entity = await _oAuth20ServerDbContext.Set<TEntity>().FindAsync(id);

        return entity;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _oAuth20ServerDbContext.Set<TEntity>().Update(entity);

        await _oAuth20ServerDbContext.SaveChangesAsync();
    }
}
