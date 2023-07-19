// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Options.Endpoints;
using DotNetExtensions.Authorization.OAuth20.Server.Options.Errors;
using DotNetExtensions.Authorization.OAuth20.Server.Options.Flows;

namespace DotNetExtensions.Authorization.OAuth20.Server.Options;

public class OAuth20ServerOptions
{
    public const string DefaultSection = "OAuth20Server";

    public bool AuthorizationRequestStateRequired { get; set; } = true;

    public bool TokenResponseExpiresInRequired { get; set; } = true;

    public bool ClientCredentialsFlowRefreshTokenAccepted { get; set; } = false;

    public OAuth20ServerEndpointsOptions Endpoints { get; set; } = default!;

    public OAuth20ServerFlowsOptions Flows { get; set; } = default!;

    public OAuth20ServerErrorsOptions Errors { get; set; } = default!;
}
