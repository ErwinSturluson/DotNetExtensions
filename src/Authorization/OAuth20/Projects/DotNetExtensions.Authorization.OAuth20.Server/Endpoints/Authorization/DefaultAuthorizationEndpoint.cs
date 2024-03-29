﻿// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Builders.Generic;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Flows;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Flows;
using DotNetExtensions.Authorization.OAuth20.Server.Models;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using Microsoft.Extensions.Options;

namespace DotNetExtensions.Authorization.OAuth20.Server.Endpoints.Authorization;

/// <summary>
/// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-3.1"/>
/// </summary>
public class DefaultAuthorizationEndpoint : IAuthorizationEndpoint
{
    private readonly IFlowRouter _flowRouter;
    private readonly IArgumentsBuilder<FlowArguments> _flowArgsBuilder;
    private readonly IRequestValidator<IAuthorizationEndpoint> _requestValidator;
    private readonly IErrorResultProvider _errorResultProvider;
    private readonly IClientService _clientService;
    private readonly ILoginService _loginService;
    private readonly IEndUserService _endUserService;
    private readonly IScopeService _scopeService;
    private readonly IPermissionsService _permissionsService;
    private readonly IOptions<OAuth20ServerOptions> _options;

    public DefaultAuthorizationEndpoint(
        IFlowRouter flowRouter,
        IArgumentsBuilder<FlowArguments> flowArgsBuilder,
        IRequestValidator<IAuthorizationEndpoint> requestValidator,
        IErrorResultProvider errorResultProvider,
        IClientService clientService,
        ILoginService loginService,
        IEndUserService endUserService,
        IScopeService scopeService,
        IPermissionsService permissionsService,
        IOptions<OAuth20ServerOptions> options)
    {
        _flowRouter = flowRouter;
        _flowArgsBuilder = flowArgsBuilder;
        _requestValidator = requestValidator;
        _errorResultProvider = errorResultProvider;
        _clientService = clientService;
        _loginService = loginService;
        _endUserService = endUserService;
        _scopeService = scopeService;
        _permissionsService = permissionsService;
        _options = options;
    }

    public async Task<IResult> InvokeAsync(HttpContext httpContext)
    {
        FlowArguments flowArgs = await _flowArgsBuilder.BuildArgumentsAsync(httpContext);
        var validationResult = _requestValidator.TryValidate(httpContext);

        if (!flowArgs.Values.TryGetValue("state", out string? state) && _options.Value.AuthorizationRequestStateRequired)
        {
            return _errorResultProvider.GetAuthorizeErrorResult(
                DefaultAuthorizeErrorType.InvalidRequest,
                state: null,
                "Missing request parameter: [state]");
        }

        if (!validationResult.Success)
        {
            return _errorResultProvider.GetAuthorizeErrorResult(DefaultAuthorizeErrorType.InvalidRequest, state, null);
        }

        if (!flowArgs.Values.TryGetValue("response_type", out string? responseType))
        {
            return _errorResultProvider.GetAuthorizeErrorResult(DefaultAuthorizeErrorType.InvalidRequest, state, null);
        }

        if (!flowArgs.Values.TryGetValue("client_id", out string? clientId))
        {
            return _errorResultProvider.GetAuthorizeErrorResult(DefaultAuthorizeErrorType.InvalidRequest, state, null);
        }

        Client? client = await _clientService.GetClientAsync(clientId);
        if (client is null)
        {
            return _errorResultProvider.GetAuthorizeErrorResult(
                DefaultAuthorizeErrorType.UnauthorizedClient,
                state,
                $"Client with [client_id] = [{clientId}] doesn't exist in the system.");
        }

        if (!_endUserService.IsAuthenticated())
        {
            return await _loginService.RedirectToLoginAsync(flowArgs);
        }

        var endUser = await _endUserService.GetCurrentEndUserAsync();
        if (endUser is null)
        {
            return _errorResultProvider.GetAuthorizeErrorResult(
                DefaultAuthorizeErrorType.UnauthorizedClient,
                state,
                "Current EndUser doesn't exist in the system.");
        }

        flowArgs.Values.TryGetValue("scope", out string? clientRequestedScope);

        bool redirectToPermissionsRequired = await _permissionsService.RedirectToPermissionsRequiredAsync(endUser, client);
        if (redirectToPermissionsRequired)
        {
            ScopeResult serverAllowedScopeResult = await _scopeService.GetServerAllowedScopeAsync(clientRequestedScope, client, state);
            await _permissionsService.AddPermissionsRequestAsync(serverAllowedScopeResult, endUser, client);

            return await _permissionsService.RedirectToPermissionsAsync(flowArgs, client, state);
        }

        ScopeResult scopeResult;
        bool endUserPermissionsReuired = await _permissionsService.EndUserPermissionsRequiredAsync(client);
        if (endUserPermissionsReuired)
        {
            scopeResult = await _scopeService.GetEndUserClientScopeAsync(clientRequestedScope, endUser, client, state);
        }
        else
        {
            scopeResult = await _scopeService.GetServerAllowedScopeAsync(clientRequestedScope, client, state);
        }

        if (_flowRouter.TryGetAuthorizeFlow(responseType, out IAuthorizeFlow? flow))
        {
            if (flow is null)
            {
                return _errorResultProvider.GetAuthorizeErrorResult(DefaultAuthorizeErrorType.ServerError, state, "Cannot determine the flow.");
            }

            return await flow.AuthorizeAsync(flowArgs, client, endUser, scopeResult);
        }
        else
        {
            // TODO: handle only this node unsupported response type
            return _errorResultProvider.GetAuthorizeErrorResult(DefaultAuthorizeErrorType.UnsupportedResponseType, state, null);
        }
    }
}
