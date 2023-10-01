// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;

public interface IClientService
{
    public Task<Client?> GetClientAsync(string clientId);

    public Task<Client?> GetClientAsync(string clientId, string clientSecret);

    /// <summary>
    /// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-3.1.2"/>
    /// </summary>
    public Task<string> GetRedirectUriAsync(string? requestedRedirectUri, Flow currentFlow, Client client, string? state = null);

    public Task<bool> IsFlowAvailableForClientAsync(Client client, Flow flow);
}
