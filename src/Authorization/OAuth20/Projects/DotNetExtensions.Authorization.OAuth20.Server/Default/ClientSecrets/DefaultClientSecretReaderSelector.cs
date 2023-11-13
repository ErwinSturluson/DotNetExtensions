// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.ClientSecretReaders;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.ClientSecrets;

public class DefaultClientSecretReaderSelector : IClientSecretReaderSelector
{
    private readonly IClientSecretReaderProvider _clientSecretReaderProvider;

    public DefaultClientSecretReaderSelector(IClientSecretReaderProvider clientSecretReaderProvider)
    {
        _clientSecretReaderProvider = clientSecretReaderProvider;
    }

    public Task<IEnumerable<IClientSecretReader>> GetClientSecretReadersAsync()
    {
        var readers = _clientSecretReaderProvider.GetAllClientSecretReaderInstances();

        return Task.FromResult(readers);
    }
}
