// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetExtensions.Authorization.OAuth20.Server.InMemory;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddOAuth20ServerInMemory(this IServiceCollection services)
    {
        services.AddDbContext<OAuth20ServerDbContext>(options => options.UseInMemoryDatabase(databaseName: "oauth20_server"));

        return services;
    }
}
