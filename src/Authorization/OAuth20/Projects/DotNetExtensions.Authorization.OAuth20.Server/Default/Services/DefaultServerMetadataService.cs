// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.Services;

public class DefaultServerMetadataService : IServerMetadataService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DefaultServerMetadataService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<string> GetTokenIssuerAsync()
    {
        string issuer = _httpContextAccessor.HttpContext!.Request.Host.Host;

        return Task.FromResult(issuer);
    }
}
