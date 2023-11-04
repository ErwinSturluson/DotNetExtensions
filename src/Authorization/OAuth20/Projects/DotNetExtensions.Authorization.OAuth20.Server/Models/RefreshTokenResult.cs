// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Models;

public class RefreshTokenResult
{
    public string Value { get; set; } = default!;

    public string AccessTokenValue { get; set; } = default!;
}
