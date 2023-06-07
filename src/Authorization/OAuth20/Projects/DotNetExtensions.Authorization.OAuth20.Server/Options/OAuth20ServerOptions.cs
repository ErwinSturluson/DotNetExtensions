// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Options;

public class OAuth20ServerOptions
{
    public const string DefaultSection = "OAuth20Server";

    public string? AuthorizationEndpointRoute { get; set; }

    public string? TokenEndpointRoute { get; set; }

    public IEnumerable<EndpointOptions> Endpoints { get; set; } = default!;
}
