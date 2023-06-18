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

    public bool TryValidate(HttpContext httpContext)
    {
        if (httpContext.Request.Method == HttpMethod.Get.Method) return true;

        if (_options.Value.AuthorizationEndpointHttpPostEnabled && httpContext.Request.Method == HttpMethod.Post.Method) return true;

        return false;
    }
}
