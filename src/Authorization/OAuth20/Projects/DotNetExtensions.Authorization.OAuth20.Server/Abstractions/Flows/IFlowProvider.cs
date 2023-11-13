// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Flows;

public interface IFlowProvider
{
    public bool TryGetFlowInstanceByGrantTypeName(string grantType, out IFlow? flow);

    public bool TryGetFlowInstanceByResponseTypeName(string responseType, out IFlow? flow);
}
