// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Endpoints;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.Endpoints;

public class DefaultRedirectResult : RedirectResultBase
{
    public DefaultRedirectResult()
    {
    }

    public DefaultRedirectResult(string redirectUri, IDictionary<string, string>? queryParameters = null)
        : base(redirectUri, queryParameters)
    {
    }

    public override Task ExecuteAsync(HttpContext httpContext)
    {
        string redirectLocation;

        if (QueryParameters is not null && QueryParameters.Any())
        {
            string queryString = string.Join("&", QueryParameters.Select(x => $"{Uri.EscapeDataString(x.Key)}={Uri.EscapeDataString(x.Value)}"));
            redirectLocation = RedirectUri + "?" + queryString;
        }
        else
        {
            redirectLocation = RedirectUri;
        }

        httpContext.Response.Redirect(redirectLocation, false, false);

        return Task.CompletedTask;
    }
}
