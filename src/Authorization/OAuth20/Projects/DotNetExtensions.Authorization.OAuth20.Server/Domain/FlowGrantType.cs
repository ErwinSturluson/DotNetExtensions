// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain.Abstractions;

namespace DotNetExtensions.Authorization.OAuth20.Server.Domain;

public class FlowGrantType : EntityBase<int>
{
    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    public IEnumerable<Flow>? Flows { get; set; }
}
