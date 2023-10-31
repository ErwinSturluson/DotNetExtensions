// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataSources;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.Services;

public class DefaultSigningCredentialsAlgorithmsService : ISigningCredentialsAlgorithmsService
{
    private readonly IResourceDataSource _resourceDataSource;

    public DefaultSigningCredentialsAlgorithmsService(IResourceDataSource resourceDataSource)
    {
        _resourceDataSource = resourceDataSource;
    }

    public async Task<IEnumerable<SigningCredentialsAlgorithm>> GetSigningCredentialsAlgorithmsForScopesAsync(IEnumerable<Scope> scopes)
    {
        Dictionary<int, Resource> resources = new();

        foreach (var scope in scopes)
        {
            if (!resources.TryGetValue(scope.ResourceId, out var _))
            {
                resources[scope.ResourceId] = await _resourceDataSource.GetResourceByScopeAsync(scope);
            }
        }

        IEnumerable<SigningCredentialsAlgorithm> signingCredentialsAlgorithms = Enumerable.Empty<SigningCredentialsAlgorithm>();

        foreach (var resource in resources.Values)
        {
            IEnumerable<SigningCredentialsAlgorithm> resourceSigningCredentialsAlgorithms = await _resourceDataSource.GetResourceSigningCredentialsAlgorithmsAsync(resource);

            if (!resourceSigningCredentialsAlgorithms.Any())
            {
                continue;
            }

            signingCredentialsAlgorithms = signingCredentialsAlgorithms.IntersectBy(resourceSigningCredentialsAlgorithms.Select(x => x.Id), x => x.Id);

            if (!signingCredentialsAlgorithms.Any())
            {
                // TODO: formatted exception
                throw new Exception();
            }
        }

        return signingCredentialsAlgorithms;
    }
}
