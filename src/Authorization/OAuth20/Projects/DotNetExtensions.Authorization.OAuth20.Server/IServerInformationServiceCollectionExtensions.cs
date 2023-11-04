// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.ServerInformation;
using DotNetExtensions.Authorization.OAuth20.Server.Default.ServerInformation;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace DotNetExtensions.Authorization.OAuth20.Server;

public static class IServerInformationServiceCollectionExtensions
{
    public static IServiceCollection SetOAuth20ServerInformation(this IServiceCollection services)
    {
        services.AddSingleton<IServerInformationMetadata, DefaultServerInformationMetadata>();

        services.SetDefaultInformaion();

        services.AddSingleton<IServerInformationService, DefaultServerInformationService>();

        return services;
    }

    public static IServiceCollection SetDefaultInformaion(this IServiceCollection services)
    {
        var options = services.BuildServiceProvider().GetRequiredService<IOptions<OAuth20ServerOptions>>().Value;

        var serverInformationMetadata = services.BuildServiceProvider().GetRequiredService<IServerInformationMetadata>();

        if (options.Information?.ScopeAdditional is not null && options.Information.ScopeAdditional.Any())
        {
            if (serverInformationMetadata.ScopeAdditional is null)
            {
                serverInformationMetadata.ScopeAdditional = new ConcurrentDictionary<string, string>();
            }

            foreach (var scopeInformationItem in options.Information.ScopeAdditional)
            {
                serverInformationMetadata.ScopeAdditional.Add(scopeInformationItem);
            }
        }

        serverInformationMetadata.ScopeDefaultValue = options.Information?.ScopeDefaultValue;
        serverInformationMetadata.ScopeRequirements = options.Information?.ScopeRequirements;

        if (options.Information?.AuthorizationCodeAdditional is not null && options.Information.AuthorizationCodeAdditional.Any())
        {
            if (serverInformationMetadata.AuthorizationCodeAdditional is null)
            {
                serverInformationMetadata.AuthorizationCodeAdditional = new ConcurrentDictionary<string, string>();
            }

            foreach (var authorizationCodeInformationItem in options.Information.AuthorizationCodeAdditional)
            {
                serverInformationMetadata.AuthorizationCodeAdditional.Add(authorizationCodeInformationItem);
            }
        }

        if (options.Information?.AuthorizationCodeSizeSymbols is null)
        {
            // TODO: a more advanced determination of the authorization code's length.
            int authorizationCodeSizeSymbols = Guid.NewGuid().ToString("N").Length * 2;

            serverInformationMetadata.AuthorizationCodeSizeSymbols = authorizationCodeSizeSymbols.ToString();
        }

        services.AddSingleton(serverInformationMetadata);

        return services;
    }
}
