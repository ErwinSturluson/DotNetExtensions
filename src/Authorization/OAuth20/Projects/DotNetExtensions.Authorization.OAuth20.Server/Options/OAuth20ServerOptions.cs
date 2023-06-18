// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Options;

public class OAuth20ServerOptions
{
    public const string DefaultSection = "OAuth20Server";

    public string? AuthorizationEndpointRoute { get; set; }

    public string? TokenEndpointRoute { get; set; }

    public bool AuthorizationEndpointHttpPostEnabled { get; set; } = false;

    public string? AuthorizationCodeFlowResponseTypeName { get; set; }

    public string? ImplicitFlowResponseTypeName { get; set; }

    public string? AuthorizationFlowGrantTypeName { get; set; }

    public string? ClientCredentialsFlowGrantTypeName { get; set; }

    public string? ResourceOwnerPasswordCredentialsFlowGrantTypeName { get; set; }

    public bool AuthorizationRequestStateRequired { get; set; } = true;

    public bool TokenResponseExpiresInRequired { get; set; } = true;

    public bool ClientCredentialsFlowRefreshTokenAccepted { get; set; } = false;

    public IEnumerable<EndpointOptions> Endpoints { get; set; } = default!;

    public IEnumerable<FlowOptions> Flows { get; set; } = default!;
}
