// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Models;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;

public interface IAccessTokenService
{
    public Task<AccessTokenResult> GetAccessTokenAsync(string issuedScope, bool issuedScopeDifferent, Client client, string? redirectUri = null, EndUser? endUser = null);
}
