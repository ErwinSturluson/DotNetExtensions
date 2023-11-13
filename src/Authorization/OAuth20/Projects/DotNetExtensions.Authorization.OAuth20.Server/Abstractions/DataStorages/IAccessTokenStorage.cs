// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Models;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataStorages;

public interface IAccessTokenStorage
{
    public Task AddAccessTokenAsync(AccessTokenResult accessToken);

    public Task<AccessTokenResult?> GetAccessTokenAsync(string accessTokenValue);
}
