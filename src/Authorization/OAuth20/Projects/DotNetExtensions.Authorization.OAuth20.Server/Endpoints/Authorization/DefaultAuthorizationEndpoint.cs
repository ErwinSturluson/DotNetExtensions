﻿// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Builders.Generic;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Flows;
using DotNetExtensions.Authorization.OAuth20.Server.Flows;
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
    private readonly IOptions<OAuth20ServerOptions> _options;

    public DefaultAuthorizationEndpoint(
        IFlowRouter flowRouter,
        IArgumentsBuilder<FlowArguments> flowArgsBuilder,
        IRequestValidator<IAuthorizationEndpoint> requestValidator,
        IErrorResultProvider errorResultProvider,
        IOptions<OAuth20ServerOptions> options)
    {
        _flowRouter = flowRouter;
        _flowArgsBuilder = flowArgsBuilder;
        _requestValidator = requestValidator;
        _errorResultProvider = errorResultProvider;
        _options = options;
    }

    public async Task<IResult> InvokeAsync(HttpContext httpContext)
    {
        FlowArguments flowArgs = await _flowArgsBuilder.BuildArgumentsAsync(httpContext);
        var validationResult = _requestValidator.TryValidate(httpContext);

        if (!validationResult.Success)
        {
            flowArgs.Values.TryGetValue("state", out string? state);
            return _errorResultProvider.GetAuthorizeErrorResult(DefaultAuthorizeErrorType.InvalidRequest, state, null);
        }

        if (!flowArgs.Values.TryGetValue("response_type", out string? responseType))
        {
            flowArgs.Values.TryGetValue("state", out string? state);
            return _errorResultProvider.GetAuthorizeErrorResult(DefaultAuthorizeErrorType.InvalidRequest, state, null);
        }

        if (_flowRouter.TryGetAuthorizeFlow(responseType, out IAuthorizeFlow? flow))
        {
            return await flow!.AuthorizeAsync(flowArgs);
        }
        else
        {
            flowArgs.Values.TryGetValue("state", out string? state);
            // TODO: handle only this node unsupported response type
            return _errorResultProvider.GetAuthorizeErrorResult(DefaultAuthorizeErrorType.UnsupportedResponseType, state, null);
        }
    }
}