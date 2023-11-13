// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Flows;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.ClientCredentials.Token;

namespace DotNetExtensions.Authorization.OAuth20.Server.Flows.ClientCredentials;

/// <summary>
/// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-4.4"/>
/// </summary>
public interface IClientCredentialsFlow : ITokenFlow
{
    Task<IResult> GetTokenAsync(TokenArguments args, Client client);
}
