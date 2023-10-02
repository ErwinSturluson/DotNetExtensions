// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Builders.Generic;

public interface IResultBuilder<TResult> : IResultBuilder
{
    public ValueTask<IResult> BuildResultAsync(TResult result);
}
