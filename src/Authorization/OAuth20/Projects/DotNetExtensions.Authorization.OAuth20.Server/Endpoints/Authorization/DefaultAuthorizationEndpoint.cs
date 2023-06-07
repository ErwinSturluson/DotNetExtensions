// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Endpoints.Authorization;

public class DefaultAuthorizationEndpoint : IAuthorizationEndpoint
{
    public DefaultAuthorizationEndpoint()
    {
    }

    public Task InvokeAsync(HttpContext httpContext)
    {
        throw new NotImplementedException();
    }
}
