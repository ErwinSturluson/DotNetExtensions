// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Data;

namespace DotNetExtensions.Authorization.OAuth20.Server.Domain;

public class ClientScope : EntityBase
{
    public ClientScope(
        int id,
        Guid guid,
        DateTime createdDateTime,
        Client client,
        Scope scope,
        IEnumerable<EndUserClientScope>? endUserClientScopes)
        : base(id, guid, createdDateTime)
    {
        Client = client;
        ClientId = client.Id;
        Scope = scope;
        ScopeId = scope.Id;
        EndUserClientScopes = endUserClientScopes;
    }

    protected ClientScope()
    {
    }

    public int ClientId { get; private set; }

    public Client Client { get; private set; } = default!;

    public int ScopeId { get; private set; }

    public Scope Scope { get; private set; } = default!;

    public IEnumerable<EndUserClientScope>? EndUserClientScopes { get; private set; }
}
