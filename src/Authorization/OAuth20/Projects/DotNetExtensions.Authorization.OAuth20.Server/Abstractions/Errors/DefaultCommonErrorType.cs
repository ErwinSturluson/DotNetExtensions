// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Options;
using System.Reflection;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors;

/// <summary>
/// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1
/// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-5.2
/// </summary>
public enum DefaultCommonErrorType
{
    /// <summary>
    /// The error is not defined. Invalid value for a response.
    /// </summary>
    [FieldName("undefined")]
    Undefined = 0,

    /// <summary>
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1
    /// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-5.2
    /// </summary>
    [FieldName("invalid_request")]
    InvalidRequest = 1,

    /// <summary>
    /// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-3.1.2.4"/>
    /// </summary>
    [FieldName("common_error")]
    CommonError = 2,
}

public static class DefaultCommonErrorTypeExtensions
{
    public static string GetDescriptionAttributeValue(this DefaultCommonErrorType defaultErrorType, OAuth20ServerOptions? options)
    {
        string errorCode = defaultErrorType switch
        {
            DefaultCommonErrorType.Undefined => GetDescriptionAttributeValue(defaultErrorType),
            DefaultCommonErrorType.InvalidRequest => options?.Errors?.CommonInvalidRequestErrorCode ?? GetDescriptionAttributeValue(defaultErrorType),
            DefaultCommonErrorType.CommonError => options?.Errors?.CommonErrorCode ?? GetDescriptionAttributeValue(defaultErrorType),
            _ => throw new NotSupportedException($"{nameof(defaultErrorType)}:{defaultErrorType}"),
        };

        return errorCode;
    }

    public static string GetDescriptionAttributeValue(this DefaultCommonErrorType defaultErrorType)
    {
        var member = typeof(DefaultCommonErrorType).GetMember(defaultErrorType.ToString()).First();
        string description = member.GetCustomAttribute<FieldNameAttribute>()!.Name;

        return description;
    }
}
