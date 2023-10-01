// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.AuthorizationCode.Authorize;
using DotNetExtensions.Authorization.OAuth20.Server.Models;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Providers;

public interface IAuthorizationCodeProvider
{
    public string GetAuthorizationCodeValue(AuthorizeArguments args, EndUser endUser, Client client, string redirectUri, ScopeResult scopeResult);
}
