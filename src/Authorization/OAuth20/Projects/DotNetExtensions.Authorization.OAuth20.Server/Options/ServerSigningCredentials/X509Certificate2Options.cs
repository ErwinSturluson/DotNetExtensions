// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Options.ServerSigningCredentials.Enumerations;

namespace DotNetExtensions.Authorization.OAuth20.Server.Options.ServerSigningCredentials;

public class X509Certificate2Options
{
    public string Algorithm { get; set; } = default!;

    public X509Certificate2DeploymentType DeploymentType { get; set; }

    public string? CertificateContent { get; set; }

    public string? KeyContent { get; set; }

    public string? CertificateFileName { get; set; }

    public string? KeyFileName { get; set; }

    public string? Password { get; set; }
}
