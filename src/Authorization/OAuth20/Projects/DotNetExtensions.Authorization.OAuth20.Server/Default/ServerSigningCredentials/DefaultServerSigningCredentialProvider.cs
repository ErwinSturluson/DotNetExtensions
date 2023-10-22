// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.ServerSigningCredentials;
using Microsoft.IdentityModel.Tokens;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.ServerSigningCredentials;

public class DefaultServerSigningCredentialProvider
{
    private readonly IServerSigningCredentialsCollection _serverSigningCredentialsCollection;

    public DefaultServerSigningCredentialProvider(IServerSigningCredentialsCollection serverSigningCredentialsCollection)
    {
        _serverSigningCredentialsCollection = serverSigningCredentialsCollection;
    }

    public Task<SigningCredentials?> GetSigningCredentialsAsync(string algorithm)
    {
        var algorithmKeySigningCredentials = _serverSigningCredentialsCollection.AlgorithmKeySigningCredentials;

        SigningCredentials? signingCredentials;

        if (!algorithmKeySigningCredentials.TryGetValue(algorithm, out signingCredentials))
        {
            if (algorithmKeySigningCredentials.Count > 0)
            {
                signingCredentials = algorithmKeySigningCredentials.First().Value;
            }
            else
            {
                signingCredentials = null;
            }
        }

        return Task.FromResult(signingCredentials);
    }
}
