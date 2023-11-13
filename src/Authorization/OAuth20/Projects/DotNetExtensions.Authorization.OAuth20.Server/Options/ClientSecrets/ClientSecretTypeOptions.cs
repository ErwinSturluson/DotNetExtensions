// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Options.ClientSecrets;

public class ClientSecretTypeOptions
{
    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public ClientSecretReaderOptions? Reader { get; set; }
}
