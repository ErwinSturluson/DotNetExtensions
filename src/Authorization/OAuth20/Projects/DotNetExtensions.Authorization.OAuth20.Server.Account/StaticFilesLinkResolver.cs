using System.Reflection;

namespace DotNetExtensions.Authorization.OAuth20.Server.Account;

public sealed class StaticFilesLinkResolver
{
    private string _leadingUriSegments;
    private string _appCssFileName;

    public StaticFilesLinkResolver()
    {
        string blazorAppName = Assembly.GetExecutingAssembly().GetName().Name ??
            "DotNetExtensions.Authorization.OAuth20.Server.Account";

        string? appName = Assembly.GetEntryAssembly()?.GetName().Name;

        _leadingUriSegments = appName != blazorAppName && appName is not null ?
            "_content/" + appName + "/" :
            string.Empty;

        _appCssFileName = appName ?? blazorAppName;
    }

    public string ResolveStaticFileLink(string sourceLink) => _leadingUriSegments + sourceLink;

    public string ResolveStylesCssStaticFileLink() => _appCssFileName + ".styles.css";
}
