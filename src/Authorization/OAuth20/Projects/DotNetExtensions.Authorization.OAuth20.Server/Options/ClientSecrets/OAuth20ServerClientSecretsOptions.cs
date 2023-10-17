// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Options.ClientSecrets;

namespace DotNetExtensions.Authorization.OAuth20.Server.Options.ClientSecretReaders;

public class OAuth20ServerClientSecretsOptions
{
    public const string DefaultSection = "OAuth20Server:ClientSecrets";

    public string? AuthorizationHeaderBasicClientSecretTypeName { get; set; }

    public string? RequestBodyClientCredentialsClientSecretTypeName { get; set; }

    public string? TlsCertificateClientSecretTypeName { get; set; }

    public IEnumerable<ClientSecretTypeOptions>? ClientSecretTypes { get; set; }
}
