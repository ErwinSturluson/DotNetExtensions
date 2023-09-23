// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Options;
using System.ComponentModel;
using System.Reflection;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors;

/// <summary>
/// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1
/// </summary>
public enum DefaultAuthorizeErrorType
{
    /// <summary>
    /// The error is not defined. Invalid value for a response.
    /// </summary>
    [FieldName("undefined")]
    Undefined = 0,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1
    /// </summary>
    [FieldName("invalid_request")]
    InvalidRequest = 1,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1
    /// </summary>
    [FieldName("unauthorized_client")]
    UnauthorizedClient = 2,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1
    /// </summary>
    [FieldName("access_denied")]
    AccessDenied = 3,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1
    /// </summary>
    [FieldName("unsupported_response_type")]
    UnsupportedResponseType = 4,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1
    /// </summary>
    [FieldName("invalid_scope")]
    InvalidScope = 5,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1
    /// </summary>
    [FieldName("server_error")]
    ServerError = 6,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1
    /// </summary>
    [FieldName("temporarily_unavailable")]
    TemporarilyUnavailable = 7,
}

public static class DefaultAuthorizeErrorTypeExtensions
{
    public static string GetDescriptionAttributeValue(this DefaultAuthorizeErrorType defaultErrorType, OAuth20ServerOptions? options)
    {
        string errorCode = defaultErrorType switch
        {
            DefaultAuthorizeErrorType.Undefined => GetDescriptionAttributeValue(defaultErrorType),
            DefaultAuthorizeErrorType.InvalidRequest => options?.Errors?.AuthorizeInvalidRequestErrorCode ?? GetDescriptionAttributeValue(defaultErrorType),
            DefaultAuthorizeErrorType.UnauthorizedClient => options?.Errors?.AuthorizeUnauthorizedClientErrorCode ?? GetDescriptionAttributeValue(defaultErrorType),
            DefaultAuthorizeErrorType.AccessDenied => options?.Errors?.AuthorizeAccessDeniedErrorCode ?? GetDescriptionAttributeValue(defaultErrorType),
            DefaultAuthorizeErrorType.UnsupportedResponseType => options?.Errors?.AuthorizeUnsupportedResponseTypeErrorCode ?? GetDescriptionAttributeValue(defaultErrorType),
            DefaultAuthorizeErrorType.InvalidScope => options?.Errors?.AuthorizeInvalidScopeErrorCode ?? GetDescriptionAttributeValue(defaultErrorType),
            DefaultAuthorizeErrorType.ServerError => options?.Errors?.AuthorizeServerErrorErrorCode ?? GetDescriptionAttributeValue(defaultErrorType),
            DefaultAuthorizeErrorType.TemporarilyUnavailable => options?.Errors?.AuthorizeTemporarilyUnavailableErrorCode ?? GetDescriptionAttributeValue(defaultErrorType),
            _ => throw new NotSupportedException($"{nameof(defaultErrorType)}:{defaultErrorType}"),
        };

        return errorCode;
    }

    public static string GetDescriptionAttributeValue(this DefaultAuthorizeErrorType defaultErrorType)
    {
        var member = typeof(DefaultAuthorizeErrorType).GetMember(defaultErrorType.ToString()).First();
        string description = member.GetCustomAttribute<FieldNameAttribute>()!.Name;

        return description;
    }
}
