﻿// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Models;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;

public interface IRefreshTokenService
{
    public Task<RefreshTokenResult> GetRefreshTokenAsync(AccessTokenResult accessToken);
}
