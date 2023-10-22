// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using Microsoft.IdentityModel.Tokens;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.ServerSigningCredentials;

public interface IServerSigningCredentialsProvider
{
    public Task<SigningCredentials?> GetSigningCredentialsAsync(string algorithm);
}
