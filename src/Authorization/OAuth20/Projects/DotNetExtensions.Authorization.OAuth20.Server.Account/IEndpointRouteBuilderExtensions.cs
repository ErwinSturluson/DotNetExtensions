using DotNetExtensions.Authorization.OAuth20.Server.Account.Client;
using DotNetExtensions.Authorization.OAuth20.Server.Account.Components;

namespace DotNetExtensions.Authorization.OAuth20.Server.Account;

public static class IEndpointRouteBuilderExtensions
{
    public static IApplicationBuilder MapOAuth20AccountComponents<TApp>(this TApp app, bool useAntiforgery = true)
        where TApp : IApplicationBuilder, IEndpointRouteBuilder
    {
        app.UseStaticFiles();

        if (useAntiforgery) app.UseAntiforgery();

        app.MapControllers();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(OAuth20AccountClientMarker).Assembly);

        return app;
    }
}
