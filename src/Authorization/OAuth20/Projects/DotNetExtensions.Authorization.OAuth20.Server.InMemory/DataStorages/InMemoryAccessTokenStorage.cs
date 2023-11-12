// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataStorages;
using DotNetExtensions.Authorization.OAuth20.Server.Models;
using System.Collections.Concurrent;

namespace DotNetExtensions.Authorization.OAuth20.Server.InMemory.DataStorages;

public class InMemoryAccessTokenStorage : IAccessTokenStorage
{
    public ConcurrentDictionary<string, AccessTokenResult> _items = new();

    public Task AddAccessTokenAsync(AccessTokenResult accessToken)
    {
        _items.TryAdd(accessToken.Value, accessToken);

        return Task.CompletedTask;
    }

    public Task<AccessTokenResult?> GetAccessTokenAsync(string accessTokenValue)
    {
        _items.TryGetValue(accessTokenValue, out var accessToken);

        return Task.FromResult(accessToken);
    }
}
