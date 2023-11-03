// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors.Exceptions.Common;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.ServerInformation;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.ServerInformation;

public class DefaultServerInformationService : IServerInformationService
{
    private readonly IServerInformationMetadata _serverInformationMetadata;

    public DefaultServerInformationService(IServerInformationMetadata serverInformationMetadata)
    {
        _serverInformationMetadata = serverInformationMetadata;
    }

    public Task<IDictionary<string, string>> GetScopeInformation()
    {
        var scopeInformation = _serverInformationMetadata.Scope;

        if (scopeInformation is null)
        {
            throw new ServerConfigurationErrorException(
                "Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-3.3" +
                "The authorization server SHOULD document its scope requirements and default value (if defined).");
        }

        return Task.FromResult<IDictionary<string, string>>(scopeInformation);
    }

    public Task<IDictionary<string, string>> GetAuthorizationCodeInformation()
    {
        var authorizationCodeInformation = _serverInformationMetadata.AuthorizationCode;

        if (authorizationCodeInformation is null)
        {
            throw new ServerConfigurationErrorException(
                "The client should avoid making assumptions about code " +
                "value sizes. The authorization server SHOULD document " +
                "the size of any value it issues.");
        }

        return Task.FromResult<IDictionary<string, string>>(authorizationCodeInformation);
    }
}
