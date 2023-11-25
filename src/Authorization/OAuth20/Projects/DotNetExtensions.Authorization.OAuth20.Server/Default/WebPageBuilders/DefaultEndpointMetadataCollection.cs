// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.WebPageBuilders;
using System.Collections.Concurrent;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.WebPageBuilders;

public class DefaultWebPageBuilderMetadataCollection : IWebPageBuilderMetadataCollection
{
    public IDictionary<string, WebPageBuilderMetadata> WebPageBuilders { get; set; } = new ConcurrentDictionary<string, WebPageBuilderMetadata>();
}
