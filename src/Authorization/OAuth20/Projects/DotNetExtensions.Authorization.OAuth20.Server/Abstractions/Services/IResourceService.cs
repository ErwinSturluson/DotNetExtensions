// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;

public interface IResourceService
{
    public Task<Resource> GetResourceByScopeAsync(Scope scope);

    public Task<IEnumerable<Resource>> GetResourcesByScopesAsync(IEnumerable<Scope> scopes);

    public Task<IEnumerable<SigningCredentialsAlgorithm>> GetResourceSigningCredentialsAlgorithmsAsync(Resource resource);
}
