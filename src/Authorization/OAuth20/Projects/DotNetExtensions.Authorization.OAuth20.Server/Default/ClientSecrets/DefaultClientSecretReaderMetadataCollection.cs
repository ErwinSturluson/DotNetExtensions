// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.ClientSecretReaders;
using System.Collections.Concurrent;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.ClientSecrets;

public class DefaultClientSecretReaderMetadataCollection : IClientSecretReaderMetadataCollection
{
    public IDictionary<string, ClientSecretReaderMetadata> ClientSecretReaders { get; set; } = new ConcurrentDictionary<string, ClientSecretReaderMetadata>();
}
