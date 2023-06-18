// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Data;

public interface IEntity
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public DateTime UpdatedDateTime { get; set; }
}
