// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;

namespace DotNetExtensions.Authorization.OAuth20.Server.Options.Entities;

public class FlowEntityOptions
{
    [Required]
    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public virtual string? GrantTypeName { get; set; }

    public virtual string? ResponseTypeName { get; set; }
}
