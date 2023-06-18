// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using System.ComponentModel;
using System.Reflection;

namespace DotNetExtensions.Authorization.OAuth20.Server.Flows;

/// <summary>
/// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1
/// </summary>
public enum DefaultErrorType
{
    /// <summary>
    /// The error is not defined. Invalid value for a response.
    /// </summary>
    [Description("undefined")]
    Undefined = 0,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1
    /// </summary>
    [Description("invalid_request")]
    InvalidRequest = 1,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1
    /// </summary>
    [Description("invalid_client")]
    InvalidClient = 2,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1
    /// </summary>
    [Description("invalid_grant")]
    InvalidGrant = 3,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1
    /// </summary>
    [Description("unauthorized_client")]
    UnauthorizedClient = 4,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1
    /// </summary>
    [Description("unsupported_grant_type")]
    UnsupportedGrantType = 5,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1
    /// </summary>
    [Description("invalid_scope")]
    InvalidScope = 6,
}

public static class DefaultErrorTypeExtensions
{
    public static string GetDescriptionAttributeValue(this DefaultErrorType chartFormat)
    {
        var member = typeof(DefaultErrorType).GetMember(chartFormat.ToString()).First();
        string description = member.GetCustomAttribute<DescriptionAttribute>()!.Description; // TODO: use another attribute than DescriptionAttribute

        return description;
    }
}
