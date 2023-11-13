// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataSources;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.Services;

public class DefaultResourceService : IResourceService
{
    private readonly IResourceDataSource _resourceDataSource;

    public DefaultResourceService(IResourceDataSource resourceDataSource)
    {
        _resourceDataSource = resourceDataSource;
    }

    public async Task<Resource> GetResourceByScopeAsync(Scope scope)
    {
        return await _resourceDataSource.GetResourceByScopeAsync(scope);
    }

    public async Task<IEnumerable<Resource>> GetResourcesByScopesAsync(IEnumerable<Scope> scopes)
    {
        Dictionary<int, Resource> resources = new();

        foreach (var scope in scopes)
        {
            if (!resources.TryGetValue(scope.ResourceId, out var _))
            {
                resources[scope.ResourceId] = await _resourceDataSource.GetResourceByScopeAsync(scope);
            }
        }

        return resources.Select(x => x.Value);
    }

    public async Task<IEnumerable<SigningCredentialsAlgorithm>> GetResourceSigningCredentialsAlgorithmsAsync(Resource resource)
    {
        return await _resourceDataSource.GetResourceSigningCredentialsAlgorithmsAsync(resource);
    }
}
