// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Generic;

public interface IEndpointStrategyArgumentsBuilder<TResult> : IEndpointStrategyArgumentsBuilder
{
    public Task<TResult> GetArgumentsAsync();
}
