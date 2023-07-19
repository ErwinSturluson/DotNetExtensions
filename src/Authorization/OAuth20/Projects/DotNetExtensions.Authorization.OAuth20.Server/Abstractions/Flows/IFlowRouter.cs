// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Flows;

public interface IFlowRouter
{
    public bool TryGetAuthorizeFlow(string responseType, out IAuthorizeFlow? authorizeFlow);

    public bool TryGetTokenFlow(string grantType, out ITokenFlow? tokenFlow);
}
