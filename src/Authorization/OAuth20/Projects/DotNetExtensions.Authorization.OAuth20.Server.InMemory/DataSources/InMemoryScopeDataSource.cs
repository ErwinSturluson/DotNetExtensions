// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataSources;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

    public async Task<IEnumerable<Scope>> GetScopesAsync(IEnumerable<string> names)
    {
        return await _oAuth20ServerDbContext.Scopes
            .Where(x => names.Contains(x.Name))
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Scope>> GetScopesAsync(Client client)
    {
        return await _oAuth20ServerDbContext.ClientScopes
            .Where(x => x.ClientId == client.Id)
            .Include(x => x.Scope)
            .Select(x => x.Scope)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> AllScopesValidAsync(IEnumerable<string> scopes)
    {
        var invalidScopes = await GetInvalidScopesAsync(scopes);

        bool allScopesValid = !invalidScopes.Any();

        return allScopesValid;
    }

    public async Task<IEnumerable<string>> GetInvalidScopesAsync(IEnumerable<string> scopes)
    {
        IEnumerable<string> validScopes = await _oAuth20ServerDbContext.Scopes
            .Where(x => scopes.Contains(x.Name))
            .Select(x => x.Name)
            .AsNoTracking()
            .ToListAsync();

        IEnumerable<string> invalidScopes = scopes.Except(validScopes);

        return invalidScopes;
    }
}
