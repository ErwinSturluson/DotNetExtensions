// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;

namespace DotNetExtensions.Authorization.OAuth20.Server.Options;

/// <summary>
/// TODO: more advanced business validation.
/// </summary>
public class EndpointOptions
{
    public string Description { get; set; } = default!;

    [Required]
    public string Route { get; set; } = default!;

    public EndpointTypeOptions? Abstraction { get; set; }

    public EndpointTypeOptions? Implementation { get; set; }
}
