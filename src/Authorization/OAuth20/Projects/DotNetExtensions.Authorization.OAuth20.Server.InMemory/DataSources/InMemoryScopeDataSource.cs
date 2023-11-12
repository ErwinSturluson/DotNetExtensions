// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataSources;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotNetExtensions.Authorization.OAuth20.Server.InMemory.DataSources;

public class InMemoryScopeDataSource : IScopeDataSource
{
    private readonly OAuth20ServerDbContext _oAuth20ServerDbContext;

    public InMemoryScopeDataSource(OAuth20ServerDbContext oAuth20ServerDbContext)
    {
        _oAuth20ServerDbContext = oAuth20ServerDbContext;
    }

    public async Task<Scope?> GetScopeAsync(string name)
    {
        return await _oAuth20ServerDbContext.Scopes.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<IEnumerable<Scope>> GetScopeListAsync(IEnumerable<string> names)
    {
        return await _oAuth20ServerDbContext.Scopes.Where(x => names.Contains(x.Name)).ToListAsync();
    }

    public async Task<IEnumerable<Scope>> GetScopesAsync(EndUser endUser, Client client)
    {
        return await _oAuth20ServerDbContext.EndUserClientScopes
            .Where(x => x.EndUserId == endUser.Id)
            .Include(x => x.ClientScope)
            .Select(x => x.ClientScope)
            .Where(x => x.ClientId == client.Id)
            .Include(x => x.Scope)
            .Select(x => x.Scope)
            .ToListAsync();
    }
}
