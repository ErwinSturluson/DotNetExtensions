// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Middlewares;

namespace DotNetExtensions.Authorization.OAuth20.Server;

public static class IApplicationBuilderExtensions
{
    public static IApplicationBuilder UseOAuth20Server(this IApplicationBuilder app)
    {
        app.UseMiddleware<OAuth20ServerEndpointsMiddleware>();
        app.UseMiddleware<OAuth20ServerWebPagesMiddleware>();

        return app;
    }
}
