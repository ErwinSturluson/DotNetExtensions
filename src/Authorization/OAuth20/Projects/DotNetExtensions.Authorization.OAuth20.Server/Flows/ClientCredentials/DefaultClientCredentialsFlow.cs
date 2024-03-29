﻿// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.ClientCredentials.Token;
using DotNetExtensions.Authorization.OAuth20.Server.Models;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using Microsoft.Extensions.Options;

namespace DotNetExtensions.Authorization.OAuth20.Server.Flows.ClientCredentials;

/// <summary>
/// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-4.4"/>
/// </summary>
public class DefaultClientCredentialsFlow : IClientCredentialsFlow
{
    private readonly IOptions<OAuth20ServerOptions> _options;
    private readonly IErrorResultProvider _errorResultProvider;
    private readonly IFlowService _flowService;
    private readonly IClientService _clientService;
    private readonly IScopeService _scopeService;
    private readonly IAccessTokenService _accessTokenService;
    private readonly IRefreshTokenService _refreshTokenService;

    public DefaultClientCredentialsFlow(
        IOptions<OAuth20ServerOptions> options,
        IErrorResultProvider errorResultProvider,
        IFlowService flowService,
        IClientService clientService,
        IScopeService scopeService,
        IAccessTokenService accessTokenService,
        IRefreshTokenService refreshTokenService)
    {
        _options = options;
        _errorResultProvider = errorResultProvider;
        _flowService = flowService;
        _clientService = clientService;
        _scopeService = scopeService;
        _accessTokenService = accessTokenService;
        _refreshTokenService = refreshTokenService;
    }

    public async Task<IResult> GetTokenAsync(FlowArguments args, Client client)
    {
        var tokenArgs = TokenArguments.Create(args);

        var result = await GetTokenAsync(tokenArgs, client);

        return result;
    }

    public async Task<IResult> GetTokenAsync(TokenArguments args, Client client)
    {
        var flow = await _flowService.GetFlowAsync<IClientCredentialsFlow>();
        if (flow is null)
        {
            // TODO: token server error or something
            return _errorResultProvider.GetTokenErrorResult(DefaultTokenErrorType.Undefined, null, "Cannot determine the flow.");
        }

        bool flowAvailable = await _clientService.IsFlowAvailableForClientAsync(client, flow);
        if (!flowAvailable)
        {
            return _errorResultProvider.GetTokenErrorResult(DefaultTokenErrorType.InvalidRequest, null, $"The selected flow is not available to the Client with [client_id] = [{client.ClientId}].");
        }

        ScopeResult scopeResult = await _scopeService.GetServerAllowedScopeAsync(args.Scope, client);

        AccessTokenResult accessToken = await _accessTokenService.GetAccessTokenAsync(
            scopeResult.IssuedScope,
            scopeResult.IssuedScopeDifferent,
            client,
            redirectUri: null!);

        RefreshTokenResult refreshToken = await _refreshTokenService.GetRefreshTokenAsync(accessToken);

        TokenResult result = TokenResult.Create(
            accessToken: accessToken.Value,
            tokenType: accessToken.Type,
            expiresInRequired: _options.Value.TokenResponseExpiresInRequired,
            expiresIn: accessToken.ExpiresIn,
            scope: accessToken.IssuedScopeDifferent ? accessToken.Scope : null,
            null, // TODO: figure additional parameters out
            refreshTokenAccepted: _options.Value.ClientCredentialsFlowRefreshTokenAccepted,
            refreshToken.Value);

        return result;
    }
}
