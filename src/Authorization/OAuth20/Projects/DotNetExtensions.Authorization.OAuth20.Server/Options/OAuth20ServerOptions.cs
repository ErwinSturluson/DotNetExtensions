// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Options.ClientSecrets;
using DotNetExtensions.Authorization.OAuth20.Server.Options.Endpoints;
using DotNetExtensions.Authorization.OAuth20.Server.Options.Entities;
using DotNetExtensions.Authorization.OAuth20.Server.Options.Errors;
using DotNetExtensions.Authorization.OAuth20.Server.Options.Flows;
using DotNetExtensions.Authorization.OAuth20.Server.Options.ServerSigningCredentials;
using DotNetExtensions.Authorization.OAuth20.Server.Options.Tokens;

namespace DotNetExtensions.Authorization.OAuth20.Server.Options;

public class OAuth20ServerOptions
{
    public const string DefaultSection = "OAuth20Server";

    public bool AuthorizationRequestStateRequired { get; set; } = true;

    public bool AuthorizationRequestScopeRequired { get; set; } = false;

    /// <summary>
    /// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-3.1.2.2"/>
    /// </summary>
    public bool RequestRedirectionUriRequired { get; set; } = false;

    /// <summary>
    /// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-3.1.2.2"/>
    /// </summary>
    public bool ClientRegistrationRedirectionEndpointsRequired { get; set; } = true;

    /// <summary>
    /// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-3.1.2.2"/>
    /// </summary>
    public bool ClientMultipleRedirectionEndpointsAllowed { get; set; } = true;

    /// <summary>
    /// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-3.1.2.2"/>
    /// </summary>
    public bool ClientRegistrationCompleteRedirectionEndpointRequired { get; set; } = true;

    /// <summary>
    /// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-3.1.2.4"/>
    /// </summary>
    public bool InformResourceOwnerOfRedirectionUriError { get; set; } = true;

    public bool FullyIgnoreRequestedScopes { get; set; } = false;

    public IEnumerable<string>? SelectivelyIgnoredRequestedScopes { get; set; }

    public IEnumerable<string>? ScopePreDefinedDefaultValue { get; set; }

    public bool InclusionScopeToResponseRequired { get; set; } = true;

    public bool UserScopeAllowanceRequired { get; set; } = true;

    public bool TokenResponseExpiresInRequired { get; set; } = true;

    public bool ClientCredentialsFlowRefreshTokenAccepted { get; set; } = false;

    public string? DefaultLoginEndpoint { get; set; }

    public long? DefaultTokenExpirationSeconds { get; set; } = 3600;

    public long? DefaultAuthorizationCodeExpirationSeconds { get; set; } = 60;

    public OAuth20ServerEndpointsOptions? Endpoints { get; set; }

    public OAuth20ServerFlowsOptions? Flows { get; set; }

    public OAuth20ServerErrorsOptions? Errors { get; set; }

    public OAuth20ServerEntitiesOptions? Entities { get; set; }

    public OAuth20ServerTokensOptions? Tokens { get; set; }

    public OAuth20ServerClientSecretsOptions? ClientSecrets { get; set; }

    public OAuth20ServerSigningCredentialsOptions? ServerSigningCredentials { get; set; }
}
