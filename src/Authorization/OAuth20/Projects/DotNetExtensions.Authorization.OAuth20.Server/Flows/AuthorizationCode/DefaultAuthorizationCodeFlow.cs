// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.AuthorizationCode.Authorize;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.AuthorizationCode.Token;
using DotNetExtensions.Authorization.OAuth20.Server.Models;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using Microsoft.Extensions.Options;

namespace DotNetExtensions.Authorization.OAuth20.Server.Flows.AuthorizationCode;

/// <summary>
/// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-4.1"/>
/// </summary>
public class DefaultAuthorizationCodeFlow : IAuthorizationCodeFlow
{
    private readonly IOptions<OAuth20ServerOptions> _options;
    private readonly IErrorResultProvider _errorResultProvider;
    private readonly IEndUserService _endUserService;
    private readonly IClientService _clientService;
    private readonly IScopeService _scopeService;
    private readonly IAuthorizationCodeService _authorizationCodeService;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IFlowService _flowService;
    private readonly ILoginService _loginService;

    public DefaultAuthorizationCodeFlow(
        IOptions<OAuth20ServerOptions> options,
        IErrorResultProvider errorResultProvider,
        IEndUserService endUserService,
        IClientService clientService,
        IScopeService scopeService,
        IAuthorizationCodeService authorizationCodeService,
        IRefreshTokenService refreshTokenService,
        IFlowService flowService,
        ILoginService loginService)
    {
        _options = options;
        _errorResultProvider = errorResultProvider;
        _endUserService = endUserService;
        _clientService = clientService;
        _scopeService = scopeService;
        _authorizationCodeService = authorizationCodeService;
        _refreshTokenService = refreshTokenService;
        _flowService = flowService;
        _loginService = loginService;
    }

    public async Task<IResult> AuthorizeAsync(FlowArguments args)
    {
        if (!_endUserService.IsAuthenticated())
        {
            return await _loginService.RedirectToLoginAsync(args);
        }
        else
        {
            AuthorizeArguments authArgs = AuthorizeArguments.Create(args);

            if (authArgs.State is null && _options.Value.AuthorizationRequestStateRequired)
            {
                return _errorResultProvider.GetAuthorizeErrorResult(DefaultAuthorizeErrorType.InvalidRequest, state: null, "Missing request parameter: [state]");
            }

            IResult result = await AuthorizeAsync(authArgs);

            return result;
        }
    }

    public async Task<IResult> GetTokenAsync(FlowArguments args, Client client)
    {
        var tokenArgs = TokenArguments.Create(args);

        IResult result = await GetTokenAsync(tokenArgs, client);

        return result;
    }

    public async Task<IResult> AuthorizeAsync(AuthorizeArguments args)
    {
        var endUser = await _endUserService.GetCurrentEndUserAsync();
        if (endUser is null)
        {
            return _errorResultProvider.GetAuthorizeErrorResult(DefaultAuthorizeErrorType.UnauthorizedClient, args.State, "Current EndUser doesn't exist in the system.");
        }

        var client = await _clientService.GetClientAsync(args.ClientId);
        if (client is null)
        {
            return _errorResultProvider.GetAuthorizeErrorResult(DefaultAuthorizeErrorType.UnauthorizedClient, args.State, $"Client with [client_id] = [{args.ClientId}] doesn't exist in the system.");
        }

        var flow = await _flowService.GetFlowAsync<IAuthorizationCodeFlow>();
        if (flow is null)
        {
            return _errorResultProvider.GetAuthorizeErrorResult(DefaultAuthorizeErrorType.ServerError, args.State, "Cannot determine the flow.");
        }

        bool flowAvailable = await _clientService.IsFlowAvailableForClientAsync(client, flow);
        if (!flowAvailable)
        {
            return _errorResultProvider.GetAuthorizeErrorResult(DefaultAuthorizeErrorType.InvalidRequest, args.State, $"The selected flow is not available to the Client with [client_id] = [{args.ClientId}].");
        }

        string redirectUri = await _clientService.GetRedirectUriAsync(args.RedirectUri, flow, client, args.State);

        ScopeResult scopeResult = await _scopeService.GetScopeAsync(args.Scope, client, endUser, args.State);

        string code = await _authorizationCodeService.GetAuthorizationCodeAsync(args, endUser, client, redirectUri, scopeResult.IssuedScope, scopeResult.IssuedScopeDifferent);
        if (code is null)
        {
            return _errorResultProvider.GetAuthorizeErrorResult(DefaultAuthorizeErrorType.ServerError, args.State, "Cannot issue a code.");
        }

        AuthorizeResult result = AuthorizeResult.Create(redirectUri, code, args.State);

        return result;
    }

    public async Task<IResult> GetTokenAsync(TokenArguments args, Client client)
    {
        var flow = await _flowService.GetFlowAsync<IAuthorizationCodeFlow>();
        if (flow is null)
        {
            return _errorResultProvider.GetAuthorizeErrorResult(DefaultAuthorizeErrorType.ServerError, "Cannot determine the flow.");
        }

        AccessTokenResult accessToken = await _authorizationCodeService.ExchangeAuthorizationCodeAsync(args.Code, client, args.RedirectUri);
        RefreshTokenResult refreshToken = await _refreshTokenService.GetRefreshTokenAsync(accessToken);

        TokenResult result = TokenResult.Create(
            accessToken: accessToken.Value,
            tokenType: accessToken.Type,
            expiresInRequired: _options.Value.TokenResponseExpiresInRequired,
            expiresIn: accessToken.ExpiresIn,
            scope: accessToken.IssuedScopeDifferent ? accessToken.Scope : null,
            null, // TODO: figure additional parameters out
            refreshToken.Value);

        return result;
    }
}
