// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions;
using DotNetExtensions.Authorization.OAuth20.Server.Default;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using Microsoft.Extensions.Options;

namespace DotNetExtensions.Authorization.OAuth20.Server;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddOAuth20Server(this IServiceCollection services, Func<IServiceCollection, OAuth20ServerOptions>? optionsConfiguration = null)
    {
        services.AddOAuth20Options(optionsConfiguration);

        services.SetOAuth20Endpoints();
        services.SetOAuth20Flows();

        services.AddScoped<ITlsValidator, DefaultTlsValidator>();

        return services;
    }

    private static IServiceCollection AddOAuth20Options(this IServiceCollection services, Func<IServiceCollection, OAuth20ServerOptions>? optionsConfiguration = null)
    {
        if (optionsConfiguration is not null)
        {
            OAuth20ServerOptions options = optionsConfiguration(services);
            services.AddSingleton(Microsoft.Extensions.Options.Options.Create(options));
        }
        else
        {
            var servicesScope = services.BuildServiceProvider().CreateScope();
            var configuration = servicesScope.ServiceProvider.GetRequiredService<IConfiguration>();
            IConfigurationSection convigurationSection = configuration.GetSection(OAuth20ServerOptions.DefaultSection);

            if (convigurationSection is not null)
            {
                services.Configure<OAuth20ServerOptions>(convigurationSection);
                services.AddSingleton<IValidateOptions<OAuth20ServerOptions>, OAuth20ServerOptionsValidator>();
            }
            else
            {
                services.AddSingleton(Microsoft.Extensions.Options.Options.Create(new OAuth20ServerOptions()));
            }
        }

        return services;
    }
}
