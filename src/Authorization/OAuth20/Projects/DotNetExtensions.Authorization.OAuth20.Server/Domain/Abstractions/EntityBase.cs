// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Domain.Abstractions;

public abstract class EntityBase<TKey>
{
    protected EntityBase()
    {
    }

    public TKey Id { get; set; } = default!;
}
