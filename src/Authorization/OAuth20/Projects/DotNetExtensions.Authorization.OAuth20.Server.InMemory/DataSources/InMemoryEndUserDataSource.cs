// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataSources;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotNetExtensions.Authorization.OAuth20.Server.InMemory.DataSources;

public class InMemoryEndUserDataSource : IEndUserDataSource
{
    private readonly OAuth20ServerDbContext _oAuth20ServerDbContext;

    public InMemoryEndUserDataSource(OAuth20ServerDbContext oAuth20ServerDbContext)
    {
        _oAuth20ServerDbContext = oAuth20ServerDbContext;
    }

    public async Task<EndUser?> GetEndUserAsync(string username)
    {
        return await _oAuth20ServerDbContext.EndUsers
            .Where(x => x.Username == username)
            .Include(x => x.EndUserInfo)
            .FirstOrDefaultAsync();
    }
}
