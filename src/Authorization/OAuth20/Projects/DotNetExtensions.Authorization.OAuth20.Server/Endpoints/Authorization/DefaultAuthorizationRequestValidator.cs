// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using Microsoft.Extensions.Options;

namespace DotNetExtensions.Authorization.OAuth20.Server.Endpoints.Authorization;

public class DefaultAuthorizationRequestValidator : IRequestValidator<IAuthorizationEndpoint>
{
    private readonly IOptions<OAuth20ServerOptions> _options;

    public DefaultAuthorizationRequestValidator(IOptions<OAuth20ServerOptions> options)
    {
        _options = options;
    }

    public ValidationResult TryValidate(HttpContext httpContext)
    {
        ValidationResult result = new();

        if (httpContext.Request.Method == HttpMethod.Get.Method)
        {
            result.Success = true;
        }
        else if (_options.Value.Endpoints.AuthorizationEndpointHttpPostEnabled && httpContext.Request.Method == HttpMethod.Post.Method)
        {
            result.Success = true;
        }
        else
        {
            result.Success = false;
        }

        return result;
    }
}
