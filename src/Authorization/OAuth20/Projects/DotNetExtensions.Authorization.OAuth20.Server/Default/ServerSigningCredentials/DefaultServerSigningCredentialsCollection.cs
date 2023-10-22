// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.ServerSigningCredentials;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Concurrent;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.ServerSigningCredentials;

public class DefaultServerSigningCredentialsCollection : IServerSigningCredentialsCollection
{
    public IDictionary<string, SigningCredentials> AlgorithmKeySigningCredentials { get; set; } = new ConcurrentDictionary<string, SigningCredentials>();
}
