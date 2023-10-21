// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Options.X509Certificate2SigningCredentials;

public class OAuth20SigningCredentialsOptions
{
    public const string DefaultSection = "OAuth20Server:SigningCredentials";

    public string? X509Certificate2DeploymentTypeTextName { get; set; }

    public string? X509Certificate2DeploymentTypeFileName { get; set; }

    public IEnumerable<X509Certificate2Options>? X509Certificate2List { get; set; }
}
