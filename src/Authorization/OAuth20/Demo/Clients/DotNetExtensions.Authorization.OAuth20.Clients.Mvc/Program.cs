// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Authentication.Cookies;

namespace DotNetExtensions.Authorization.OAuth20.Clients.Mvc;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services
            .AddAuthentication(config =>
            {
                config.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = "OAuth20";
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddOAuth("OAuth20", options =>
            {
                options.ClientId = "client_id_mvc_oauth20";
                options.ClientSecret = "client_secret_mvc_oauth20";

                options.AuthorizationEndpoint = "https://localhost:50101/auth/authorize";
                options.TokenEndpoint = "https://localhost:50101/auth/token";

                // options.UserInformationEndpoint = "https://localhost:50101/auth/user_info";

                options.Scope.Clear();
                options.Scope.Add("resource1.scope1");
                options.Scope.Add("resource2.scope1");
                options.Scope.Add("resource3.scope1");

                options.SaveTokens = true;

                options.CallbackPath = "/auth/callback";
            });

        builder.Services.AddAuthorization();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
