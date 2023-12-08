// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Models;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataStorages;

public interface IEndUserClientScopeStorage
{
    public Task AddEndUserClientScopeRequestAsync(EndUserClientScopeRequest endUserClientScopeRequest);

    public Task<EndUserClientScopeRequest?> GetEndUserClientScopeRequestAsync(string username, string clientId);

    public Task AddEndUserClientScopeResultAsync(EndUserClientScopeResult endUserClientScopeResult);

    public Task<EndUserClientScopeResult?> GetEndUserClientScopeResultAsync(string username, string clientId);
}
