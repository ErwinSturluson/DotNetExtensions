// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataStorages;
using DotNetExtensions.Authorization.OAuth20.Server.InMemory.DataStorages;

namespace DotNetExtensions.Authorization.OAuth20.Server.InMemory;

public class InMemoryDataStorageContext : IDataStorageContext
{
    public Type AccessTokenStorageType { get; set; } = typeof(InMemoryAccessTokenStorage);

    public Type AuthorizationCodeStorageType { get; set; } = typeof(InMemoryAuthorizationCodeStorage);

    public Type RefreshTokenStorageType { get; set; } = typeof(InMemoryRefreshTokenStorage);

    public Type EndUserClientScopeStorageType { get; set; } = typeof(InMemoryEndUserClientScopeStorage);
}
