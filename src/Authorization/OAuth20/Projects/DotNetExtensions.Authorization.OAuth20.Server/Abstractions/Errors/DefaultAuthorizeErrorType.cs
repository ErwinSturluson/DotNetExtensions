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
    [Description("unauthorized_client")]
    UnauthorizedClient = 2,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1
    /// </summary>
    [Description("access_denied")]
    AccessDenied = 3,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1
    /// </summary>
    [Description("unsupported_response_type")]
    UnsupportedResponseType = 4,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1
    /// </summary>
    [Description("invalid_scope")]
    InvalidScope = 5,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1
    /// </summary>
    [Description("server_error")]
    ServerError = 6,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1
    /// </summary>
    [Description("temporarily_unavailable")]
    TemporarilyUnavailable = 7,
}

public static class DefaultAuthorizeErrorTypeExtensions
{
    public static string GetDescriptionAttributeValue(this DefaultAuthorizeErrorType defaultErrorType, OAuth20ServerOptions? options)
    {
        string errorCode = defaultErrorType switch
        {
            DefaultAuthorizeErrorType.Undefined => GetDescriptionAttributeValue(defaultErrorType),
            DefaultAuthorizeErrorType.InvalidRequest => options?.AuthorizeInvalidRequestErrorCode ?? GetDescriptionAttributeValue(defaultErrorType),
            DefaultAuthorizeErrorType.UnauthorizedClient => options?.AuthorizeUnauthorizedClientErrorCode ?? GetDescriptionAttributeValue(defaultErrorType),
            DefaultAuthorizeErrorType.AccessDenied => options?.AuthorizeAccessDeniedErrorCode ?? GetDescriptionAttributeValue(defaultErrorType),
            DefaultAuthorizeErrorType.UnsupportedResponseType => options?.AuthorizeUnsupportedResponseTypeErrorCode ?? GetDescriptionAttributeValue(defaultErrorType),
            DefaultAuthorizeErrorType.InvalidScope => options?.AuthorizeInvalidScopeErrorCode ?? GetDescriptionAttributeValue(defaultErrorType),
            DefaultAuthorizeErrorType.ServerError => options?.AuthorizeServerErrorErrorCode ?? GetDescriptionAttributeValue(defaultErrorType),
            DefaultAuthorizeErrorType.TemporarilyUnavailable => options?.AuthorizeTemporarilyUnavailableErrorCode ?? GetDescriptionAttributeValue(defaultErrorType),
            _ => throw new NotSupportedException($"{nameof(defaultErrorType)}:{defaultErrorType}"),
        };

        return errorCode;
    }

    public static string GetDescriptionAttributeValue(this DefaultAuthorizeErrorType defaultErrorType)
    {
        var member = typeof(DefaultAuthorizeErrorType).GetMember(defaultErrorType.ToString()).First();
        string description = member.GetCustomAttribute<DescriptionAttribute>()!.Description; // TODO: use another attribute than DescriptionAttribute

        return description;
    }
}
