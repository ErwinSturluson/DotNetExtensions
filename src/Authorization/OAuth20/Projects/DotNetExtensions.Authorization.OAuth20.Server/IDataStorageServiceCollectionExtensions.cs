// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataStorages;

namespace DotNetExtensions.Authorization.OAuth20.Server;

public static class IDataStorageServiceCollectionExtensions
{
    public static IServiceCollection SetOAuth20DataStorages(this IServiceCollection services, IDataStorageContext dataStorageContext)
    {
        services.AddScoped(typeof(IAccessTokenStorage), dataStorageContext.AccessTokenStorageType);
        services.AddScoped(typeof(IAuthorizationCodeStorage), dataStorageContext.AuthorizationCodeStorageType);
        services.AddScoped(typeof(IRefreshTokenStorage), dataStorageContext.RefreshTokenStorageType);

        return services;
    }
}
