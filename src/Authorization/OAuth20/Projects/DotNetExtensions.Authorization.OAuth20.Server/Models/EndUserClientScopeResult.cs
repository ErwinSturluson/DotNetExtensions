// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Models;

public class EndUserClientScopeResult
{
    public string Username { get; set; } = default!;

    public string ClientId { get; set; } = default!;

    public string ServerAllowedScope { get; set; } = default!;

    public string EndUserIssuedScope { get; set; } = default!;

    public bool EndUserIssuedScopeDifferent { get; set; } = false;
}
