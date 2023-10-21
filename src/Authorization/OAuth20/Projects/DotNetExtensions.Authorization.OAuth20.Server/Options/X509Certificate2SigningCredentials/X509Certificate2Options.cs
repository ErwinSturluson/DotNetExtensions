// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Options.X509Certificate2SigningCredentials.Enumerations;

namespace DotNetExtensions.Authorization.OAuth20.Server.Options.X509Certificate2SigningCredentials;

public class X509Certificate2Options
{
    public string Algorithm { get; set; } = default!;

    public X509Certificate2DeploymentType DeploymentType { get; set; }

    public string DeploymentValue { get; set; } = default!;
}
