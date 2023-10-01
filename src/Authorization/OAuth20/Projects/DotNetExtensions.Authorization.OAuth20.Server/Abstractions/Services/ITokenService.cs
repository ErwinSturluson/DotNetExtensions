// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Models;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;

public interface ITokenService
{
    public Task<Token> GetTokenAsync(ScopeResult scopeResult, EndUser endUser, Client client, string redirectUri);
}
