// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataStorages;
using DotNetExtensions.Authorization.OAuth20.Server.Models;
using System.Collections.Concurrent;

namespace DotNetExtensions.Authorization.OAuth20.Server.InMemory.DataStorages;

public class InMemoryAuthorizationCodeStorage : IAuthorizationCodeStorage
{
    public ConcurrentDictionary<string, AuthorizationCodeResult> _items = new();

    public Task AddAuthorizationCodeResultAsync(AuthorizationCodeResult authorizationCode)
    {
        _items.TryAdd(authorizationCode.Value, authorizationCode);

        return Task.CompletedTask;
    }

    public Task<AuthorizationCodeResult?> GetAuthorizationCodeResultAsync(string authorizationCodeValue)
    {
        _items.TryGetValue(authorizationCodeValue, out var authorizationCode);

        return Task.FromResult(authorizationCode);
    }
}
