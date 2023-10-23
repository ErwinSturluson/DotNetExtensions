// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Options.ServerSigningCredentials;

public class SelfSignedX509Certificate2Options
{
    public int? RsaSize { get; set; }

    public string? RsaSecurityKeyId { get; set; }

    public IEnumerable<string>? Algorithms { get; set; }
}
