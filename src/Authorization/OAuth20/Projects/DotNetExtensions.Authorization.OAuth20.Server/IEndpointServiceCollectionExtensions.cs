﻿// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Endpoints;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Models.Generic;
using DotNetExtensions.Authorization.OAuth20.Server.Default.Endpoints;
using DotNetExtensions.Authorization.OAuth20.Server.Endpoints.Authorization;
using DotNetExtensions.Authorization.OAuth20.Server.Endpoints.Token;
using DotNetExtensions.Authorization.OAuth20.Server.Flows;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace DotNetExtensions.Authorization.OAuth20.Server;

public static class IEndpointServiceCollectionExtensions
{
    public static IServiceCollection SetOAuth20Endpoints(this IServiceCollection services)
    {
        services.AddSingleton<IEndpointMetadataCollection, DefaultEndpointMetadataCollection>();

        services.SetOAuth20DefaultEndpoints();
        services.SetOAuth20EndpointsFromConfiguration();

        services.AddScoped<IEndpointProvider, DefaultEndpointProvider>();
        services.AddScoped<IEndpointRouter, DefaultEndpointRouter>();
        services.AddScoped<IArgumentsBuilder<FlowArguments>, FlowArgumentsBuilder>();

        return services;
    }

    public static IServiceCollection SetOAuth20EndpointsFromConfiguration(this IServiceCollection services)
    {
        var servicesScope = services.BuildServiceProvider().CreateScope();
        var options = servicesScope.ServiceProvider.GetRequiredService<IOptions<OAuth20ServerOptions>>().Value;

        if (options.Endpoints?.EndpointList is not null && options.Endpoints.EndpointList.Any())
        {
            foreach (var endpointOptions in options.Endpoints.EndpointList)
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

                services.SetOAuth20Endpoint(endpointMetadata, implementationType!);
            }
        }

        return services;
    }

    public static IServiceCollection SetOAuth20Endpoint<TAbstraction, TImplementation>(this IServiceCollection services, string route, string? description = null)
        where TImplementation : TAbstraction
        where TAbstraction : IEndpoint
        => services.SetOAuth20Endpoint(route, typeof(TAbstraction), typeof(TImplementation));

    public static IServiceCollection SetOAuth20Endpoint(this IServiceCollection services, string route, Type abstraction, Type implementation, string? description = null)
        => services.SetOAuth20Endpoint(EndpointMetadata.Create(route, abstraction, description), implementation);

    public static IServiceCollection SetOAuth20Endpoint(this IServiceCollection services, EndpointMetadata endpointMetadata, Type implementation)
    {
        services.SetOAuth20Endpoint(endpointMetadata);
        services.AddScoped(endpointMetadata.Abstraction, implementation);

        return services;
    }

    public static IServiceCollection SetOAuth20Endpoint<TImplementation>(this IServiceCollection services, EndpointMetadata endpointMetadata)
        where TImplementation : IEndpoint
        => services.SetOAuth20Endpoint(endpointMetadata, typeof(TImplementation));

    private static IServiceCollection SetOAuth20DefaultEndpoints(this IServiceCollection services)
    {
        var servicesScope = services.BuildServiceProvider().CreateScope();
        var options = servicesScope.ServiceProvider.GetRequiredService<IOptions<OAuth20ServerOptions>>().Value;

        services.SetOAuth20DefaultEndpoint<IAuthorizationEndpoint, DefaultAuthorizationEndpoint>(options.Endpoints?.AuthorizationEndpointRoute ?? "/oauth/authorize", "Authorization Endpoint");
        services.SetOAuth20DefaultEndpoint<ITokenEndpoint, DefaultTokenEndpoint>(options.Endpoints?.TokenEndpointRoute ?? "/oauth/token", "Token Endpoint");

        services.SetOAuth20DefaultEndpointValidators();

        return services;
    }

    private static IServiceCollection SetOAuth20DefaultEndpointValidators(this IServiceCollection services)
    {
        services.AddScoped<IRequestValidator<IAuthorizationEndpoint>, DefaultAuthorizationRequestValidator>();
        services.AddScoped<IRequestValidator<ITokenEndpoint>, DefaultTokenRequestValidator>();

        return services;
    }

    private static IServiceCollection SetOAuth20DefaultEndpoint<TDefaultAbstraction, TDefaultImplementation>(this IServiceCollection services, string route, string? defaultDescription = null)
        where TDefaultImplementation : TDefaultAbstraction
        where TDefaultAbstraction : IEndpoint
        => services.SetOAuth20DefaultEndpoint(route, typeof(TDefaultAbstraction), typeof(TDefaultImplementation), defaultDescription);

    private static IServiceCollection SetOAuth20DefaultEndpoint(this IServiceCollection services, string route, Type defaultAbstraction, Type defaultImplementation, string? defaultDescription = null)
    {
        services.SetOAuth20Endpoint(EndpointMetadata.Create(route, defaultAbstraction, defaultDescription), defaultImplementation);

        return services;
    }

    private static IServiceCollection SetOAuth20Endpoint(this IServiceCollection services, EndpointMetadata endpointMetadata)
    {
        using var servicesScope = services.BuildServiceProvider().CreateScope();
        var endpointMetadataCollection = servicesScope.ServiceProvider.GetRequiredService<IEndpointMetadataCollection>();

        endpointMetadataCollection.Endpoints[endpointMetadata.Route] = endpointMetadata;

        services.AddSingleton(endpointMetadataCollection);

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
        Assembly? asm = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.GetName().Name == assemblyName);
        type = asm?.GetTypes().FirstOrDefault(x => x.Name == typeName);

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
