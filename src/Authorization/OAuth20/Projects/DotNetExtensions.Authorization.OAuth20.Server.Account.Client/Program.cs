using DotNetExtensions.Authorization.OAuth20.Server.Account.Client.Abstractions;
using DotNetExtensions.Authorization.OAuth20.Server.Account.Client.Default;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IEndUserAuthenticationService, WasmEndUserAuthenticationService>();

await builder.Build().RunAsync();
