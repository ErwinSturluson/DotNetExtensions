// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.AuthorizationCode.Authorize;
using DotNetExtensions.Authorization.OAuth20.Server.Models;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;

public interface IAuthorizationCodeService
{
    public Task<string> GetAuthorizationCodeAsync(AuthorizeArguments args, EndUser endUser, Client client, string redirectUri, ScopeResult scopeResult);

    public Task<Token> ExchangeAuthorizationCodeAsync(string code, EndUser endUser, Client client, string? redirectUri);
}
