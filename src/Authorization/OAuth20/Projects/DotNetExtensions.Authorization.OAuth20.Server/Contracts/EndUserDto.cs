// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Contracts;

public class EndUserDto
{
    public string Username { get; set; } = default!;

    public string? Description { get; set; }
}
