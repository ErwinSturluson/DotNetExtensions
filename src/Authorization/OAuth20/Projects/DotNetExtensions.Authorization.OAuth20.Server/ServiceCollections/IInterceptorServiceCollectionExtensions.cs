// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Interceptors;
using DotNetExtensions.Authorization.OAuth20.Server.Default.Interceptors;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using Microsoft.Extensions.Options;

namespace DotNetExtensions.Authorization.OAuth20.Server.ServiceCollections;

public static class IInterceptorServiceCollectionExtensions
{
    public static IServiceCollection SetOAuth20LoggingScopeInterceptor(this IServiceCollection services)
    {
        var options = services.BuildServiceProvider().GetRequiredService<IOptions<OAuth20ServerOptions>>().Value;

        bool enableInterceptors = options.EnableLoggingScopeInterceptor;

        if (enableInterceptors)
        {
            services.AddScoped<IScopeInterceptor, LoggingScopeInterceptor>();
        }

        return services;
    }
}
