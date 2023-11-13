// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain.Abstractions;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Reporitories;

public interface INamedRepository<TEntity, TIdentifier> : IRepository<TEntity, TIdentifier>
    where TEntity : EntityBase<TIdentifier>, INamedEntity, new()
{
    public Task<TEntity?> GetByNameAsync(string name);
}
