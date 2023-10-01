// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Models;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Providers;

public interface ITokenProvider
{
    public string GetTokenType(EndUser endUser, Client client, string redirectUri);

    public string GetTokenValue(ScopeResult scopeResult, EndUser endUser, Client client, string redirectUri);
}
