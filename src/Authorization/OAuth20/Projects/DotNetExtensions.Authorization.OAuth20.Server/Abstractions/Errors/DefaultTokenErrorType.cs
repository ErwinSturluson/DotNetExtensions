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
    [FieldName("undefined")]
    Undefined = 0,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-5.2
    /// </summary>
    [FieldName("invalid_request")]
    InvalidRequest = 1,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-5.2
    /// </summary>
    [FieldName("invalid_client")]
    InvalidClient = 2,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-5.2
    /// </summary>
    [FieldName("invalid_grant")]
    InvalidGrant = 3,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-5.2
    /// </summary>
    [FieldName("unauthorized_client")]
    UnauthorizedClient = 4,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-5.2
    /// </summary>
    [FieldName("unsupported_grant_type")]
    UnsupportedGrantType = 5,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-5.2
    /// </summary>
    [FieldName("invalid_scope")]
    InvalidScope = 6,
}

public static class DefaultErrorTypeExtensions
{
    public static string GetDescriptionAttributeValue(this DefaultTokenErrorType defaultErrorType, OAuth20ServerOptions? options)
    {
        string errorCode = defaultErrorType switch
        {
            DefaultTokenErrorType.Undefined => GetDescriptionAttributeValue(defaultErrorType),
            DefaultTokenErrorType.InvalidRequest => options?.Errors?.TokenInvalidRequestErrorCode ?? GetDescriptionAttributeValue(defaultErrorType),
            DefaultTokenErrorType.InvalidClient => options?.Errors?.TokenInvalidClientErrorCode ?? GetDescriptionAttributeValue(defaultErrorType),
            DefaultTokenErrorType.InvalidGrant => options?.Errors?.TokenInvalidGrantErrorCode ?? GetDescriptionAttributeValue(defaultErrorType),
            DefaultTokenErrorType.UnauthorizedClient => options?.Errors?.TokenUnauthorizedClientErrorCode ?? GetDescriptionAttributeValue(defaultErrorType),
            DefaultTokenErrorType.UnsupportedGrantType => options?.Errors?.TokenUnsupportedGrantTypeErrorCode ?? GetDescriptionAttributeValue(defaultErrorType),
            DefaultTokenErrorType.InvalidScope => options?.Errors?.TokenInvalidScopeErrorCode ?? GetDescriptionAttributeValue(defaultErrorType),
            _ => throw new NotSupportedException($"{nameof(defaultErrorType)}:{defaultErrorType}"),
        };

        return errorCode;
    }

    public static string GetDescriptionAttributeValue(this DefaultTokenErrorType defaultErrorType)
    {
        var member = typeof(DefaultTokenErrorType).GetMember(defaultErrorType.ToString()).First();
        string description = member.GetCustomAttribute<FieldNameAttribute>()!.Name;

        return description;
    }
}
