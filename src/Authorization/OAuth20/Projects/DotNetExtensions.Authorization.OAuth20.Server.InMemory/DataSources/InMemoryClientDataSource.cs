// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataSources;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotNetExtensions.Authorization.OAuth20.Server.InMemory.DataSources;

public class InMemoryClientDataSource : IClientDataSource
{
    private readonly OAuth20ServerDbContext _oAuth20ServerDbContext;

    public InMemoryClientDataSource(OAuth20ServerDbContext oAuth20ServerDbContext)
    {
        _oAuth20ServerDbContext = oAuth20ServerDbContext;
    }

    public async Task<Client?> GetClientAsync(string clientId)
    {
        return await _oAuth20ServerDbContext.Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ClientId == clientId);
    }

    public async Task<Client> GetClientAsync(ClientSecret clientSecret)
    {
        return await _oAuth20ServerDbContext.ClientSecrets
            .Where(x => x.Id == clientSecret.Id)
            .Include(x => x.Client)
            .Select(x => x.Client)
            .AsNoTracking()
            .FirstAsync();
    }

    public async Task<IEnumerable<Flow>> GetClientFlowsAsync(string clientId)
    {
        var client = await _oAuth20ServerDbContext.Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ClientId == clientId);
        if (client is null) return Enumerable.Empty<Flow>();

        return await _oAuth20ServerDbContext.ClientFlows
            .Where(x => x.ClientId == client.Id)
            .Include(x => x.Flow)
            .Select(x => x.Flow)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<ClientRedirectionEndpoint>> GetClientRedirectionEndpointsAsync(string clientId)
    {
        var client = await _oAuth20ServerDbContext.Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ClientId == clientId);
        if (client is null) return Enumerable.Empty<ClientRedirectionEndpoint>();

        return await _oAuth20ServerDbContext.ClientRedirectionEndpoints
            .Where(x => x.ClientId == client.Id)
            .AsNoTracking()
            .ToListAsync();
    }
}
