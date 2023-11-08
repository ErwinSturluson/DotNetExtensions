// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Options.Entities;

public class ClientSecretEntityOptions
{
    public string Content { get; set; } = default!;

    public string? Title { get; set; }

    public string? Desription { get; set; }

    public ClientSecretTypeEntityOptions ClientSecretType { get; set; } = default!;
}
