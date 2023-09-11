// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Contracts;

public class ResourceDto
{
    public string Name { get; set; } = default!;

    public string[]? Scopes { get; set; }
}
