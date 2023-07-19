// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Options;
using System.ComponentModel;
using System.Reflection;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors;

/// <summary>
/// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-5.2
/// </summary>
public enum DefaultTokenErrorType
{
    /// <summary>
    /// The error is not defined. Invalid value for a response.
    /// </summary>
    [Description("undefined")]
    Undefined = 0,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-5.2
    /// </summary>
    [Description("invalid_request")]
    InvalidRequest = 1,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-5.2
    /// </summary>
    [Description("invalid_client")]
    InvalidClient = 2,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-5.2
    /// </summary>
    [Description("invalid_grant")]
    InvalidGrant = 3,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-5.2
    /// </summary>
    [Description("unauthorized_client")]
    UnauthorizedClient = 4,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-5.2
    /// </summary>
    [Description("unsupported_grant_type")]
    UnsupportedGrantType = 5,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-5.2
    /// </summary>
    [Description("invalid_scope")]
    InvalidScope = 6,
}

public static class DefaultErrorTypeExtensions
{
    public static string GetDescriptionAttributeValue(this DefaultTokenErrorType defaultErrorType, OAuth20ServerOptions? options)
    {
        string errorCode = defaultErrorType switch
        {
            DefaultTokenErrorType.Undefined => GetDescriptionAttributeValue(defaultErrorType),
            DefaultTokenErrorType.InvalidRequest => options?.Errors.TokenInvalidRequestErrorCode ?? GetDescriptionAttributeValue(defaultErrorType),
            DefaultTokenErrorType.InvalidClient => options?.Errors.TokenInvalidClientErrorCode ?? GetDescriptionAttributeValue(defaultErrorType),
            DefaultTokenErrorType.InvalidGrant => options?.Errors.TokenInvalidGrantErrorCode ?? GetDescriptionAttributeValue(defaultErrorType),
            DefaultTokenErrorType.UnauthorizedClient => options?.Errors.TokenUnauthorizedClientErrorCode ?? GetDescriptionAttributeValue(defaultErrorType),
            DefaultTokenErrorType.UnsupportedGrantType => options?.Errors.TokenUnsupportedGrantTypeErrorCode ?? GetDescriptionAttributeValue(defaultErrorType),
            DefaultTokenErrorType.InvalidScope => options?.Errors.TokenInvalidScopeErrorCode ?? GetDescriptionAttributeValue(defaultErrorType),
            _ => throw new NotSupportedException($"{nameof(defaultErrorType)}:{defaultErrorType}"),
        };

        return errorCode;
    }

    public static string GetDescriptionAttributeValue(this DefaultTokenErrorType defaultErrorType)
    {
        var member = typeof(DefaultTokenErrorType).GetMember(defaultErrorType.ToString()).First();
        string description = member.GetCustomAttribute<DescriptionAttribute>()!.Description; // TODO: use another attribute than DescriptionAttribute

        return description;
    }
}
