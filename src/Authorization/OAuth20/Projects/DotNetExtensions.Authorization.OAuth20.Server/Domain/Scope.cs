// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Data;

namespace DotNetExtensions.Authorization.OAuth20.Server.Domain;

public class Scope : EntityBase
{
    public Scope(
        int id,
        Guid guid,
        DateTime createdDateTime,
        string name,
        Resource resource,
        IEnumerable<ClientScope>? clientScopes)
        : base(id, guid, createdDateTime)
    {
        Name = name;
        ResourceId = resource.Id;
        Resource = resource;
        ClientScopes = clientScopes;
    }

    protected Scope()
    {
    }

    public string Name { get; private set; } = default!;

    public int ResourceId { get; private set; }

    public Resource Resource { get; private set; } = default!;

    public IEnumerable<ClientScope>? ClientScopes { get; private set; }
}
