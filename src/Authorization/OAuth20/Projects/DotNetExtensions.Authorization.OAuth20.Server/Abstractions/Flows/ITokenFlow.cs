// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Flows;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Flows;

/// <summary>
/// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-1.4
/// </summary>
public interface ITokenFlow : IFlow
{
    public Task<IResult> GetTokenAsync(FlowArguments args, Client client);
}
