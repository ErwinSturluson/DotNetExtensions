// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using System.ComponentModel;

namespace DotNetExtensions.Authorization.OAuth20.Server.Domain.Enums;

/// <summary>
/// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-2.1"/>
/// </summary>
public enum ClientType
{
    /// <summary>
    /// Client type value by default. Invalid value for processing.
    /// </summary>
    [Description("Client profile value by default. Invalid value for processing.")]
    Undefined = 0,

    /// <summary>
    /// Clients capable of maintaining the confidentiality of their
    /// credentials (e.g., client implemented on a secure server with
    /// restricted access to the client credentials), or capable of secure
    /// client authentication using other means.
    /// </summary>
    [Description("Clients capable of maintaining the confidentiality of their " +
        "credentials (e.g., client implemented on a secure server with " +
        "restricted access to the client credentials), or capable of secure " +
        "client authentication using other means.")]
    Confidential = 1,

    /// <summary>
    /// Clients incapable of maintaining the confidentiality of their
    /// credentials (e.g., clients executing on the device used by the
    /// resource owner, such as an installed native application or a web
    /// browser-based application), and incapable of secure client
    /// authentication via any other means.
    /// </summary>
    [Description("Clients incapable of maintaining the confidentiality of their " +
        "credentials (e.g., clients executing on the device used by the " +
        "resource owner, such as an installed native application or a web " +
        "browser-based application), and incapable of secure client " +
        "authentication via any other means.")]
    Public = 2,
}
