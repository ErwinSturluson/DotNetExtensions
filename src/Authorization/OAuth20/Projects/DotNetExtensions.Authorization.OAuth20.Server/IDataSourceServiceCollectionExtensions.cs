// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataSources;

namespace DotNetExtensions.Authorization.OAuth20.Server;

public static class IDataSourceServiceCollectionExtensions
{
    public static IServiceCollection SetOAuth20DataSources(this IServiceCollection services, IDataSourceContext dataSourceContext)
    {
        services.AddScoped(typeof(IClientDataSource), dataSourceContext.ClientDataSource.GetType());
        services.AddScoped(typeof(IClientSecretDataSource), dataSourceContext.ClientSecretDataSource.GetType());
        services.AddScoped(typeof(IEndUserDataSource), dataSourceContext.EndUserDataSource.GetType());
        services.AddScoped(typeof(IFlowDataSource), dataSourceContext.FlowDataSource.GetType());
        services.AddScoped(typeof(IResourceDataSource), dataSourceContext.ResourceDataSource.GetType());
        services.AddScoped(typeof(IScopeDataSource), dataSourceContext.ScopeDataSource.GetType());
        services.AddScoped(typeof(ITokenTypeDataSource), dataSourceContext.TokenTypeDataSource.GetType());

        return services;
    }
}
