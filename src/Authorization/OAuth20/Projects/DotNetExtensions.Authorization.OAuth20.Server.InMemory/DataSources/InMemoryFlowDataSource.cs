// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataSources;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotNetExtensions.Authorization.OAuth20.Server.InMemory.DataSources;

public class InMemoryFlowDataSource : IFlowDataSource
{
    private readonly OAuth20ServerDbContext _oAuth20ServerDbContext;

    public InMemoryFlowDataSource(OAuth20ServerDbContext oAuth20ServerDbContext)
    {
        _oAuth20ServerDbContext = oAuth20ServerDbContext;
    }

    public async Task<Flow?> GetFlowAsync(string name)
    {
        return await _oAuth20ServerDbContext.Flows
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name);
    }
}
