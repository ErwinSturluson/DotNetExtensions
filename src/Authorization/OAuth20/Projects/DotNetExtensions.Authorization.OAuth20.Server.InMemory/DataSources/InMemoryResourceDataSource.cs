// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataSources;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotNetExtensions.Authorization.OAuth20.Server.InMemory.DataSources;

public class InMemoryResourceDataSource : IResourceDataSource
{
    private readonly OAuth20ServerDbContext _oAuth20ServerDbContext;

    public InMemoryResourceDataSource(OAuth20ServerDbContext oAuth20ServerDbContext)
    {
        _oAuth20ServerDbContext = oAuth20ServerDbContext;
    }

    public async Task<Resource> GetResourceByScopeAsync(Scope scope)
    {
        return await _oAuth20ServerDbContext.Scopes
            .Include(x => x.Resource)
            .Select(x => x.Resource)
            .FirstAsync();
    }

    public async Task<IEnumerable<SigningCredentialsAlgorithm>> GetResourceSigningCredentialsAlgorithmsAsync(Resource resource)
    {
        return await _oAuth20ServerDbContext.ResourceSigningCredentialsAlgorithms
            .Where(x => x.ResourceId == resource.Id)
            .Include(x => x.SigningCredentialsAlgorithm)
            .Select(x => x.SigningCredentialsAlgorithm)
            .ToListAsync();
    }
}
