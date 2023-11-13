// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataSources;

public interface IEndUserDataSource
{
    public Task<EndUser?> GetEndUserAsync(string username);

    public Task<EndUser?> GetEndUserAsync(string username, string passwordHash);
}
