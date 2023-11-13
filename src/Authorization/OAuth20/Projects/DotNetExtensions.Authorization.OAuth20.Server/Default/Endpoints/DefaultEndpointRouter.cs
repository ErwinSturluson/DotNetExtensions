// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Endpoints;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.Endpoints;

public class DefaultEndpointRouter : IEndpointRouter
{
    private readonly IEndpointProvider _endpointProvider;

    public DefaultEndpointRouter(IEndpointProvider endpointProvider)
    {
        _endpointProvider = endpointProvider;
    }

    public bool TryGetEndpoint(HttpContext httpContext, out IEndpoint? endPoint)
    {
        string endpointPath = httpContext.Request.Path.ToUriComponent();

        return _endpointProvider.TryGetEndpointInstanceByRoute(endpointPath, out endPoint);
    }
}
