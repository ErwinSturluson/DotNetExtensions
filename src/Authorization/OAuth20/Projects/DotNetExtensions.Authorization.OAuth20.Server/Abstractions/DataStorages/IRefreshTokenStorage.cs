// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Models;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataStorages;

public interface IRefreshTokenStorage
{
    public Task AddRefreshTokenAsync(RefreshTokenResult token);

    public Task<RefreshTokenResult?> GetRefreshTokenAsync(string token);
}
