using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace DotNetExtensions.Authorization.OAuth20.Server.Account;

public sealed class StaticFilesLinkResolver
{
    private string _leadingUriSegments;
    private string _appCssFileName;

    public StaticFilesLinkResolver(IWebHostEnvironment env)
    {
        string blazorAppName = Assembly.GetExecutingAssembly().GetName().Name ??
            "DotNetExtensions.Authorization.OAuth20.Server.Account";

        string? appName = Assembly.GetEntryAssembly()?.GetName().Name;

        _leadingUriSegments = appName != blazorAppName && appName is not null ?
            "_content/" + blazorAppName + "/" :
            string.Empty;

        _appCssFileName = appName ?? blazorAppName;

#if DEBUG
        var rootProvider = env.WebRootFileProvider as CompositeFileProvider;
        var provider = rootProvider!.FileProviders.FirstOrDefault(x => x.GetType().Name == "ManifestStaticWebAssetFileProvider")!;
        var rootNodes = provider.GetType().GetRuntimeFields().FirstOrDefault(x => x.Name == "_root")!.GetValue(provider)!;
        var children = rootNodes.GetType().GetProperty("Children")!.GetValue(rootNodes);
#endif
    }

    public string ResolveStaticFileLink(string sourceLink) => _leadingUriSegments + sourceLink;

    public string ResolveStylesCssStaticFileLink() => _appCssFileName + ".styles.css";
}
