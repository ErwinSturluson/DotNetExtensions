// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Options.ServerSigningCredentials.Enumerations;

namespace DotNetExtensions.Authorization.OAuth20.Server.Options.ServerSigningCredentials;

public class SecurityKeyOptions
{
    public int? SecurityKeySize { get; set; }

    public string? SecurityKeyId { get; set; }

    public SecurityAlgorithmType SecurityAlgorithmType { get; set; }
}
