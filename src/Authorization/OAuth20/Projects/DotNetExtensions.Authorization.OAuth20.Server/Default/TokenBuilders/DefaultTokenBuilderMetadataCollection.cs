// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.TokenBuilders;
using System.Collections.Concurrent;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.TokenBuilders;

public class DefaultTokenBuilderMetadataCollection : ITokenBuilderMetadataCollection
{
    public IDictionary<string, TokenBuilderMetadata> TokenBuilders { get; set; } = new ConcurrentDictionary<string, TokenBuilderMetadata>();
}
