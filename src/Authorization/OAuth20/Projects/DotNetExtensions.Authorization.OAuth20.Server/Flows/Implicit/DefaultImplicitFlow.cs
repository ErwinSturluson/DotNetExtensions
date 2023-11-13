// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.Implicit.Mixed;
using DotNetExtensions.Authorization.OAuth20.Server.Models;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using Microsoft.Extensions.Options;

namespace DotNetExtensions.Authorization.OAuth20.Server.Flows.Implicit;

/// <summary>
/// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-4.2"/>
/// </summary>
public class DefaultImplicitFlow : IImplicitFlow
{
    private readonly IOptions<OAuth20ServerOptions> _options;
    private readonly IErrorResultProvider _errorResultProvider;
    private readonly IEndUserService _endUserService;
    private readonly ILoginService _loginService;
    private readonly IClientService _clientService;
    private readonly IFlowService _flowService;
    private readonly IScopeService _scopeService;
    private readonly IAccessTokenService _accessTokenService;

    public DefaultImplicitFlow(
        IOptions<OAuth20ServerOptions> options,
        IErrorResultProvider errorResultProvider,
        IEndUserService endUserService,
        ILoginService loginService,
        IClientService clientService,
        IFlowService flowService,
        IScopeService scopeService,
        IAccessTokenService accessTokenService)
    {
        _options = options;
        _errorResultProvider = errorResultProvider;
        _endUserService = endUserService;
        _loginService = loginService;
        _clientService = clientService;
        _flowService = flowService;
        _scopeService = scopeService;
        _accessTokenService = accessTokenService;
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

        var flow = await _flowService.GetFlowAsync<IImplicitFlow>();
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

        ScopeResult scopeResult = await _scopeService.GetScopeAsync(args.Scope, endUser, client, args.State);

        AccessTokenResult accessToken = await _accessTokenService.GetAccessTokenAsync(
            scopeResult.IssuedScope,
            scopeResult.IssuedScopeDifferent,
            client,
            redirectUri,
            endUser);

        TokenResult result = TokenResult.Create(
            redirectUri: redirectUri,
            accessToken: accessToken.Value,
            tokenType: accessToken.Type,
            expiresInRequired: _options.Value.TokenResponseExpiresInRequired,
            expiresIn: accessToken.ExpiresIn,
            scope: accessToken.IssuedScopeDifferent ? accessToken.Scope : null,
            null, // TODO: figure additional parameters out
            args.State);

        return result;
    }
}
