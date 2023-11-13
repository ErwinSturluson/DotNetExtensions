// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Reporitories;
using DotNetExtensions.Authorization.OAuth20.Server.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DotNetExtensions.Authorization.OAuth20.Server.InMemory.Repositories.Common;

public class InMemoryInt32IdNamedRepository<TEntity> : InMemoryInt32IdRepository<TEntity>, IInt32IdNamedRepository<TEntity>
    where TEntity : Int32IdEntityBase, INamedEntity, new()
{
    public InMemoryInt32IdNamedRepository(OAuth20ServerDbContext oAuth20ServerDbContext)
        : base(oAuth20ServerDbContext)
    {
    }

    public async Task<TEntity?> GetByNameAsync(string name)
    {
        var entity = await _oAuth20ServerDbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Name == name);

        return entity;
    }
}
