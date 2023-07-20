using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Data;

namespace DotNetExtensions.Authorization.OAuth20.Server.Domain;

public class Flow : EntityBase
{
    public Flow(
        int id,
        Guid guid,
        DateTime createdDateTime,
        string name,
        IEnumerable<ClientFlow>? clientFlows)
        : base(id, guid, createdDateTime)
    {
        Name = name;
        ClientFlows = clientFlows;
    }

    protected Flow()
    {
    }

    public string Name { get; private set; } = default!;

    public IEnumerable<ClientFlow>? ClientFlows { get; private set; }
}
