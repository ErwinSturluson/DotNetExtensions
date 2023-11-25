// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.WebPageBuilders;

public interface IWebPageBuilderMetadataCollection
{
    public IDictionary<string, WebPageBuilderMetadata> WebPageBuilders { get; set; }
}
