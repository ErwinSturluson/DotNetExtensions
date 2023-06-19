// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Flows;
using DotNetExtensions.Authorization.OAuth20.Server.Default.Flows;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.AuthorizationCode;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.ClientCredentials;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.Implicit;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.RefreshToken;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.ResourceOwnerPasswordCredentials;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace DotNetExtensions.Authorization.OAuth20.Server;

public static class IFlowServiceCollectionExtensions
{
    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-4
    /// </summary>
    public static IServiceCollection SetOAuth20Flows(this IServiceCollection services)
    {
        services.AddScoped<IFlowMetadataCollection, DefaultFlowMetadataCollection>();

        services.SetOAuth20DefaultFlows();

        services.SetOAuth20FlowsFromConfiguration();

        services.AddScoped<IFlowProvider, DefaultFlowProvider>();
        services.AddScoped<IFlowRouter, DefaultFlowRouter>();

        return services;
    }

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-8.3
    /// </summary>
    public static IServiceCollection SetOAuth20FlowsFromConfiguration(this IServiceCollection services)
    {
        var servicesScope = services.BuildServiceProvider().CreateScope();
        var options = servicesScope.ServiceProvider.GetRequiredService<IOptions<OAuth20ServerOptions>>().Value;

        foreach (var flowOptions in options.Flows)
        {
            if (flowOptions.Implementation is null)
            {
                continue;
            }

            if (flowOptions.Abstraction is null || !TryGetType(flowOptions.Abstraction.AssemblyName, flowOptions.Abstraction.TypeName, out Type? abstractionType))
            {
                if (!TryGetType(services, flowOptions.GrantTypeName, flowOptions.ResponseTypeName, out abstractionType))
                {
                    continue;
                }
            }

            var flowMetadata = FlowMetadata.Create(flowOptions.GrantTypeName, flowOptions.ResponseTypeName, abstractionType!, flowOptions.Description);

            if (!TryGetType(flowOptions.Implementation.AssemblyName, flowOptions.Implementation.TypeName, out Type? implementationType))
            {
                continue;
            }

            SetOAuth20Flow(services, flowMetadata, implementationType!);
        }

        return services;
    }

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-8.3
    /// </summary>
    public static IServiceCollection SetOAuth20Flow<TAbstraction, TImplementation>(this IServiceCollection services, string grantTypeName, string responseTypeName, string? description = null)
        where TImplementation : TAbstraction
        where TAbstraction : IFlow
        => SetOAuth20Flow(services, grantTypeName, responseTypeName, typeof(TAbstraction), typeof(TImplementation), description);

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-8.3
    /// </summary>
    public static IServiceCollection SetOAuth20Flow(this IServiceCollection services, string grantTypeName, string responseTypeName, Type abstraction, Type implementation, string? description = null)
       => SetOAuth20Flow(services, FlowMetadata.Create(grantTypeName, responseTypeName, abstraction, description), implementation);

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-8.3
    /// </summary>
    public static IServiceCollection SetOAuth20Flow(this IServiceCollection services, FlowMetadata flowMetadata, Type implementation)
    {
        SetOAuth20Flow(services, flowMetadata);
        services.AddScoped(flowMetadata.Abstraction, implementation);

        return services;
    }

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-8.3
    /// </summary>
    public static IServiceCollection SetOAuth20Flow<TImplementation>(this IServiceCollection services, FlowMetadata flowMetadata)
        where TImplementation : IFlow
        => SetOAuth20Flow(services, flowMetadata, typeof(TImplementation));

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-4
    /// </summary>
    private static IServiceCollection SetOAuth20DefaultFlows(this IServiceCollection services)
    {
        var servicesScope = services.BuildServiceProvider().CreateScope();
        var options = servicesScope.ServiceProvider.GetRequiredService<IOptions<OAuth20ServerOptions>>().Value;

        services.SetOAuth20DefaultFlow<IAuthorizationCodeFlow, DefaultAuthorizationCodeFlow>(
            defaultGrantTypeName: options.AuthorizationFlowGrantTypeName ?? "authorization_code",
            defaultResponseTypeName: options.AuthorizationCodeFlowResponseTypeName ?? "code",
            defaultDescription: "Authorization flow");

        services.SetOAuth20DefaultFlow<IClientCredentialsFlow, DefaultClientCredentialsFlow>(
            defaultGrantTypeName: options.ClientCredentialsFlowGrantTypeName ?? "client_credentials",
            defaultResponseTypeName: null,
            defaultDescription: "Client credentials flow");

        services.SetOAuth20DefaultFlow<IImplicitFlow, DefaultImplicitFlow>(
            defaultGrantTypeName: null,
            defaultResponseTypeName: options.ImplicitFlowResponseTypeName ?? "token",
            defaultDescription: "Implicit flow");

        services.SetOAuth20DefaultFlow<IResourceOwnerPasswordCredentialsFlow, DefaultResourceOwnerPasswordCredentialsFlow>(
            defaultGrantTypeName: options.ResourceOwnerPasswordCredentialsFlowGrantTypeName ?? "password",
            defaultResponseTypeName: null,
            defaultDescription: "Resource owner password credentials flow");

        services.SetOAuth20DefaultFlow<IRefreshTokenFlow, DefaultRefreshTokenFlow>(
            defaultGrantTypeName: options.RefreshTokenFlowGrantTypeName ?? "refresh_token",
            defaultResponseTypeName: null,
            defaultDescription: "Refresh token flow");

        return services;
    }

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-8.3
    /// </summary>
    private static IServiceCollection SetOAuth20DefaultFlow<TDefaultAbstraction, TDefaultImplementation>(this IServiceCollection services, string? defaultGrantTypeName, string? defaultResponseTypeName, string? defaultDescription = null)
        where TDefaultImplementation : TDefaultAbstraction
        where TDefaultAbstraction : IFlow
        => services.SetOAuth20DefaultFlow(defaultGrantTypeName, defaultResponseTypeName, typeof(TDefaultAbstraction), typeof(TDefaultImplementation), defaultDescription);

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-8.3
    /// </summary>
    private static IServiceCollection SetOAuth20DefaultFlow(this IServiceCollection services, string? defaultGrantTypeName, string? defaultResponseTypeName, Type defaultAbstraction, Type defaultImplementation, string? defaultDescription = null)
        => services.SetOAuth20Flow(FlowMetadata.Create(defaultGrantTypeName, defaultResponseTypeName, defaultAbstraction, defaultDescription), defaultImplementation);

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-8.3
    /// </summary>
    private static IServiceCollection SetOAuth20Flow(this IServiceCollection services, FlowMetadata flowMetadata)
    {
        using var servicesScope = services.BuildServiceProvider().CreateScope();
        var flowMetadataCollection = servicesScope.ServiceProvider.GetRequiredService<IFlowMetadataCollection>();

        if (flowMetadata.GrantTypeName is not null)
        {
            if (!flowMetadata.Abstraction.IsAssignableTo(typeof(ITokenFlow)))
            {
                throw new InvalidOperationException($"{nameof(flowMetadata.Abstraction)}:{flowMetadata.Abstraction.Name} should be inherited from {nameof(ITokenFlow)}");
            }

            flowMetadataCollection.FlowsWithGrantType[flowMetadata.GrantTypeName] = flowMetadata;
        }

        if (flowMetadata.ResponseTypeName is not null)
        {
            if (!flowMetadata.Abstraction.IsAssignableTo(typeof(IAuthorizeFlow)))
            {
                throw new InvalidOperationException($"{nameof(flowMetadata.Abstraction)}:{flowMetadata.Abstraction.Name} should be inherited from {nameof(IAuthorizeFlow)}");
            }

            flowMetadataCollection.FlowsWithResponseType[flowMetadata.ResponseTypeName] = flowMetadata;
        }

        return services;
    }

    private static bool TryGetType(IServiceCollection services, string? grantTypeName, string? responseTypeName, out Type? type)
    {
        var servicesScope = services.BuildServiceProvider().CreateScope();
        var flowMetadataCollection = servicesScope.ServiceProvider.GetRequiredService<IFlowMetadataCollection>();

        if (grantTypeName is not null && flowMetadataCollection.FlowsWithGrantType.TryGetValue(grantTypeName, out FlowMetadata? flowMetadata) ||
            responseTypeName is not null && flowMetadataCollection.FlowsWithResponseType.TryGetValue(responseTypeName, out flowMetadata))
        {
            type = flowMetadata.Abstraction;
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
