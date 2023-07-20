// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Data;
using System;

namespace DotNetExtensions.Authorization.OAuth20.Server.Domain;

public class EndUserClientScope : EntityBase
{
    public EndUserClientScope(int id,
        Guid guid,
        DateTime createdDateTime,
        EndUser endUser,
        ClientScope clientScope)
        : base(id, guid, createdDateTime)
    {
        EndUser = endUser;
        EndUserId = endUser.Id;
        ClientScope = clientScope;
        ClientScopeId = clientScope.Id;
    }

    protected EndUserClientScope()
    {
    }

    public EndUser EndUser { get; private set; } = default!;

    public int EndUserId { get; private set; }

    public ClientScope ClientScope { get; private set; } = default!;

    public int ClientScopeId { get; private set; }
}
