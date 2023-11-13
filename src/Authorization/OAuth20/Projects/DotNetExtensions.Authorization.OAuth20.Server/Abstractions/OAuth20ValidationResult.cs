// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions;

public class OAuth20ValidationResult
{
    public bool Success { get; set; }

    public string? Status { get; set; }

    public string? Description { get; set; }
}
