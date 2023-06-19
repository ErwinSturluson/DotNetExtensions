// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Flows;

public abstract class TokenArgumentsBase
{
    protected TokenArgumentsBase(string grantType,
        string clientId,
        string? clientSecret = null,
        string? redirectUri = null,
        string? scope = null)
    {
        GrantType = grantType;
        ClientId = clientId;
        ClientSecret = clientSecret;
        RedirectUri = redirectUri;
        Scope = scope;
    }

    public string GrantType { get; set; } = default!;

    public string ClientId { get; set; } = default!;

    public string? ClientSecret { get; set; } = default!;

    public string? RedirectUri { get; set; }

    public string? Scope { get; set; }
}
