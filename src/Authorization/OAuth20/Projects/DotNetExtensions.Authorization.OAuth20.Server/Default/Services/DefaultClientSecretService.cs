// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataSources;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.Services;

public class DefaultClientSecretService : IClientSecretService
{
    private readonly IClientSecretDataSource _clientSecretDataSource;

    public DefaultClientSecretService(IClientSecretDataSource clientSecretDataSource)
    {
        _clientSecretDataSource = clientSecretDataSource;
    }

    public async Task<ClientSecret?> GetClientSecretAsync(string type, string clientSecretContent)
    {
        return await _clientSecretDataSource.GetClientSecretAsync(type, clientSecretContent);
    }

    public async Task<ClientSecret?> GetEmptyClientSecretAsync(string type, Client client)
    {
        return await _clientSecretDataSource.GetEmptyClientSecretAsync(type, client);
    }
}
