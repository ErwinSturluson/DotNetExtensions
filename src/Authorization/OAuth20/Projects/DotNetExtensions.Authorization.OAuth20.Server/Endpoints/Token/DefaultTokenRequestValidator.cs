// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions;

namespace DotNetExtensions.Authorization.OAuth20.Server.Endpoints.Token;

public class DefaultTokenRequestValidator : IRequestValidator<ITokenEndpoint>
{
    public bool TryValidate(HttpContext httpContext)
    {
        if (httpContext.Request.Method == HttpMethod.Post.Method) return true;

        return false;
    }
}
