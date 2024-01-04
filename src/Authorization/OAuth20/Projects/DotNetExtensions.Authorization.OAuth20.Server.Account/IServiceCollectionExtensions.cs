namespace DotNetExtensions.Authorization.OAuth20.Server.Account;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddOAuth20Account(this IServiceCollection services)
    {
        services.AddSingleton<StaticFilesLinkResolver>();

        services.AddControllers();
        services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();

        return services;
    }
}
