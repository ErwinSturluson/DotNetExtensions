// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Flows;

namespace DotNetExtensions.Authorization.OAuth20.Server.Flows.AuthorizationCode.Token;

/// <summary>
/// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.3"/>
/// </summary>
public class TokenArguments : TokenArgumentsBase
{
    private TokenArguments(
        string code,
        string grantType,
        string clientId,
        string? clientSecret = null,
        string? redirectUri = null,
        string? scope = null)
        : base(grantType, clientId, clientSecret, redirectUri, scope)
    {
        Code = code;
    }

    public string Code { get; set; } = default!;

    public static TokenArguments Create(
        string code,
        string grantType,
        string clientId,
        string? clientSecret = null,
        string? redirectUri = null,
        string? scope = null)
        => new(code, grantType, clientId, clientSecret, redirectUri, scope);

    public static TokenArguments Create(FlowArguments flowArguments)
    {
        flowArguments.Values.TryGetValue("client_secret", out string? clientSecret);
        flowArguments.Values.TryGetValue("redirect_uri", out string? redirectUri);
        flowArguments.Values.TryGetValue("scope", out string? scope);
        string code = flowArguments.Values["code"];
        string grantType = flowArguments.Values["grant_type"];
        string clientId = flowArguments.Values["client_id"];

        return new(code, grantType, clientId, clientSecret, redirectUri, scope);
    }
}
