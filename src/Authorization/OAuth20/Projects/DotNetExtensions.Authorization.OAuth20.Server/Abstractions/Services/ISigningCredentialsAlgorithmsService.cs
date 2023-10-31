// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;

public interface ISigningCredentialsAlgorithmsService
{
    public Task<IEnumerable<SigningCredentialsAlgorithm>> GetSigningCredentialsAlgorithmsForScopesAsync(IEnumerable<Scope> scopes);
}
