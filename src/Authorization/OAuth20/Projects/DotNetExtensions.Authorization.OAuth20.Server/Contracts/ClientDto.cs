// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Contracts.Enumerations;

namespace DotNetExtensions.Authorization.OAuth20.Server.Contracts;

public class ClientDto
{
    public string ClientId { get; init; } = default!;

    public string? ClientSecret { get; init; }

    public ClientType ClientType { get; init; }

    public ClientProfile ClientProfile { get; init; }

    public string[]? RedirectionEndpoints { get; private set; }

    public string? LoginPageEndpoint { get; private set; }

    public string[]? Scopes { get; private set; }

    public string[]? Flows { get; private set; }
}
