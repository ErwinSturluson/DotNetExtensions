// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Reporitories.Common;
using DotNetExtensions.Authorization.OAuth20.Server.Domain.Abstractions;

namespace DotNetExtensions.Authorization.OAuth20.Server.InMemory.Repositories.Common;

public class InMemoryInt32IdRepository<TEntity> : InMemoryRepository<TEntity, int>, IInt32IdRepository<TEntity>
    where TEntity : Int32IdEntityBase, new()
{
    public InMemoryInt32IdRepository(OAuth20ServerDbContext oAuth20ServerDbContext)
        : base(oAuth20ServerDbContext)
    {
    }
}
