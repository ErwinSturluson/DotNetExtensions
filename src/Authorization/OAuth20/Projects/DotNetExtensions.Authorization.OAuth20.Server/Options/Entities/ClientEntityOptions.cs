// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;

namespace DotNetExtensions.Authorization.OAuth20.Server.Options.Entities;

public class ClientEntityOptions : EntityOptionsBase
{
    [Required]
    public string ClientId { get; set; } = default!;

    public string? ClientSecret { get; set; }

    [Required]
    public string ClientType { get; set; } = default!;

    [Required]
    public string ClientProfile { get; set; } = default!;

    public string[]? RedirectionEndpoints { get; set; }

    public string? LoginPageEndpoint { get; set; }

    public string[]? Scopes { get; set; }

    public string[]? Flows { get; set; }
}
