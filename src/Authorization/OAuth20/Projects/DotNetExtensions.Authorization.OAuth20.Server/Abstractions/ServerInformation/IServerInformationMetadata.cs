// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.ServerInformation;

public interface IServerInformationMetadata
{
    public IDictionary<string, string>? Scope { get; set; }

    public IDictionary<string, string>? AuthorizationCode { get; set; }
}
