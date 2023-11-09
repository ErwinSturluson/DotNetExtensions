// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataSources;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Providers;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;
using DotNetExtensions.Authorization.OAuth20.Server.Default;
using DotNetExtensions.Authorization.OAuth20.Server.Default.Providers;
using DotNetExtensions.Authorization.OAuth20.Server.Default.Services;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using Microsoft.Extensions.Options;

namespace DotNetExtensions.Authorization.OAuth20.Server;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddOAuth20Server(this IServiceCollection services, IDataSourceContext dataSourceContext, Action<OAuth20ServerOptions>? optionsConfiguration = null)
    {
        services.AddOAuth20Options(optionsConfiguration);

        services.SetOAuth20DataSources(dataSourceContext);
        services.SetOAuth20Endpoints();
        services.SetOAuth20Flows();
        services.SetOAuth20Errors();
        services.SetOAuth20TokenTypes();
        services.SetOAuth20ClientSecretTypes();
        services.SetOAuth20ServerSigningCredentials();
        services.SetOAuth20ServerInformation();

        services.AddScoped<ITlsValidator, DefaultTlsValidator>();

        return services;
    }

    public static IServiceCollection AddProviders(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationCodeProvider, EncryptedGuidAuthorizationCodeProvider>();
        services.AddScoped<ITokenProvider, DefaultTokenProvider>();
        services.AddScoped<IRefreshTokenProvider, EncryptedGuidRefreshTokenProvider>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationCodeService, DefaultAuthorizationCodeService>();
        services.AddScoped<IClientAuthenticationService, DefaultClientAuthenticationService>();
        services.AddScoped<IClientSecretService, DefaultClientSecretService>();
        services.AddScoped<IClientService, DefaultClientService>();
        services.AddScoped<IDateTimeService, UtcDateTimeService>();
        services.AddScoped<IEndUserService, DefaultEndUserService>();
        services.AddScoped<IFlowService, DefaultFlowService>();
        services.AddScoped<ILoginService, DefaultLoginService>();
        services.AddScoped<IResourceService, DefaultResourceService>();
        services.AddScoped<IScopeService, DefaultScopeService>();
        services.AddScoped<IServerMetadataService, DefaultServerMetadataService>();
        services.AddScoped<ISigningCredentialsAlgorithmsService, DefaultSigningCredentialsAlgorithmsService>();
        services.AddScoped<ITokenService, DefaultTokenService>();
        services.AddScoped<IRefreshTokenService, DefaultRefreshTokenService>();

        return services;
    }

    private static IServiceCollection AddOAuth20Options(this IServiceCollection services, Action<OAuth20ServerOptions>? optionsConfiguration = null)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        IConfigurationSection configurationSection = configuration.GetSection(OAuth20ServerOptions.DefaultSection);

        services.Configure<OAuth20ServerOptions>(configurationSection);

        if (configurationSection is not null)
        {
            var options = services.BuildServiceProvider().GetRequiredService<IOptions<OAuth20ServerOptions>>();

            optionsConfiguration!.Invoke(options.Value);
        }

        services.AddSingleton<IValidateOptions<OAuth20ServerOptions>, OAuth20ServerOptionsValidator>();

        return services;
    }
}
