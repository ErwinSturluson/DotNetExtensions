// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Endpoints;

public abstract class RedirectResultBase : IResult
{
    protected RedirectResultBase(string redirectUri, IDictionary<string, string>? queryParameters = null)
    {
        RedirectUri = redirectUri;
        QueryParameters = queryParameters;
    }

    protected RedirectResultBase()
    {
    }

    public string RedirectUri { get; set; } = default!;

    public IDictionary<string, string>? QueryParameters { get; set; }

    public abstract Task ExecuteAsync(HttpContext httpContext);
}
