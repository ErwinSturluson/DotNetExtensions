// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;

public interface IServerMetadataService
{
    public Task<string> GetTokenIssuerAsync();
}
