// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Flows;
using System.Diagnostics;
using System.Text;

namespace DotNetExtensions.Authorization.OAuth20.Server.Flows.AuthorizationCode.Authorize;

/// <summary>
/// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2"/>
/// </summary>
public class AuthorizeResult : AuthorizeResultBase
{
    public AuthorizeResult(string redirectUri, string code, string? state = null)
        : base(state)
    {
        RedirectUri = redirectUri;
        Code = code;
    }

    public string RedirectUri { get; }

    /// <summary>
    /// TODO: NOTE: The client should avoid making assumptions about code
    /// value sizes.The authorization server SHOULD document the size of
    /// any value it issues.
    /// </summary>
    public string Code { get; set; } = default!;

    public static AuthorizeResult Create(
        string redirectUri,
        string code,
        string? state = null)
    => new AuthorizeResult(redirectUri, code, state);

    public override Task ExecuteAsync(HttpContext httpContext)
    {
        StringBuilder stringBuilder = new(RedirectUri);
        stringBuilder.AppendFormat("?code={0}", Code);
        if (State is not null)
        {
            stringBuilder.AppendFormat("&state={0}", State);
        }

        string redirectLocation = stringBuilder.ToString();
#if DEBUG
        Debug.WriteLine(redirectLocation);
#endif
        httpContext.Response.Redirect(redirectLocation, false, false);

        return Task.CompletedTask;
    }
}
