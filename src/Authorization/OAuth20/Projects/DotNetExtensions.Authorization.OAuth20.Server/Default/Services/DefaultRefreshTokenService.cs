// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataStorages;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Providers;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;
using DotNetExtensions.Authorization.OAuth20.Server.Models;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.Services;

public class DefaultRefreshTokenService : IRefreshTokenService
{
    private readonly IRefreshTokenStorage _refreshTokenStorage;
    private readonly IRefreshTokenProvider _refreshTokenProvider;

    public DefaultRefreshTokenService(IRefreshTokenStorage refreshTokenStorage, IRefreshTokenProvider refreshTokenProvider)
    {
        _refreshTokenStorage = refreshTokenStorage;
        _refreshTokenProvider = refreshTokenProvider;
    }

    public async Task<RefreshTokenResult> GetRefreshTokenAsync(AccessTokenResult accessToken)
    {
        string refreshTokenValue = await _refreshTokenProvider.GetRefreshTokenValueAsync(accessToken);

        RefreshTokenResult refreshToken = new()
        {
            Value = refreshTokenValue,
            AccessTokenValue = accessToken.Value
        };

        await _refreshTokenStorage.AddRefreshTokenAsync(refreshToken);

        return refreshToken;
    }
}
