// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Flows;

public abstract class TokenArgumentsBase
{
    protected TokenArgumentsBase(
        string grantType,
        string? redirectUri = null,
        string? scope = null)
    {
        GrantType = grantType;
        RedirectUri = redirectUri;
        Scope = scope;
    }

    public string GrantType { get; set; } = default!;

    public string? RedirectUri { get; set; }

    public string? Scope { get; set; }
}
