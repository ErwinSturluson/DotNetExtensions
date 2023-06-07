// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions;
using DotNetExtensions.Authorization.OAuth20.Server.Default;
using DotNetExtensions.Authorization.OAuth20.Server.Endpoints.Authorization;
using DotNetExtensions.Authorization.OAuth20.Server.Endpoints.Token;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace DotNetExtensions.Authorization.OAuth20.Server;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddOAuth20Server(this IServiceCollection services, Func<IServiceCollection, OAuth20ServerOptions>? optionsConfiguration = null)
    {
        OAuth20ServerOptions options;

        if (optionsConfiguration is not null)
        {
            options = optionsConfiguration(services);
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

        services.AddScoped<IEndpointMetadataCollection, DefaultEndpointMetadataCollection>();

        services.SetOAuth20Endpoint<DefaultAuthorizationEndpoint>(EndpointMetadata.Create<IAuthorizationEndpoint>("Authorization Endpoint", "/oauth/authorize"));
        services.SetOAuth20Endpoint<DefaultTokenEndpoint>(EndpointMetadata.Create<ITokenEndpoint>("Token Endpoint", "/oauth/token"));

        services.SetOAuth20EndpointsFromConfiguration();

        services.AddScoped<IEndpointProvider, DefaultEndpointProvider>();
        services.AddScoped<IEndpointRouter, DefaultEndpointRouter>();
        services.AddScoped<ITlsValidator, DefaultTlsValidator>();

        return services;
    }

    public static IServiceCollection SetOAuth20EndpointsFromConfiguration(this IServiceCollection services)
    {
        var servicesScope = services.BuildServiceProvider().CreateScope();
        var options = servicesScope.ServiceProvider.GetRequiredService<IOptions<OAuth20ServerOptions>>().Value;

        foreach (var endpointOptions in options.Endpoints)
        {
            if (endpointOptions.Implementation is null)
            {
                continue;
            }

            if (endpointOptions.Abstraction is null || !TryGetType(endpointOptions.Abstraction.AssemblyName, endpointOptions.Abstraction.TypeName, out Type? abstractionType))
            {
                if (!TryGetType(services, endpointOptions.Route, out abstractionType))
                {
                    continue;
                }
            }

            var endpointMetadata = EndpointMetadata.Create(endpointOptions.Route, abstractionType!, endpointOptions.Description);

            if (!TryGetType(endpointOptions.Implementation.AssemblyName, endpointOptions.Implementation.TypeName, out Type? implementationType))
            {
                continue;
            }

            SetOAuth20Endpoint(services, endpointMetadata, implementationType!);
        }

        return services;
    }

    public static IServiceCollection SetOAuth20Endpoint<TAbstraction, TImplementation>(this IServiceCollection services, string route, string? description = null)
        where TImplementation : TAbstraction
        where TAbstraction : IEndpoint
        => SetOAuth20Endpoint(services, route, typeof(TAbstraction), typeof(TImplementation));

    public static IServiceCollection SetOAuth20Endpoint(this IServiceCollection services, string route, Type abstraction, Type implementation, string? description = null)
        => SetOAuth20Endpoint(services, EndpointMetadata.Create(route, abstraction, description), implementation);

    public static IServiceCollection SetOAuth20Endpoint(this IServiceCollection services, EndpointMetadata endpointMetadata, Type implementation)
    {
        SetOAuth20Endpoint(services, endpointMetadata);
        services.AddScoped(endpointMetadata.Abstraction, implementation);

        return services;
    }

    public static IServiceCollection SetOAuth20Endpoint<TImplementation>(this IServiceCollection services, EndpointMetadata endpointMetadata)
        where TImplementation : IEndpoint
        => SetOAuth20Endpoint(services, endpointMetadata, typeof(TImplementation));

    private static IServiceCollection SetOAuth20Endpoint(this IServiceCollection services, EndpointMetadata endpointMetadata)
    {
        using var servicesScope = services.BuildServiceProvider().CreateScope();
        var endpointMetadataCollection = servicesScope.ServiceProvider.GetRequiredService<IEndpointMetadataCollection>();

        endpointMetadataCollection.Endpoints[endpointMetadata.Route] = endpointMetadata;

        return services;
    }

    private static bool TryGetType(IServiceCollection services, string route, out Type? type)
    {
        var servicesScope = services.BuildServiceProvider().CreateScope();
        var endpointMetadataCollection = servicesScope.ServiceProvider.GetRequiredService<IEndpointMetadataCollection>();

        if (endpointMetadataCollection.Endpoints.TryGetValue(route, out EndpointMetadata? endpointMetadata))
        {
            type = endpointMetadata.Abstraction;
            return true;
        }
        else
        {
            type = null;
            return false;
        }
    }

    private static bool TryGetType(string assemblyName, string typeName, out Type? type)
    {
        AssemblyName? an = Assembly.GetCallingAssembly().GetReferencedAssemblies().FirstOrDefault(x => x.Name == assemblyName);

        if (an is null)
        {
            type = null;
            return false;
        }

        Assembly asm = Assembly.Load(an.ToString());
        type = asm.GetTypes().FirstOrDefault(x => x.Name == typeName);

        if (type is not null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
