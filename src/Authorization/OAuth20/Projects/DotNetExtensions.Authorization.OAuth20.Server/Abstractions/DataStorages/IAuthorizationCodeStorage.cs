// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Models;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataStorages;

public interface IAuthorizationCodeStorage
{
    public Task AddAuthorizationCodeAsync(AuthorizationCode code);

    public Task<AuthorizationCode> GetAuthorizationCodeAsync(string value);
}
