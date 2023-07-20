using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Data;

namespace DotNetExtensions.Authorization.OAuth20.Server.Domain;

public class ClientType : EntityBase
{
    public ClientType(
        int id,
        Guid guid,
        DateTime createdDateTime,
        string name,
        IEnumerable<Client>? clients)
        : base(id, guid, createdDateTime)
    {
        Name = name;
        Clients = clients;
    }

    protected ClientType()
    {
    }

    public string Name { get; private set; } = default!;

    public IEnumerable<Client>? Clients { get; private set; }
}
