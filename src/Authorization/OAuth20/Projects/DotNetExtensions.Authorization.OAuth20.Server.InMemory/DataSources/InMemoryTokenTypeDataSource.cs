// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataSources;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotNetExtensions.Authorization.OAuth20.Server.InMemory.DataSources;

public class InMemoryTokenTypeDataSource : ITokenTypeDataSource
{
    private readonly OAuth20ServerDbContext _oAuth20ServerDbContext;

    public InMemoryTokenTypeDataSource(OAuth20ServerDbContext oAuth20ServerDbContext)
    {
        _oAuth20ServerDbContext = oAuth20ServerDbContext;
    }

    public async Task<IEnumerable<TokenAdditionalParameter>> GetTokenAdditionalParametersAsync(TokenType tokenType)
    {
        return await _oAuth20ServerDbContext.TokenTypeTokenAdditionalParameters
            .Where(x => x.TokenTypeId == tokenType.Id)
            .Include(x => x.TokenAdditionalParameter)
            .Select(x => x.TokenAdditionalParameter)
            .ToListAsync();
    }

    public async Task<TokenType?> GetTokenTypeAsync(string name)
    {
        return await _oAuth20ServerDbContext.TokenTypes.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<TokenType?> GetTokenTypeAsync(Client client)
    {
        return await _oAuth20ServerDbContext.TokenTypes.FirstOrDefaultAsync(x => x.Id == client.TokenTypeId);
    }
}
