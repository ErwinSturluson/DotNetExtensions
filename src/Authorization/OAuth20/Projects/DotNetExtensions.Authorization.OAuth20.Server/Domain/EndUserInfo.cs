using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Data;

namespace DotNetExtensions.Authorization.OAuth20.Server.Domain;

public class EndUserInfo : EntityBase
{
    public EndUserInfo(int id,
        Guid guid,
        DateTime createdDateTime,
        EndUser endUser,
        string? description)
        : base(id, guid, createdDateTime)
    {
        EndUser = endUser;
        Description = description;
    }

    protected EndUserInfo()
    {
    }

    public int EndUserId { get; private set; }

    public EndUser EndUser { get; private set; } = default!;

    public string? Description { get; private set; }
}
