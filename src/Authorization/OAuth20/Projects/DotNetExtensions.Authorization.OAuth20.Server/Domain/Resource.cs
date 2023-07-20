// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Data;

namespace DotNetExtensions.Authorization.OAuth20.Server.Domain;

public class Resource : EntityBase
{
    public Resource(
        int id,
        Guid guid,
        DateTime createdDateTime,
        string name,
        IEnumerable<Scope>? scopes)
        : base(id, guid, createdDateTime)
    {
        Name = name;
        Scopes = scopes;
    }

    protected Resource()
    {
    }

    public string Name { get; private set; } = default!;

    public IEnumerable<Scope>? Scopes { get; private set; }
}
