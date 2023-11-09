// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain.Abstractions;

namespace DotNetExtensions.Authorization.OAuth20.Server.Domain;

public class Flow : EntityBase<int>
{
    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public virtual string? GrantTypeName { get; set; }

    public virtual string? ResponseTypeName { get; set; }

    public IEnumerable<ClientFlow>? ClientFlows { get; set; }
}
