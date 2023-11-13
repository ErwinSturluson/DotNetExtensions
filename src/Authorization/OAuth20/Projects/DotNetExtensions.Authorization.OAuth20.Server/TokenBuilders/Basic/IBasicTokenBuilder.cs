// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.TokenBuilders;

namespace DotNetExtensions.Authorization.OAuth20.Server.TokenBuilders.Basic;

/// <summary>
/// Description RFC7519: <see cref="https://datatracker.ietf.org/doc/html/rfc7617"/>
/// </summary>
public interface IBasicTokenBuilder : ITokenBuilder
{
}
