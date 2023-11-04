// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Models;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Providers;

public interface IRefreshTokenProvider
{
    public Task<string> GetRefreshTokenValueAsync(AccessTokenResult accessToken);
}
