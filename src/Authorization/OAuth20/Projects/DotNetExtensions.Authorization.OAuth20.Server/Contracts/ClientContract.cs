// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain.Enums;

namespace DotNetExtensions.Authorization.OAuth20.Server.Contracts;

public class ClientContract
{
    public string ClientId { get; set; } = default!;

    public string? ClientSecret { get; set; }

    public ClientType ClientType { get; set; }

    public ClientProfile ClientProfile { get; set; }

    public string[]? RedirectionEndpoints { get; set; }

    public string? LoginPageEndpoint { get; set; }

    public string[]? Scopes { get; set; }

    public string[]? Flows { get; set; }
}
