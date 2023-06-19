﻿// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Endpoints;

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
        if (router.TryGetEndpoint(context, out IEndpoint? endpoint))
        {
            var validationResult = tlsValidator.TryValidate(context);

            if (!validationResult.Success)
            {
                throw new InvalidOperationException(validationResult.Description);
            }

            await endpoint!.InvokeAsync(context);
        }
        else
        {
            await _next.Invoke(context);
        }
    }
}
