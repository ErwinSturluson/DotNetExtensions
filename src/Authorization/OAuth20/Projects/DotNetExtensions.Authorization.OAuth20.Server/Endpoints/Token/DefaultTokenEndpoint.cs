// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Flows;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Models.Generic;
using DotNetExtensions.Authorization.OAuth20.Server.Flows;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using Microsoft.Extensions.Options;

namespace DotNetExtensions.Authorization.OAuth20.Server.Endpoints.Token;

/// <summary>
/// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-3.2"/>
/// </summary>
public class DefaultTokenEndpoint : ITokenEndpoint
{
    private readonly IFlowRouter _flowRouter;
    private readonly IArgumentsBuilder<FlowArguments> _flowArgsBuilder;
    private readonly IRequestValidator<ITokenEndpoint> _requestValidator;
    private readonly IErrorResultProvider _errorResultProvider;
    private readonly IOptions<OAuth20ServerOptions> _options;

    public DefaultTokenEndpoint(
        IFlowRouter flowRouter,
        IArgumentsBuilder<FlowArguments> flowArgsBuilder,
        IRequestValidator<ITokenEndpoint> requestValidator,
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
        var validationResult = _requestValidator.TryValidate(httpContext);

        FlowArguments flowArgs = await _flowArgsBuilder.BuildArgumentsAsync(httpContext);

        if (!validationResult.Success)
        {
            flowArgs.Values.TryGetValue("state", out string? state);
            return _errorResultProvider.GetTokenErrorResult(DefaultTokenErrorType.InvalidRequest, state, null, _options.Value);
        }

        if (!flowArgs.Values.TryGetValue("grant_type", out string? responseType))
        {
            flowArgs.Values.TryGetValue("state", out string? state);
            return _errorResultProvider.GetTokenErrorResult(DefaultTokenErrorType.InvalidRequest, state, null, _options.Value);
        }

        if (_flowRouter.TryGetTokenFlow(responseType, out ITokenFlow? flow))
        {
            return await flow!.GetTokenAsync(flowArgs);
        }
        else
        {
            flowArgs.Values.TryGetValue("state", out string? state);
            return _errorResultProvider.GetTokenErrorResult(DefaultTokenErrorType.UnsupportedGrantType, state, null, _options.Value);
        }
    }
}
