// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.ServerInformation;
using System.Collections.Concurrent;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.ServerInformation;

public class DefaultServerInformationMetadata : IServerInformationMetadata
{
    public IDictionary<string, string>? ScopeAdditional { get; set; } = new ConcurrentDictionary<string, string>();

    public string? ScopeDefaultValue { get; set; }

    public string? ScopeRequirements { get; set; }

    public IDictionary<string, string>? AuthorizationCodeAdditional { get; set; } = new ConcurrentDictionary<string, string>();

    public string AuthorizationCodeSizeSymbols { get; set; } = default!;
}
