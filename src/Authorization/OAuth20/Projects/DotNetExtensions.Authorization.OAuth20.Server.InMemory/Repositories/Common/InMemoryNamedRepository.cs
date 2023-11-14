// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Reporitories.Common;
using DotNetExtensions.Authorization.OAuth20.Server.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DotNetExtensions.Authorization.OAuth20.Server.InMemory.Repositories.Common;

public class InMemoryNamedRepository<TEntity, TIdentifier> : InMemoryRepository<TEntity, TIdentifier>, INamedRepository<TEntity, TIdentifier>
    where TEntity : EntityBase<TIdentifier>, INamedEntity, new()
{
    public InMemoryNamedRepository(OAuth20ServerDbContext oAuth20ServerDbContext)
        : base(oAuth20ServerDbContext)
    {
    }

    public async Task<TEntity?> GetByNameAsync(string name)
    {
        var entity = await _oAuth20ServerDbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Name == name);

        return entity;
    }
}
