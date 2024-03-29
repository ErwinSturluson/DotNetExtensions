﻿// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.ServerSigningCredentials;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using Microsoft.IdentityModel.Tokens;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.ServerSigningCredentials;

public class DefaultServerSigningCredentialsProvider : IServerSigningCredentialsProvider
{
    private readonly IServerSigningCredentialsCollection _serverSigningCredentialsCollection;

    public DefaultServerSigningCredentialsProvider(IServerSigningCredentialsCollection serverSigningCredentialsCollection)
    {
        _serverSigningCredentialsCollection = serverSigningCredentialsCollection;
    }

    public Task<IEnumerable<SigningCredentials>> GetSigningCredentialsAsync(IEnumerable<SigningCredentialsAlgorithm> signingCredentialsAlgorithms)
    {
        var algorithmKeySigningCredentials = _serverSigningCredentialsCollection.AlgorithmKeySigningCredentials;

        List<SigningCredentials> signingCredentialsList = new(signingCredentialsAlgorithms.Count());

        foreach (var signingCredentialsAlgorithm in signingCredentialsAlgorithms)
        {
            if (algorithmKeySigningCredentials.TryGetValue(signingCredentialsAlgorithm.Name, out var signingCredentials))
            {
                signingCredentialsList.Add(signingCredentials);
            }
        }

        return Task.FromResult<IEnumerable<SigningCredentials>>(signingCredentialsList);
    }

    public Task<SigningCredentials> GetDefaultSigningCredentialsAsync()
    {
        return Task.FromResult(_serverSigningCredentialsCollection.DefaultSigningCredentials);
    }
}
