// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Providers;

public interface ITokenProvider
{
    public Task<TokenType> GetTokenTypeAsync(Client client);

    public string GetTokenValue(TokenType tokenType, IEnumerable<Scope> scope, Client client, string redirectUri, EndUser? endUser = null);
}
