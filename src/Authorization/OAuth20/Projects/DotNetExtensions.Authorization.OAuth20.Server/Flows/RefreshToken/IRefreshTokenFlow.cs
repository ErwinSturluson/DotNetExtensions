﻿// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Flows;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.RefreshToken.Token;

namespace DotNetExtensions.Authorization.OAuth20.Server.Flows.RefreshToken;

/// <summary>
/// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-1.5
/// </summary>
public interface IRefreshTokenFlow : ITokenFlow
{
    Task<TokenResult> GetTokenAsync(TokenArguments args, Client client);
}
