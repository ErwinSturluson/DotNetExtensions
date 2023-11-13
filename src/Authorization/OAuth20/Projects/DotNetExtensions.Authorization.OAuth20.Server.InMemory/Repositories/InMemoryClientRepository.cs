// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Reporitories;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.InMemory.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace DotNetExtensions.Authorization.OAuth20.Server.InMemory.Repositories;

internal class InMemoryClientRepository : InMemoryInt32IdRepository<Client>, IClientRepository
{
    public InMemoryClientRepository(OAuth20ServerDbContext oAuth20ServerDbContext)
        : base(oAuth20ServerDbContext)
    {
    }

    public async Task<Client?> GetByClientIdAsync(string clientId)
    {
        var client = await _oAuth20ServerDbContext.Clients.FirstOrDefaultAsync(x => x.ClientId == clientId);

        return client;
    }
}
