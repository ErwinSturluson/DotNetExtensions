// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain;

namespace DotNetExtensions.Authorization.OAuth20.Server.ClientSecretReaders.RequestBodyClientCredentials;

public class DefaultRequestBodyClientCredentialsClientSecretReader : IRequestBodyClientCredentialsClientSecretReader
{
    public Task<ClientSecret?> GetClientSecretAsync(HttpContext httpContext)
    {
        throw new NotImplementedException();
    }
}
