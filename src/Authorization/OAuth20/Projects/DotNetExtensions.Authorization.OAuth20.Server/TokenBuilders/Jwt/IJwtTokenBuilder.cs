// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.TokenBuilders;

namespace DotNetExtensions.Authorization.OAuth20.Server.TokenBuilders.Jwt;

/// <summary>
/// Description RFC7519: <see cref="https://datatracker.ietf.org/doc/html/rfc7519"/>
/// </summary>
public interface IJwtTokenBuilder : ITokenBuilder
{
}
