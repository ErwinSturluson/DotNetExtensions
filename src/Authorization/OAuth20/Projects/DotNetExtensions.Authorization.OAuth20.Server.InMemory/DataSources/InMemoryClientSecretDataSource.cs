// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataSources;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotNetExtensions.Authorization.OAuth20.Server.InMemory.DataSources;

public class InMemoryClientSecretDataSource : IClientSecretDataSource
{
    private readonly OAuth20ServerDbContext _oAuth20ServerDbContext;

    public InMemoryClientSecretDataSource(OAuth20ServerDbContext oAuth20ServerDbContext)
    {
        _oAuth20ServerDbContext = oAuth20ServerDbContext;
    }

    public async Task<ClientSecret?> GetClientSecretAsync(string type, string clientSecretContent)
    {
        var clientSecretType = await _oAuth20ServerDbContext.ClientSecretTypes.FirstOrDefaultAsync(x => x.Name == type);
        if (clientSecretType is null) return null;

        return await _oAuth20ServerDbContext.ClientSecrets
            .FirstOrDefaultAsync(x =>
                x.ClientSecretType.Id == clientSecretType.Id &&
                x.Content == clientSecretContent);
    }

    public async Task<ClientSecret?> GetEmptyClientSecretAsync(string type, Client client)
    {
        var clientSecretType = await _oAuth20ServerDbContext.ClientSecretTypes.FirstOrDefaultAsync(x => x.Name == type);
        if (clientSecretType is null) return null;

        return await _oAuth20ServerDbContext.ClientSecrets
            .FirstOrDefaultAsync(x =>
                x.ClientId == client.Id &&
                x.ClientSecretType.Id == clientSecretType.Id &&
                x.Content == string.Empty);
    }
}
