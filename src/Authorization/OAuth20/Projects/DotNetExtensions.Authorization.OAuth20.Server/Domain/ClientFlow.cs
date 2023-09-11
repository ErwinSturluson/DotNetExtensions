// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Data;

namespace DotNetExtensions.Authorization.OAuth20.Server.Domain;

public class ClientFlow : EntityBase
{
    public ClientFlow(
        int id,
        Guid externalId,
        DateTime createdDateTime,
        Client client,
        Flow flow)
        : base(id, externalId, createdDateTime)
    {
        Client = client;
        ClientId = client.Id;
        Flow = flow;
        FlowId = flow.Id;
    }

    protected ClientFlow()
    {
    }

    public int ClientId { get; private set; }

    public Client Client { get; private set; } = default!;

    public int FlowId { get; private set; }

    public Flow Flow { get; private set; } = default!;
}
