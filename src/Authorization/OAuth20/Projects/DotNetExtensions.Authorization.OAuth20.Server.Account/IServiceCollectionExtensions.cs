using DotNetExtensions.Authorization.OAuth20.Server.Account.Client.Abstractions;
using DotNetExtensions.Authorization.OAuth20.Server.Account.Default;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace DotNetExtensions.Authorization.OAuth20.Server.Account;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddOAuth20Account(this IServiceCollection services)
    {
        services.AddScoped<IEndUserAuthenticationService, ServerEndUserAuthenticationService>();
        services.AddSingleton<StaticFilesLinkResolver>();

        services.AddControllers();
        services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "oauth20_server";
                options.LoginPath = "/login"; // Specify the login path
                options.LogoutPath = "/logout"; // Specify the logout path
            });

        return services;
    }
}
