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

    public string? RefreshTokenFlowGrantTypeName { get; set; }

    public bool AuthorizationRequestStateRequired { get; set; } = true;

    public bool TokenResponseExpiresInRequired { get; set; } = true;

    public bool ClientCredentialsFlowRefreshTokenAccepted { get; set; } = false;

    public string? TokenInvalidRequestErrorCode { get; set; }

    public string? TokenInvalidClientErrorCode { get; set; }

    public string? TokenInvalidGrantErrorCode { get; set; }

    public string? TokenUnauthorizedClientErrorCode { get; set; }

    public string? TokenUnsupportedGrantTypeErrorCode { get; set; }

    public string? TokenInvalidScopeErrorCode { get; set; }

    public string? AuthorizeInvalidRequestErrorCode { get; set; }

    public string? AuthorizeUnauthorizedClientErrorCode { get; set; }

    public string? AuthorizeAccessDeniedErrorCode { get; set; }

    public string? AuthorizeUnsupportedResponseTypeErrorCode { get; set; }

    public string? AuthorizeInvalidScopeErrorCode { get; set; }

    public string? AuthorizeServerErrorErrorCode { get; set; }

    public string? AuthorizeTemporarilyUnavailableErrorCode { get; set; }

    public IEnumerable<EndpointOptions> Endpoints { get; set; } = default!;

    public IEnumerable<FlowOptions> Flows { get; set; } = default!;

    public IEnumerable<ErrorOptions> AuthorizeErrors { get; set; } = default!;

    public IEnumerable<ErrorOptions> TokenErrors { get; set; } = default!;
}
