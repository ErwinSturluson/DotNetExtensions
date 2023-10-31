// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Models;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;

public interface ITokenService
{
    public Task<AccessTokenResult> GetTokenAsync(string issuedScope, bool issuedScopeDifferent, Client client, string redirectUri, EndUser? endUser = null);
}
