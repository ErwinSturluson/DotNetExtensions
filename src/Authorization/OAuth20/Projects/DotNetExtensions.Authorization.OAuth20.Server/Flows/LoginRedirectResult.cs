// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Default.Endpoints;

namespace DotNetExtensions.Authorization.OAuth20.Server.Flows;

public class LoginRedirectResult : DefaultRedirectResult
{
    public LoginRedirectResult(string loginEndpoint)
        : base(loginEndpoint)
    {
    }

    public LoginRedirectResult(string loginEndpoint, FlowArguments flowArguments, IDictionary<string, string>? additionalParameters = null)
        : base(loginEndpoint)
    {
        QueryParameters = flowArguments.Values;

        if (additionalParameters is not null && additionalParameters.Any())
        {
            QueryParameters = QueryParameters.Concat(additionalParameters).ToDictionary();
        }

        QueryParameters["oauth20_server_redirect"] = "/oauth/authorize";
    }
}
