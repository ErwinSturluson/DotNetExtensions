// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataSources;

namespace DotNetExtensions.Authorization.OAuth20.Server;

public static class IDataSourceServiceCollectionExtensions
{
    public static IServiceCollection AddOAuth20DataSources<
        TClientDataSource,
        TClientSecretDataSource,
        TEndUserDataSource,
        TFlowDataSource,
        TResourceDataSource,
        TScopeDataSource,
        TTokenTypeDataSource>(this IServiceCollection services)
        where TClientDataSource : class, IClientDataSource
        where TClientSecretDataSource : class, IClientSecretDataSource
        where TEndUserDataSource : class, IEndUserDataSource
        where TFlowDataSource : class, IFlowDataSource
        where TResourceDataSource : class, IResourceDataSource
        where TScopeDataSource : class, IScopeDataSource
        where TTokenTypeDataSource : class, ITokenTypeDataSource

    {
        services.AddScoped<IClientDataSource, TClientDataSource>();
        services.AddScoped<IClientSecretDataSource, TClientSecretDataSource>();
        services.AddScoped<IEndUserDataSource, TEndUserDataSource>();
        services.AddScoped<IFlowDataSource, TFlowDataSource>();
        services.AddScoped<IResourceDataSource, TResourceDataSource>();
        services.AddScoped<IScopeDataSource, TScopeDataSource>();
        services.AddScoped<ITokenTypeDataSource, TTokenTypeDataSource>();

        return services;
    }
}
