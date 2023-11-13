// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Flows;

public abstract class RedirectResultBase : IResult
{
    protected RedirectResultBase(string redirectUri)
    {
        RedirectUri = redirectUri;
    }

    protected RedirectResultBase()
    {
    }

    public string RedirectUri { get; set; } = default!;

    public abstract Task ExecuteAsync(HttpContext httpContext);
}
