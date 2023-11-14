// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain.Abstractions;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Reporitories.Common;

public interface IInt32IdRepository<TEntity> : IRepository<TEntity, int>
    where TEntity : EntityBase<int>, new()
{
}
