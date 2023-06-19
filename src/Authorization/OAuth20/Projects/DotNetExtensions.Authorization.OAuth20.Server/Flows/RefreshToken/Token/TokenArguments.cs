// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Flows;

namespace DotNetExtensions.Authorization.OAuth20.Server.Flows.RefreshToken.Token;

/// <summary>
/// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-6
/// </summary>
public class TokenArguments : TokenArgumentsBase
{
    private TokenArguments(
        string refreshToken,
        string grantType,
        string clientId,
        string? clientSecret = null,
        string? redirectUri = null,
        string? scope = null)
        : base(grantType, clientId, clientSecret, redirectUri, scope)
    {
        RefreshToken = refreshToken;
    }

    public string RefreshToken { get; set; } = default!;

    public static TokenArguments Create(
        string refreshToken,
        string grantType,
        string clientId,
        string? clientSecret = null,
        string? redirectUri = null,
        string? scope = null)
        => new(refreshToken, grantType, clientId, clientSecret, redirectUri, scope);

    public static TokenArguments Create(FlowArguments flowArguments)
    {
        flowArguments.Values.TryGetValue("client_secret", out string? clientSecret);
        flowArguments.Values.TryGetValue("redirect_uri", out string? redirectUri);
        flowArguments.Values.TryGetValue("state", out string? scope);
        string refreshToken = flowArguments.Values["refresh_token"];
        string grantType = flowArguments.Values["grant_type"];
        string clientId = flowArguments.Values["client_id"];

        return new(refreshToken, grantType, clientId, clientSecret, redirectUri, scope);
    }
}
