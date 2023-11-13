// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain.Abstractions;

namespace DotNetExtensions.Authorization.OAuth20.Server.Storage;

public class AccessTokenScopeSet : EntityBase<int>
{
    public int AccessTokenId { get; set; }

    public AccessToken AccessToken { get; set; } = default!;

    public int ScopeSetId { get; set; }

    public ScopeSet ScopeSet { get; set; } = default!;
}
