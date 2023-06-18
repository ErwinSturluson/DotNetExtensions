// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Flows;

public interface IFlowRouter
{
    public bool TryGetAuthorizeFlow(string responseTypeName, out IAuthorizeFlow? authorizeFlow);

    public bool TryGetTokenFlow(string grantTypeName, out ITokenFlow? tokenFlow);
}
