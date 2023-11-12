// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Reporitories;

public interface IEndUserRepository : IInt32IdRepository<EndUser>
{
    public Task<EndUser?> GetByUsernameAsync(string username);
}
