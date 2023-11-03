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

        if (serverInformationMetadata.Scope is null)
        {
            serverInformationMetadata.Scope = new ConcurrentDictionary<string, string>();
        }

        if (options.Information?.Scope is not null && options.Information.Scope.Any())
        {
            foreach (var scopeInformationItem in options.Information.Scope)
            {
                serverInformationMetadata.Scope.Add(scopeInformationItem);
            }
        }
        else
        {
            if (options.ScopePreDefinedDefaultValue is not null && options.ScopePreDefinedDefaultValue.Any())
            {
                string scopePreDefinedDefaultValue = options.ScopePreDefinedDefaultValue.Aggregate((x, y) => $"{x} {y}");

                serverInformationMetadata.Scope.Add("server_scope_default_value", scopePreDefinedDefaultValue);
            }
        }

        if (serverInformationMetadata.AuthorizationCode is null)
        {
            serverInformationMetadata.AuthorizationCode = new ConcurrentDictionary<string, string>();
        }

        if (options.Information?.AuthorizationCode is not null && options.Information.AuthorizationCode.Any())
        {
            foreach (var authorizationCodeInformationItem in options.Information.AuthorizationCode)
            {
                serverInformationMetadata.AuthorizationCode.Add(authorizationCodeInformationItem);
            }
        }
        else
        {
            int serverAuthorizatioCodeSizeSymbols;

            if (options.AuthorizationCodeDefaultSizeSymbols is not null)
            {
                serverAuthorizatioCodeSizeSymbols = options.AuthorizationCodeDefaultSizeSymbols.Value;
            }
            else
            {
                // TODO: a more advanced determination of the authorization code's length.
                serverAuthorizatioCodeSizeSymbols = Guid.NewGuid().ToString("N").Length * 2;
            }

            serverInformationMetadata.AuthorizationCode.Add("server_authorization_code_size_symbols", serverAuthorizatioCodeSizeSymbols.ToString());
        }

        services.AddSingleton(serverInformationMetadata);

        return services;
    }
}
