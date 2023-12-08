// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataStorages;
using DotNetExtensions.Authorization.OAuth20.Server.Converters;
using DotNetExtensions.Authorization.OAuth20.Server.Models;
using System.Collections.Concurrent;
using System.Text;

namespace DotNetExtensions.Authorization.OAuth20.Server.InMemory.DataStorages;

internal class InMemoryEndUserClientScopeStorage : IEndUserClientScopeStorage
{
    public static ConcurrentDictionary<string, EndUserClientScopeRequest> _requestItems = new();
    public static ConcurrentDictionary<string, EndUserClientScopeResult> _resultItems = new();

    public Task AddEndUserClientScopeRequestAsync(EndUserClientScopeRequest endUserClientScopeRequest)
    {
        string identifier = GetIdentifier(endUserClientScopeRequest.Username, endUserClientScopeRequest.ClientId);

        _requestItems.TryAdd(identifier, endUserClientScopeRequest);

        return Task.CompletedTask;
    }

    public Task<EndUserClientScopeRequest?> GetEndUserClientScopeRequestAsync(string username, string clientId)
    {
        string identifier = GetIdentifier(username, clientId);

        _requestItems.TryGetValue(identifier, out EndUserClientScopeRequest? requestItem);

        return Task.FromResult(requestItem);
    }

    public Task AddEndUserClientScopeResultAsync(EndUserClientScopeResult endUserClientScopeResult)
    {
        string identifier = GetIdentifier(endUserClientScopeResult.Username, endUserClientScopeResult.ClientId);

        _resultItems.TryAdd(identifier, endUserClientScopeResult);

        return Task.CompletedTask;
    }

    public Task<EndUserClientScopeResult?> GetEndUserClientScopeResultAsync(string username, string clientId)
    {
        string identifier = GetIdentifier(username, clientId);

        _resultItems.TryGetValue(identifier, out EndUserClientScopeResult? resultItem);

        return Task.FromResult(resultItem);
    }

    private string GetIdentifier(string username, string clientId)
    {
        byte[] usernameBytes = Encoding.UTF8.GetBytes(username);
        string usernameEncoded = Base64UrlConverter.Encode(usernameBytes);

        byte[] clientIdBytes = Encoding.UTF8.GetBytes(clientId);
        string clientIdEncoded = Base64UrlConverter.Encode(clientIdBytes);

        string identifier = $"{usernameEncoded}|{clientIdEncoded}";

        return identifier;
    }
}
