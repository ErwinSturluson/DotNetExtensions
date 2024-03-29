﻿// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Flows;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.ResourceOwnerPasswordCredentials.Token;

namespace DotNetExtensions.Authorization.OAuth20.Server.Flows.ResourceOwnerPasswordCredentials;

/// <summary>
/// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-4.3"/>
/// </summary>
public interface IResourceOwnerPasswordCredentialsFlow : ITokenFlow
{
    Task<IResult> GetTokenAsync(TokenArguments args, Client client);
}
