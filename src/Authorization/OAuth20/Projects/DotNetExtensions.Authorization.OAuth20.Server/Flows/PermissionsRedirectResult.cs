// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Default.Endpoints;

namespace DotNetExtensions.Authorization.OAuth20.Server.Flows;

public class PermissionsRedirectResult : DefaultRedirectResult
{
    public PermissionsRedirectResult(string permissionsEndpoint)
        : base(permissionsEndpoint)
    {
    }

    public PermissionsRedirectResult(string permissionsEndpoint, FlowArguments flowArguments, IDictionary<string, string>? additionalParameters = null)
        : base(permissionsEndpoint)
    {
        QueryParameters = flowArguments.Values;

        if (additionalParameters is not null && additionalParameters.Any())
        {
            QueryParameters = QueryParameters.Concat(additionalParameters).ToDictionary();
        }

        QueryParameters.Add("oauth20_server_redirect", "/oauth/authorize");
    }
}
