﻿// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain.Abstractions;

namespace DotNetExtensions.Authorization.OAuth20.Server.Domain;

/// <summary>
/// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-2"/>
/// </summary>
public class Client : EntityBase<int>
{
    /// <summary>
    /// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-2.3.1"/>
    /// </summary>
    public string ClientId { get; set; } = default!;

    /// <summary>
    /// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-2.3.1"/>
    /// </summary>
    public IEnumerable<ClientSecret>? ClientSecrets { get; set; }

    /// <summary>
    /// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-2.1"/>
    /// </summary>
    public Enums.ClientType ClientTypeId { get; set; }

    public ClientType ClientType { get; set; } = default!;

    /// <summary>
    /// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-2.1"/>
    /// </summary>
    public Enums.ClientProfile ClientProfileId { get; set; }

    public ClientProfile ClientProfile { get; set; } = default!;

    // TODO: database models
    /// <summary>
    /// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-3.1.2"/>
    /// Description RFC6749 (Registration Requirements): <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-3.1.2.2"/>
    /// </summary>
    public string[]? RedirectionEndpoints { get; set; }

    /// <summary>
    /// Description RFC6749 #1: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-4.1"/> (steb "B")
    /// Description RFC6749 #2: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-4.2"/> (steb "B")
    /// </summary>
    public string? LoginEndpoint { get; set; }

    public int? TokenTypeId { get; set; }

    public TokenType? TokenType { get; set; }

    public int? TokenExpirationSeconds { get; set; }

    public IEnumerable<ClientScope>? ClientScopes { get; set; }

    public IEnumerable<ClientFlow>? ClientFlows { get; set; }
}
