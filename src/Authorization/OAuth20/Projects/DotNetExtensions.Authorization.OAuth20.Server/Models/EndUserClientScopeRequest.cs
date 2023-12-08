// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Models;

public class EndUserClientScopeRequest
{
    public string Username { get; set; } = default!;

    public string ClientId { get; set; } = default!;

    public string? ClientRequestedScope { get; set; }

    public string ServerAllowedScope { get; set; } = default!;

    public bool ServerAllowedScopeDifferent { get; set; } = false;
}
