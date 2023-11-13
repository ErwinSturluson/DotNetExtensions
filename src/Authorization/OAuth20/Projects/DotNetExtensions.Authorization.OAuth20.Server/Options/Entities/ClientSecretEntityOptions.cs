// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;

namespace DotNetExtensions.Authorization.OAuth20.Server.Options.Entities;

public class ClientSecretEntityOptions
{
    [Required]
    public string Content { get; set; } = default!;

    public string? Title { get; set; }

    public string? Desription { get; set; }

    [Required]
    public string ClientSecretType { get; set; } = default!;
}
