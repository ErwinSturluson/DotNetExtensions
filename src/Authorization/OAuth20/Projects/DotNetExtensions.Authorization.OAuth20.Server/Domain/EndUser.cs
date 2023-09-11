// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Data;

namespace DotNetExtensions.Authorization.OAuth20.Server.Domain;

public class EndUser : EntityBase
{
    public EndUser(int id,
        Guid externalId,
        DateTime createdDateTime,
        string username,
        string? passwordHash,
        IEnumerable<EndUserClientScope>? endUserClientScopes)
        : base(id, externalId, createdDateTime)
    {
        Username = username;
        PasswordHash = passwordHash;
        EndUserClientScopes = endUserClientScopes;
    }

    protected EndUser()
    {
    }

    public string Username { get; private set; } = default!;

    public string? PasswordHash { get; private set; }

    public IEnumerable<EndUserClientScope>? EndUserClientScopes { get; private set; }
}
