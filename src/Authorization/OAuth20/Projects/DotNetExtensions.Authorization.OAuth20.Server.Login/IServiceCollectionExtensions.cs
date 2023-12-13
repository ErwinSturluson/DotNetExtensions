// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Login.Endpoint.Abstractions;
using DotNetExtensions.Authorization.OAuth20.Server.Login.Endpoint.Default;
using DotNetExtensions.Authorization.OAuth20.Server.Login.Pages.Abstractions;
using DotNetExtensions.Authorization.OAuth20.Server.Login.Pages.Default;
using DotNetExtensions.Authorization.OAuth20.Server.ServiceCollections;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace DotNetExtensions.Authorization.OAuth20.Server.Login;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection SetLoginPage(this IServiceCollection services)
    {
        services.SetOAuth20WebPageBuilder<ILoginWebPageBuilder, DefaultLoginWebPageBuilder>("/login");
        services.SetOAuth20WebPageBuilder<ILoginSuccessfulWebPageBuilder, DefaultLoginSuccessfulWebPageBuilder>("/login/successful");
        services.SetOAuth20WebPageBuilder<IPermissionsWebPageBuilder, DefaultPermissionsWebPageBuilder>("/permissions");
        services.SetOAuth20Endpoint<ILoginPasswordEndpoint, DefaultLoginPasswordEndpoint>("/endpoint/login/password");
        services.SetOAuth20Endpoint<IPermissionsEndpoint, DefaultPermissionsEndpoint>("/endpoint/permissions");

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "oauth20_server";
                options.LoginPath = "/Login"; // Specify the login path
                options.LogoutPath = "/Logout"; // Specify the logout path
            });

        return services;
    }
}
