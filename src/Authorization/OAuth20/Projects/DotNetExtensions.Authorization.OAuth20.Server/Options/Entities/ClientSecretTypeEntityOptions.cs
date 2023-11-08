// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;

namespace DotNetExtensions.Authorization.OAuth20.Server.Options.Entities;

public class ClientSecretTypeEntityOptions
{
    [Required]
    public string Name { get; set; } = default!;

    public string? Description { get; set; }
}
