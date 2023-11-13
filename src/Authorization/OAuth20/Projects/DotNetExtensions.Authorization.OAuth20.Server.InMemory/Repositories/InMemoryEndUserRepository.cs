// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Reporitories;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.InMemory.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace DotNetExtensions.Authorization.OAuth20.Server.InMemory.Repositories;

internal class InMemoryEndUserRepository : InMemoryInt32IdRepository<EndUser>, IEndUserRepository
{
    public InMemoryEndUserRepository(OAuth20ServerDbContext oAuth20ServerDbContext)
        : base(oAuth20ServerDbContext)
    {
    }

    public async Task<EndUser?> GetByUsernameAsync(string username)
    {
        var endUser = await _oAuth20ServerDbContext.EndUsers.FirstOrDefaultAsync(x => x.Username == username);

        return endUser;
    }
}
