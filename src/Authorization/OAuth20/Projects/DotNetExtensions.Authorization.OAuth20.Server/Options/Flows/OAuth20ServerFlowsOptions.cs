// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Options.Flows;

public class OAuth20ServerFlowsOptions
{
    public string? AuthorizationCodeFlowResponseTypeName { get; set; }

    public string? ImplicitFlowResponseTypeName { get; set; }

    public string? AuthorizationFlowGrantTypeName { get; set; }

    public string? ClientCredentialsFlowGrantTypeName { get; set; }

    public string? ResourceOwnerPasswordCredentialsFlowGrantTypeName { get; set; }

    public string? RefreshTokenFlowGrantTypeName { get; set; }

    public IEnumerable<FlowOptions> FlowList { get; set; } = default!;
}
