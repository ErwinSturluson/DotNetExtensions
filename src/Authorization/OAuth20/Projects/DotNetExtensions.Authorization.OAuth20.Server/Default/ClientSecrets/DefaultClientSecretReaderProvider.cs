// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.ClientSecretReaders;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.ClientSecrets;

public class DefaultClientSecretReaderProvider : IClientSecretReaderProvider
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IClientSecretReaderMetadataCollection _clientSecretReaderMetadataCollection;

    public DefaultClientSecretReaderProvider(IServiceProvider serviceProvider, IClientSecretReaderMetadataCollection clientSecretReaderMetadataCollection)
    {
        _serviceProvider = serviceProvider;
        _clientSecretReaderMetadataCollection = clientSecretReaderMetadataCollection;
    }

    public IEnumerable<IClientSecretReader> GetAllClientSecretReaderInstances()
    {
        foreach (var clientSecretReaderMetadata in _clientSecretReaderMetadataCollection.ClientSecretReaders)
        {
            yield return (IClientSecretReader)_serviceProvider.GetRequiredService(clientSecretReaderMetadata.Value.Abstraction);
        }
    }
}
