// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.ServerInformation;
using System.Collections.Concurrent;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.ServerInformation;

public class DefaultServerInformationMetadata : IServerInformationMetadata
{
    public IDictionary<string, string>? Scope { get; set; } = new ConcurrentDictionary<string, string>();

    public IDictionary<string, string>? AuthorizationCode { get; set; } = new ConcurrentDictionary<string, string>();
}
