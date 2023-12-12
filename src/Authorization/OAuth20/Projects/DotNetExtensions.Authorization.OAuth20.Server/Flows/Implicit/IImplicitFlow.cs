// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Flows;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.Implicit.Mixed;
using DotNetExtensions.Authorization.OAuth20.Server.Models;

namespace DotNetExtensions.Authorization.OAuth20.Server.Flows.Implicit;

/// <summary>
/// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-4.2"/>
/// </summary>
public interface IImplicitFlow : IAuthorizeFlow
{
    Task<IResult> AuthorizeAsync(AuthorizeArguments args, EndUser endUser, Client client, ScopeResult scopeResult);
}
