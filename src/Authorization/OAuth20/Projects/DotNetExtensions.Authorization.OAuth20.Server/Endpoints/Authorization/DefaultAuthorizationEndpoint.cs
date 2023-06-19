// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Flows;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Models.Generic;
using DotNetExtensions.Authorization.OAuth20.Server.Flows;

namespace DotNetExtensions.Authorization.OAuth20.Server.Endpoints.Authorization;

/// <summary>
/// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-3.1"/>
/// </summary>
public class DefaultAuthorizationEndpoint : IAuthorizationEndpoint
{
    private readonly IFlowRouter _flowRouter;
    private readonly IArgumentsBuilder<FlowArguments> _flowArgsBuilder;
    private readonly IRequestValidator<IAuthorizationEndpoint> _requestValidator;

    public DefaultAuthorizationEndpoint(
        IFlowRouter flowRouter,
        IArgumentsBuilder<FlowArguments> flowArgsBuilder,
        IRequestValidator<IAuthorizationEndpoint> requestValidator)
    {
        _flowRouter = flowRouter;
        _flowArgsBuilder = flowArgsBuilder;
        _requestValidator = requestValidator;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var validationResult = _requestValidator.TryValidate(httpContext);

        if (!validationResult.Success)
        {
            throw new InvalidOperationException(validationResult.Description);
        }

        FlowArguments flowArgs = await _flowArgsBuilder.BuildArgumentsAsync(httpContext);

        if (!flowArgs.Values.TryGetValue("response_type", out string? responseType))
        {
            throw new ArgumentNullException("response_type");
        }

        if (_flowRouter.TryGetAuthorizeFlow(responseType, out IAuthorizeFlow? flow))
        {
            IResult result = await flow!.AuthorizeAsync(flowArgs);
            await result.ExecuteAsync(httpContext);
        }
        else
        {
            throw new NotSupportedException($"{nameof(responseType)}:{responseType}");
        }
    }
}
