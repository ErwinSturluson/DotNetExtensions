// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;

namespace DotNetExtensions.Authorization.OAuth20.Server.Options.Errors;

public class ErrorOptions
{
    [Required]
    public string Code { get; set; } = default!;

    public string? Description { get; set; }

    public string? Uri { get; set; }
}
