﻿// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.ServerInformation;

namespace DotNetExtensions.Authorization.OAuth20.Server.Options.Information;

public class OAuth20ServerInformationOptions
{
    /// <summary>
    /// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-3.3"/>
    /// All information should be provided by a <see cref="IServerInformationService"/> instance.
    /// </summary>
    public IDictionary<string, string>? ScopeAdditional { get; set; }

    /// <summary>
    /// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-3.3"/>
    /// The authorization server SHOULD document its default value (if defined).
    /// All information should be provided by a <see cref="IServerInformationService"/> instance.
    /// </summary>
    public string? ScopeDefaultValue { get; set; }

    /// <summary>
    /// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-3.3"/>
    /// The authorization server SHOULD document its scope requirements.
    /// All information should be provided by a <see cref="IServerInformationService"/> instance.
    /// </summary>
    public string? ScopeRequirements { get; set; }

    /// <summary>
    /// All information should be provided by a
    /// <see cref="IServerInformationService"/> instance.
    /// </summary>
    public IDictionary<string, string>? AuthorizationCodeAdditional { get; set; }

    /// <summary>
    /// The client should avoid making assumptions about code
    /// value sizes. The authorization server SHOULD document the size of
    /// any value it issues. All information should be provided by a
    /// <see cref="IServerInformationService"/> instance.
    /// </summary>
    public string? AuthorizationCodeSizeSymbols { get; set; } = default!;
}
