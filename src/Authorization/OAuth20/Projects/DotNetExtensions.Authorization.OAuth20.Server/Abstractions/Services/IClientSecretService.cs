// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;

public interface IClientSecretService
{
    public Task<ClientSecret?> GetClientSecretAsync(string type, string clientSecretContent);

    public Task<ClientSecret?> GetEmptyClientSecretAsync(string type, Client client);
}
