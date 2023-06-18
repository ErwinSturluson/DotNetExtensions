// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Endpoints;

public interface IEndpointRouter
{
    public bool TryGetEndpoint(HttpContext httpContext, out IEndpoint? endPoint);
}
