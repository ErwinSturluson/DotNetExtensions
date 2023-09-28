// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Flows;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;

public interface IFlowService
{
    public Task<Flow?> GetFlowAsync(string name);

    public Task<Flow?> GetFlowAsync<T>(T implementation)
        where T : IFlow;

    public Task<Flow?> GetFlowAsync(Type type);
}
