// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.ClientSecretReaders;

public class ClientSecretReaderMetadata
{
    protected ClientSecretReaderMetadata(string clientSecretType, Type abstraction, string? description = null)
    {
        ClientSecretType = clientSecretType;
        Abstraction = abstraction;
        Description = description;
    }

    public virtual string ClientSecretType { get; set; } = default!;

    public virtual Type Abstraction { get; set; } = default!;

    public virtual string? Description { get; set; }

    public static ClientSecretReaderMetadata Create<TAbstraction>(string clientSecretType, string? description = null)
        where TAbstraction : IClientSecretReaderSelector
        => Create(clientSecretType, typeof(TAbstraction), description);

    public static ClientSecretReaderMetadata Create(string clientSecretType, Type abstraction, string? description = null)
    {
        if (!abstraction.IsAssignableTo(typeof(IClientSecretReaderSelector)))
        {
            throw new ArgumentException(nameof(abstraction));
        }

        return new(clientSecretType, abstraction, description);
    }
}
