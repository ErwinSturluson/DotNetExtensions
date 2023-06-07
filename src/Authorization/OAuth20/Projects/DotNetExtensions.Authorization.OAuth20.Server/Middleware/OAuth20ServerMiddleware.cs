// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions;

namespace DotNetExtensions.Authorization.OAuth20.Server.Middleware;

public class OAuth20ServerMiddleware
{
    private readonly RequestDelegate _next;

    public OAuth20ServerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IEndpointRouter router, ITlsValidator tlsValidator)
    {
        if (router.TryGetEndpoint(context, out IEndpoint? endPoint))
        {
            bool isValidTls = tlsValidator.TryValidate(context);

            if (!isValidTls)
            {
                throw new Exception(nameof(isValidTls));
            }

            await endPoint!.InvokeAsync(context);
        }
        else
        {
            await _next.Invoke(context);
        }
    }
}
