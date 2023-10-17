// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain;

namespace DotNetExtensions.Authorization.OAuth20.Server.ClientSecretReaders.TlsCertificate;

public class DefaultTlsCertificateClientSecretReader : ITlsCertificateClientSecretReader
{
    public Task<ClientSecret?> GetClientSecretAsync(HttpContext httpContext)
    {
        throw new NotImplementedException();
    }
}
