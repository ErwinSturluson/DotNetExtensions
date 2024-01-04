namespace DotNetExtensions.Authorization.OAuth20.Server.Account;

public static class IApplicationBuilderExtensions
{
    public static IApplicationBuilder UseOAuth20AccountDebugging(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }

        return app;
    }

    public static IApplicationBuilder UseOAuth20AccountExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
        }

        return app;
    }

    public static IApplicationBuilder UseOAuth20AccountHttps(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        return app;
    }
}
