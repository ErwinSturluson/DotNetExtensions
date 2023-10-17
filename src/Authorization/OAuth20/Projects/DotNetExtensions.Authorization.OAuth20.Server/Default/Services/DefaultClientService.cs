// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataSources;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors.Exceptions;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors.Exceptions.Authorize;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors.Exceptions.Common;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.Implicit;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.Services;

public class DefaultClientService : IClientService
{
    private static readonly Regex _redirectionUriRegex = new(@"http(?s):\/\/.+\/.*", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private readonly IClientDataSource _clientDataSource;
    private readonly IFlowService _flowService;
    private readonly ITokenTypeDataSource _tokenTypeDataSource;
    private readonly IOptions<OAuth20ServerOptions> _options;

    public DefaultClientService(
        IClientDataSource clientDataSource,
        IFlowService flowService,
        ITokenTypeDataSource tokenTypeDataSource,
        IOptions<OAuth20ServerOptions> options)
    {
        _clientDataSource = clientDataSource;
        _flowService = flowService;
        _tokenTypeDataSource = tokenTypeDataSource;
        _options = options;
    }

    public async Task<Client?> GetClientAsync(string clientId)
    {
        return await _clientDataSource.GetClientAsync(clientId);
    }

    public async Task<Client> GetClientAsync(ClientSecret clientSecret)
    {
        return await _clientDataSource.GetClientAsync(clientSecret);
    }

    public async Task<TokenType> GetTokenType(Client client)
    {
        TokenType? clientTokenType = await _tokenTypeDataSource.GetTokenTypeAsync(client);

        if (clientTokenType is null && _options.Value.Tokens?.DefaultTokenType is not null)
        {
            clientTokenType = await _tokenTypeDataSource.GetTokenTypeAsync(_options.Value.Tokens.DefaultTokenType);

            if (clientTokenType is null) throw new ServerConfigurationErrorException(
                $"The token type which Identifier [{_options.Value.Tokens.DefaultTokenType}] " +
                $"specified in the server options as a default token type for the server " +
                $"doesn't exist in the data source.");
        }
        else
        {
            throw new ServerConfigurationErrorException(
                $"There isn't an any specified token type for Client with [client_id] = [{client.ClientId}] " +
                $"and default token type for the server as well.");
        }

        return clientTokenType;
    }

    /// <summary>
    /// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-3.1.2"/>
    /// </summary>
    public async Task<string> GetRedirectUriAsync(string? requestedRedirectUri, Flow currentFlow, Client client, string? state = null)
    {
        // Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-3.1.2.2"/>
        if (requestedRedirectUri is null && _options.Value.RequestRedirectionUriRequired)
        {
            throw GetConfiguredRedirectUriException(new Abstractions.Errors.Exceptions.Common.InvalidRequestException("Missing request parameter: [redirect_uri]", state));
        }

        if (client.RedirectionEndpoints is null || !client.RedirectionEndpoints.Any())
        {
            if (_options.Value.ClientRegistrationRedirectionEndpointsRequired)
            {
                throw GetConfiguredRedirectUriException(new ServerErrorException(
                    "There isn't any redirect URI registered. The server requires every Client to register a single redirect URI at least.",
                    state));
            }

            Flow? implicitFlow = await _flowService.GetFlowAsync(typeof(IImplicitFlow));
            if (implicitFlow is null)
            {
                throw GetConfiguredRedirectUriException(new ServerErrorException("Implicit flow isn't registered.", state));
            }

            if (client.ClientTypeId == Domain.Enums.ClientType.Public || client.ClientTypeId == Domain.Enums.ClientType.Confidential && currentFlow.Name == implicitFlow.Name)
            {
                throw GetConfiguredRedirectUriException(new ServerErrorException(
                    "Redirect URI isn't registered. Only confidential clients are able to " +
                    "perform an action with a requested redirect URI without a registered redirect Uri, excluded the Implicit flow.",
                    state));
            }

            if (requestedRedirectUri is null)
            {
                // Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-3.1.2.3"/>
                throw GetConfiguredRedirectUriException(new Abstractions.Errors.Exceptions.Common.InvalidRequestException("Missing request parameter: [redirect_uri]", state));
            }

            return requestedRedirectUri;
        }
        else
        {
            if (_options.Value.ClientRegistrationCompleteRedirectionEndpointRequired)
            {
                // Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-3.1.2.2"/>
                var incompleteRedirectionUriList = client.RedirectionEndpoints.Where(x => !IsCompleteRedirectionUri(x));
                if (incompleteRedirectionUriList.Any())
                {
                    throw GetConfiguredRedirectUriException(new ServerErrorException(
                        "Incomplete redirection URI's were registered but the server requires clients to provide the complete redirection URI. " +
                        $"The following registered redirection URI's are incomplete: " +
                        $"[{incompleteRedirectionUriList.Aggregate((first, second) => $"\"{first}\", \"{second}\"")}]",
                        state));
                }
            }

            if (client.RedirectionEndpoints.Length > 1 && !_options.Value.ClientMultipleRedirectionEndpointsAllowed)
            {
                // Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-3.1.2.2"/>
                throw GetConfiguredRedirectUriException(new ServerErrorException(
                    "Multiple redirect URI are registered but the server doesn't allow multiple redirect URI to register.",
                    state));
            }
            else if (client.RedirectionEndpoints.Length == 1 && !IsCompleteRedirectionUri(client.RedirectionEndpoints.First()))
            {
                // Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-3.1.2.3"/>
                throw GetConfiguredRedirectUriException(new ServerErrorException(
                    "A single URI was registered for this client but it's only a part of a URI when the server requires " +
                    "to register a full redirection URI if it's the only redirection URI is registered for this client.",
                    state));
            }

            if (requestedRedirectUri is not null)
            {
                // Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-3.1.2.3"/>
                string? redirectUri = client.RedirectionEndpoints.FirstOrDefault(x =>
                    IsCompleteRedirectionUri(x) ?
                        x == requestedRedirectUri :
                        requestedRedirectUri.StartsWith(x));

                if (redirectUri is null)
                {
                    throw GetConfiguredRedirectUriException(new ServerErrorException(
                       "A single matched requested redirect URI isn't found.",
                       state));
                }

                return redirectUri;
            }
            else
            {
                if (client.RedirectionEndpoints.Length > 1)
                {
                    // Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-3.1.2.3"/>
                    throw GetConfiguredRedirectUriException(new Abstractions.Errors.Exceptions.Common.InvalidRequestException(
                        "Missing request parameter: [redirect_uri]",
                        state));
                }

                return client.RedirectionEndpoints.First();
            }
        }
    }

    public async Task<bool> IsFlowAvailableForClientAsync(Client client, Flow flow)
    {
        var clientFlows = await _clientDataSource.GetClientFlowsAsync(client.ClientId);

        bool flowAvailable = clientFlows.Any(x => x.Name == flow.Name);

        return flowAvailable;
    }

    /// <summary>
    /// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-3.1.2.2"/>
    /// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-3.1.2.3"/>
    /// </summary>
    private static bool IsCompleteRedirectionUri(string redirectionUri) =>
        _redirectionUriRegex.IsMatch(redirectionUri);

    private OAuth20Exception GetConfiguredRedirectUriException(OAuth20Exception exception)
    {
        // Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-3.1.2.4"/>
        if (_options.Value.InformResourceOwnerOfRedirectionUriError)
        {
            return exception;
        }
        else
        {
            return new CommonErrorException("Cannot determine the redirect URI.", exception.State);
        }
    }
}
