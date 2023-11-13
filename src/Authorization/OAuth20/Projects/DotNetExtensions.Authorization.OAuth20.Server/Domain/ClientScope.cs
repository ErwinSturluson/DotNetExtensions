// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain.Abstractions;

namespace DotNetExtensions.Authorization.OAuth20.Server.Domain;

public class ClientScope : Int32IdEntityBase
{
    public int ClientId { get; set; }

    public Client Client { get; set; } = default!;

    public int ScopeId { get; set; }

    public Scope Scope { get; set; } = default!;

    public IEnumerable<EndUserClientScope>? EndUserClientScopes { get; set; }
}
