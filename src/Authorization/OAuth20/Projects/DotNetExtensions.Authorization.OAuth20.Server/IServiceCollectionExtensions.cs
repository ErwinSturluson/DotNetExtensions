// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions;
using DotNetExtensions.Authorization.OAuth20.Server.Default;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using Microsoft.Extensions.Options;

namespace DotNetExtensions.Authorization.OAuth20.Server;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddOAuth20Server(this IServiceCollection services, Action<OAuth20ServerOptions>? optionsConfiguration = null)
    {
        services.AddOAuth20Options(optionsConfiguration);

        services.SetOAuth20Endpoints();
        services.SetOAuth20Flows();
        services.SetOAuth20Errors();
        services.SetOAuth20TokenTypes();
        services.SetOAuth20ClientSecretTypes();

        services.AddScoped<ITlsValidator, DefaultTlsValidator>();

        return services;
    }

    private static IServiceCollection AddOAuth20Options(this IServiceCollection services, Action<OAuth20ServerOptions>? optionsConfiguration = null)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        IConfigurationSection convigurationSection = configuration.GetSection(OAuth20ServerOptions.DefaultSection);

        services.Configure<OAuth20ServerOptions>(convigurationSection);

        if (convigurationSection is not null)
        {
            var options = services.BuildServiceProvider().GetRequiredService<IOptions<OAuth20ServerOptions>>();

            optionsConfiguration!.Invoke(options.Value);
        }

        services.AddSingleton<IValidateOptions<OAuth20ServerOptions>, OAuth20ServerOptionsValidator>();

        return services;
    }
}
