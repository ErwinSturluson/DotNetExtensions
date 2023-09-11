// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Data;

namespace DotNetExtensions.Authorization.OAuth20.Server.Domain;

public class EndUserInfo : EntityBase
{
    public EndUserInfo(
        int id,
        Guid externalId,
        DateTime createdDateTime,
        EndUser endUser,
        string? description)
        : base(id, externalId, createdDateTime)
    {
        EndUser = endUser;
        EndUserId = endUser.Id;
        Description = description;
    }

    protected EndUserInfo()
    {
    }

    public int EndUserId { get; private set; }

    public EndUser EndUser { get; private set; } = default!;

    public string? Description { get; private set; }
}
