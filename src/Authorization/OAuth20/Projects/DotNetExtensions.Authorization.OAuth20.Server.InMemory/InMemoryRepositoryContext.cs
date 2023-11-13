// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Reporitories;
using DotNetExtensions.Authorization.OAuth20.Server.InMemory.Repositories;
using DotNetExtensions.Authorization.OAuth20.Server.InMemory.Repositories.Common;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetExtensions.Authorization.OAuth20.Server.InMemory;

public class InMemoryRepositoryContext : IRepositoryContext
{
    public void SetRepositories(IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<,>), typeof(InMemoryRepository<,>));
        services.AddScoped(typeof(INamedRepository<,>), typeof(InMemoryNamedRepository<,>));
        services.AddScoped(typeof(IInt32IdRepository<>), typeof(InMemoryInt32IdRepository<>));
        services.AddScoped(typeof(IInt32IdNamedRepository<>), typeof(InMemoryInt32IdNamedRepository<>));
        services.AddScoped<IClientRepository, InMemoryClientRepository>();
        services.AddScoped<IEndUserRepository, InMemoryEndUserRepository>();
        services.AddScoped<IClientProfileRepository, InMemoryClientProfileRepository>();
        services.AddScoped<IClientTypeRepository, InMemoryClientTypeRepository>();
    }
}
