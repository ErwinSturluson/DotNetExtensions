// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Flows;

namespace DotNetExtensions.Authorization.OAuth20.Server.Flows.AuthorizationCode;

/// <summary>
/// Description RFC6749: <see cref=""/>
/// </summary>
public interface IAuthorizationCodeFlow : IAuthorizeFlow, ITokenFlow
{
}
