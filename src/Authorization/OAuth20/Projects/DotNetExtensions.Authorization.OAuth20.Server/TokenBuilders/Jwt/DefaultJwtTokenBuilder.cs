// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.TokenBuilders.Jwt;

/// <summary>
/// Description RFC7519: <see cref="https://datatracker.ietf.org/doc/html/rfc7519"/>
/// </summary>
public class DefaultJwtTokenBuilder : IJwtTokenBuilder
{
    public ValueTask<string> BuildTokenAsync(IDictionary<string, string> args)
    {
        throw new NotImplementedException();
    }
}
