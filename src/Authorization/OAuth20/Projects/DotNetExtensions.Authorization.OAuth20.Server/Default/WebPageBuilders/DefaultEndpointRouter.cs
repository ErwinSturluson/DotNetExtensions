// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.WebPageBuilders;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.WebPageBuilders;

public class DefaultWebPageBuilderRouter : IWebPageBuilderRouter
{
    private readonly IWebPageBuilderProvider _webPageBuilderProvider;

    public DefaultWebPageBuilderRouter(IWebPageBuilderProvider webPageBuilderProvider)
    {
        _webPageBuilderProvider = webPageBuilderProvider;
    }

    public bool TryGetWebPageBuilder(HttpContext httpContext, out IWebPageBuilder? webPageBuilder)
    {
        string webPagePath = httpContext.Request.Path.ToUriComponent();

        return _webPageBuilderProvider.TryGetWebPageBuilderInstanceByRoute(webPagePath, out webPageBuilder);
    }
}
