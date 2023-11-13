// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataStorages;
using DotNetExtensions.Authorization.OAuth20.Server.Models;
using System.Collections.Concurrent;

namespace DotNetExtensions.Authorization.OAuth20.Server.InMemory.DataStorages;

public class InMemoryRefreshTokenStorage : IRefreshTokenStorage
{
    public static ConcurrentDictionary<string, RefreshTokenResult> _items = new();

    public Task AddRefreshTokenAsync(RefreshTokenResult refreshToken)
    {
        _items.TryAdd(refreshToken.Value, refreshToken);

        return Task.CompletedTask;
    }

    public Task<RefreshTokenResult?> GetRefreshTokenAsync(string refreshTokenValue)
    {
        _items.TryGetValue(refreshTokenValue, out var refreshToken);

        return Task.FromResult(refreshToken);
    }
}
