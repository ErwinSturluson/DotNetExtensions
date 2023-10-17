// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataSources;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Models.Enums;

namespace DotNetExtensions.Authorization.OAuth20.Server.ClientSecretReaders.TlsCertificate;

public class DefaultTlsCertificateClientSecretReader : ITlsCertificateClientSecretReader
{
    private readonly IClientSecretDataSource _clientSecretDataSource;

    public DefaultTlsCertificateClientSecretReader(IClientSecretDataSource clientSecretDataSource)
    {
        _clientSecretDataSource = clientSecretDataSource;
    }

    public async Task<ClientSecret?> GetClientSecretAsync(HttpContext httpContext)
    {
        ClientSecret? clientSecret = null;

        var clientCertificate = await httpContext.Connection.GetClientCertificateAsync();

        if (clientCertificate is null)
        {
            return clientSecret;
        }

        string clientSecretContent = clientCertificate.GetRawCertDataString();

        await _clientSecretDataSource.GetClientSecretAsync(DefaultClientSecretType.TlsCertificate.GetFieldNameAttributeValue(), clientSecretContent);

        return clientSecret;
    }
}
