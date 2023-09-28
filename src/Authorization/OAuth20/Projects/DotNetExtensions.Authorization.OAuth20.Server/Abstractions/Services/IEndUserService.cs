// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;

public interface IEndUserService
{
    public Task<EndUser?> GetCurrentEndUserAsync(string? state = null);

    public Task<EndUser?> GetEndUserAsync(string username);

    public bool IsAuthenticated();
}
