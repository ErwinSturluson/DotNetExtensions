// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Options;

public class ErrorOptions
{
    public string Code { get; set; } = default!;

    public string? Description { get; set; }

    public string? Uri { get; set; }
}
