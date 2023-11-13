// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain.Abstractions;

namespace DotNetExtensions.Authorization.OAuth20.Server.Storage;

public class ScopeSet : EntityBase<int>
{
    public string Value { get; set; } = default!;

    public IEnumerable<AccessTokenScopeSet>? AccessTokenScopeSets { get; set; }

    public IEnumerable<ScopeSetScope>? ScopeSetScopes { get; set; }
}
