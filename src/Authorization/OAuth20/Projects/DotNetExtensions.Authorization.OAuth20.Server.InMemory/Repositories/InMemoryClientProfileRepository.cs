// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Reporitories;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.InMemory.Repositories.Common;

namespace DotNetExtensions.Authorization.OAuth20.Server.InMemory.Repositories;

public class InMemoryClientProfileRepository : InMemoryNamedRepository<ClientProfile, Domain.Enums.ClientProfile>, IClientProfileRepository
{
    public InMemoryClientProfileRepository(OAuth20ServerDbContext oAuth20ServerDbContext)
        : base(oAuth20ServerDbContext)
    {
    }
}
