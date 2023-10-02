// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.TokenBuilders;
using DotNetExtensions.Authorization.OAuth20.Server.Default.TokenBuilders;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using DotNetExtensions.Authorization.OAuth20.Server.TokenBuilders.Basic;
using DotNetExtensions.Authorization.OAuth20.Server.TokenBuilders.Jwt;
using DotNetExtensions.Authorization.OAuth20.Server.TokenBuilders.Mac;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace DotNetExtensions.Authorization.OAuth20.Server;

public static class ITokenBuilderServiceCollectionExtensions
{
    public static IServiceCollection SetOAuth20TokenBuilders(this IServiceCollection services)
    {
        services.AddSingleton<ITokenBuilderMetadataCollection, DefaultTokenBuilderMetadataCollection>();

        services.SetOAuth20DefaultTokenBuilders();
        services.SetOAuth20TokenBuildersFromConfiguration();

        services.AddScoped<ITokenBuilderProvider, DefaultTokenBuilderProvider>();
        services.AddScoped<ITokenBuilderSelector, DefaultTokenBuilderSelector>();

        return services;
    }

    public static IServiceCollection SetOAuth20TokenBuildersFromConfiguration(this IServiceCollection services)
    {
        var options = services.BuildServiceProvider().GetRequiredService<IOptions<OAuth20ServerOptions>>().Value;

        if (options.TokenBuilders?.TokenBuilderList is not null && options.TokenBuilders.TokenBuilderList.Any())
        {
            foreach (var tokenBuilderOptions in options.TokenBuilders.TokenBuilderList)
            {
                if (tokenBuilderOptions.Implementation is null)
                {
                    continue;
                }

                if (tokenBuilderOptions.Abstraction is null || !TryGetType(tokenBuilderOptions.Abstraction.AssemblyName, tokenBuilderOptions.Abstraction.TypeName, out Type? abstractionType))
                {
                    if (!TryGetType(services, tokenBuilderOptions.Type, out abstractionType))
                    {
                        continue;
                    }
                }

                var tokenBuilderMetadata = TokenBuilderMetadata.Create(tokenBuilderOptions.Type, abstractionType!, tokenBuilderOptions.Description);

                if (!TryGetType(tokenBuilderOptions.Implementation.AssemblyName, tokenBuilderOptions.Implementation.TypeName, out Type? implementationType))
                {
                    continue;
                }

                services.SetOAuth20TokenBuilder(tokenBuilderMetadata, implementationType!);
            }
        }

        return services;
    }

    public static IServiceCollection SetOAuth20TokenBuilder<TAbstraction, TImplementation>(this IServiceCollection services, string route, string? description = null)
        where TImplementation : TAbstraction
        where TAbstraction : ITokenBuilder
        => services.SetOAuth20TokenBuilder(route, typeof(TAbstraction), typeof(TImplementation));

    public static IServiceCollection SetOAuth20TokenBuilder(this IServiceCollection services, string type, Type abstraction, Type implementation, string? description = null)
        => services.SetOAuth20TokenBuilder(TokenBuilderMetadata.Create(type, abstraction, description), implementation);

    public static IServiceCollection SetOAuth20TokenBuilder(this IServiceCollection services, TokenBuilderMetadata tokenBuilderMetadata, Type implementation)
    {
        services.SetOAuth20TokenBuilder(tokenBuilderMetadata);
        services.AddScoped(tokenBuilderMetadata.Abstraction, implementation);

        return services;
    }

    public static IServiceCollection SetOAuth20TokenBuilder<TImplementation>(this IServiceCollection services, TokenBuilderMetadata tokenBuilderMetadata)
        where TImplementation : ITokenBuilder
        => services.SetOAuth20TokenBuilder(tokenBuilderMetadata, typeof(TImplementation));

    private static IServiceCollection SetOAuth20DefaultTokenBuilders(this IServiceCollection services)
    {
        var options = services.BuildServiceProvider().GetRequiredService<IOptions<OAuth20ServerOptions>>().Value;

        services.SetOAuth20DefaultTokenBuilder<IBasicTokenBuilder, DefaultBasicTokenBuilder>(options.TokenBuilders?.BasicTokenTypeName ?? "Basic", "Basic Token Builder");
        services.SetOAuth20DefaultTokenBuilder<IJwtTokenBuilder, DefaultJwtTokenBuilder>(options.TokenBuilders?.JwtTokenTypeName ?? "JWT", "JWT Token Builder");
        services.SetOAuth20DefaultTokenBuilder<IMacTokenBuilder, DefaultMacTokenBuilder>(options.TokenBuilders?.MacTokenTypeName ?? "MAC", "MAC Token Builder");

        return services;
    }

    private static IServiceCollection SetOAuth20DefaultTokenBuilder<TDefaultAbstraction, TDefaultImplementation>(this IServiceCollection services, string type, string? defaultDescription = null)
        where TDefaultImplementation : TDefaultAbstraction
        where TDefaultAbstraction : ITokenBuilder
        => services.SetOAuth20DefaultTokenBuilder(type, typeof(TDefaultAbstraction), typeof(TDefaultImplementation), defaultDescription);

    private static IServiceCollection SetOAuth20DefaultTokenBuilder(this IServiceCollection services, string type, Type defaultAbstraction, Type defaultImplementation, string? defaultDescription = null)
    {
        services.SetOAuth20TokenBuilder(TokenBuilderMetadata.Create(type, defaultAbstraction, defaultDescription), defaultImplementation);

        return services;
    }

    private static IServiceCollection SetOAuth20TokenBuilder(this IServiceCollection services, TokenBuilderMetadata tokenBuilderMetadata)
    {
        var tokenBuilderMetadataCollection = services.BuildServiceProvider().GetRequiredService<ITokenBuilderMetadataCollection>();

        tokenBuilderMetadataCollection.TokenBuilders[tokenBuilderMetadata.Type] = tokenBuilderMetadata;

        services.AddSingleton(tokenBuilderMetadataCollection);

        return services;
    }

    private static bool TryGetType(IServiceCollection services, string tokenType, out Type? type)
    {
        var tokenBuilderMetadataCollection = services.BuildServiceProvider().GetRequiredService<ITokenBuilderMetadataCollection>();

        if (tokenBuilderMetadataCollection.TokenBuilders.TryGetValue(tokenType, out TokenBuilderMetadata? tokenBuilderMetadata))
        {
            type = tokenBuilderMetadata.Abstraction;
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
