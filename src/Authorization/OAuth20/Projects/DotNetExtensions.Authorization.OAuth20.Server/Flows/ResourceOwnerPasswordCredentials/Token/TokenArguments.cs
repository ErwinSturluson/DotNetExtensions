// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Flows;

namespace DotNetExtensions.Authorization.OAuth20.Server.Flows.ResourceOwnerPasswordCredentials.Token;

/// <summary>
/// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-4.3.2"/>
/// </summary>
public class TokenArguments : TokenArgumentsBase
{
    private TokenArguments(
        string username,
        string password,
        string grantType,
        string clientId,
        string? clientSecret = null,
        string? redirectUri = null,
        string? scope = null)
        : base(grantType, clientId, clientSecret, redirectUri, scope)
    {
        Username = username;
        Password = password;
    }

    public string Username { get; set; } = default!;

    public string Password { get; set; } = default!;

    public static TokenArguments Create(
        string username,
        string password,
        string grantType,
        string clientId,
        string? clientSecret = null,
        string? redirectUri = null,
        string? scope = null)
        => new TokenArguments(username, password, grantType, clientId, clientSecret, redirectUri, scope);

    public static TokenArguments Create(FlowArguments flowArguments)
    {
        flowArguments.Values.TryGetValue("client_secret", out string? clientSecret);
        flowArguments.Values.TryGetValue("redirect_uri", out string? redirectUri);
        flowArguments.Values.TryGetValue("state", out string? scope);
        string username = flowArguments.Values["username"];
        string password = flowArguments.Values["password"];
        string grantType = flowArguments.Values["grant_type"];
        string clientId = flowArguments.Values["client_id"];

        return Create(username, password, grantType, clientId, clientSecret, redirectUri, scope);
    }

    public override Task ExecuteAsync(HttpContext httpContext)
    {
        throw new NotImplementedException();
    }
}
