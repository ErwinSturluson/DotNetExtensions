// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions;

namespace DotNetExtensions.Authorization.OAuth20.Server.Endpoints.Token;

public class DefaultTokenRequestValidator : IRequestValidator<ITokenEndpoint>
{
    public OAuth20ValidationResult TryValidate(HttpContext httpContext)
    {
        OAuth20ValidationResult result = new();

        if (httpContext.Request.Method == HttpMethod.Post.Method) result.Success = true;

        return result;
    }
}
