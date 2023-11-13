// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Reporitories;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.InMemory.Repositories.Common;

namespace DotNetExtensions.Authorization.OAuth20.Server.InMemory.Repositories;

internal class InMemoryClientTypeRepository : InMemoryNamedRepository<ClientType, Domain.Enums.ClientType>, IClientTypeRepository
{
    public InMemoryClientTypeRepository(OAuth20ServerDbContext oAuth20ServerDbContext)
        : base(oAuth20ServerDbContext)
    {
    }
}
