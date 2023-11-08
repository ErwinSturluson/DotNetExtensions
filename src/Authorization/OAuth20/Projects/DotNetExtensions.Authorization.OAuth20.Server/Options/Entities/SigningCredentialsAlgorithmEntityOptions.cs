// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Options.Entities;

public class SigningCredentialsAlgorithmEntityOptions
{
    public string Name { get; set; } = default!;

    public string? Description { get; set; }
}
