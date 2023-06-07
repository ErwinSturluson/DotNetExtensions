// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions;
using System.Collections.Concurrent;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default;

public class DefaultEndpointMetadataCollection : IEndpointMetadataCollection
{
    public IDictionary<string, EndpointMetadata> Endpoints { get; set; } = new ConcurrentDictionary<string, EndpointMetadata>();
}
