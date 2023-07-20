// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Data;

public abstract class EntityBase
{
    protected EntityBase(int id, Guid externalId, DateTime createdDateTime)
    {
        Id = id;
        ExternalId = externalId;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = createdDateTime;
    }

    private EntityBase()
    {
    }

    public int Id { get; private set; }

    public Guid ExternalId { get; private set; }

    public DateTime CreatedDateTime { get; private set; }

    public DateTime UpdatedDateTime { get; private set; }
}
