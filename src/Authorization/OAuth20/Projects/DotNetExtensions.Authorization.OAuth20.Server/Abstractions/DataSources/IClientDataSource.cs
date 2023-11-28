// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataSources;

public interface IClientDataSource
{
    public Task<Client?> GetClientAsync(string clientId);

    public Task<Client> GetClientAsync(ClientSecret clientSecret);

    public Task<IEnumerable<Flow>> GetClientFlowsAsync(string clientId);

    public Task<IEnumerable<ClientRedirectionEndpoint>> GetClientRedirectionEndpointsAsync(string clientId);
}
