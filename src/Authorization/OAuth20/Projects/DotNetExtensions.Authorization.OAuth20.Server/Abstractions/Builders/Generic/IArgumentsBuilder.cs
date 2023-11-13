// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Builders.Generic;

public interface IArgumentsBuilder<TResult> : IArgumentsBuilder
{
    public ValueTask<TResult> BuildArgumentsAsync(HttpContext httpContext);
}
