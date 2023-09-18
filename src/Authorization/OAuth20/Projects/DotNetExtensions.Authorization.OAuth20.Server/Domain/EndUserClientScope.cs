// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain.Abstractions;

namespace DotNetExtensions.Authorization.OAuth20.Server.Domain;

public class EndUserClientScope : EntityBase<int>
{
    public EndUser EndUser { get; set; } = default!;

    public int EndUserId { get; set; }

    public ClientScope ClientScope { get; set; } = default!;

    public int ClientScopeId { get; set; }
}
