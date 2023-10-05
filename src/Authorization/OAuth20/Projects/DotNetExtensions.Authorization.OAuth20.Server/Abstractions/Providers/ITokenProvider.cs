// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Models;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Providers;

public interface ITokenProvider
{
    public Task<string> GetTokenTypeAsync(EndUser endUser, Client client, string redirectUri);

    public string GetTokenValue(string tokenType, ScopeResult scopeResult, EndUser endUser, Client client, string redirectUri);
}
