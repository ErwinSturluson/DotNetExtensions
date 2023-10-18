// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Flows;

namespace DotNetExtensions.Authorization.OAuth20.Server.Flows.ClientCredentials.Token;

/// <summary>
/// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-4.4.2"/>
/// </summary>
public class TokenArguments : TokenArgumentsBase
{
    private TokenArguments(
        string grantType,
        string? redirectUri = null,
        string? scope = null)
        : base(grantType, redirectUri, scope)
    {
    }

    public static TokenArguments Create(
        string grantType,
        string? redirectUri = null,
        string? scope = null)
        => new(grantType, redirectUri, scope);

    public static TokenArguments Create(FlowArguments flowArguments)
    {
        flowArguments.Values.TryGetValue("redirect_uri", out string? redirectUri);
        flowArguments.Values.TryGetValue("scope", out string? scope);
        string grantType = flowArguments.Values["grant_type"];

        return new(grantType, redirectUri, scope);
    }
}
