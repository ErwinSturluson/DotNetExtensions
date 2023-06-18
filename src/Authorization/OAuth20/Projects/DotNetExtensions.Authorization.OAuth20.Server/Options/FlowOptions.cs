// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Options;

/// <summary>
/// TODO: more advanced business validation.
/// </summary>
public class FlowOptions
{
    public string Description { get; set; } = default!;

    public string? GrantTypeName { get; set; }

    public string? ResponseTypeName { get; set; }

    public EndpointTypeOptions? Abstraction { get; set; }

    public EndpointTypeOptions? Implementation { get; set; }
}
