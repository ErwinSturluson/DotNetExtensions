// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain.Abstractions;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Reporitories;

public interface IInt32IdNamedRepository<TEntity> : IInt32IdRepository<TEntity>, INamedRepository<TEntity, int>
    where TEntity : Int32IdEntityBase, INamedEntity, new()
{
}
