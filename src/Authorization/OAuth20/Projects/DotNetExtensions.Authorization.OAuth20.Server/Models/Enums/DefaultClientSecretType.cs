// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Attributes;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using System.Reflection;

namespace DotNetExtensions.Authorization.OAuth20.Server.Models.Enums;

/// <summary>
/// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-2.3.1"/>
/// </summary>
public enum DefaultClientSecretType
{
    /// <summary>
    /// The error is not defined. Invalid value for a response.
    /// </summary>
    [FieldName("undefined")]
    Undefined = 0,

    /// <summary>
    /// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1"/>
    /// Description RFC2617: <see cref="https://datatracker.ietf.org/doc/html/rfc2617"/>
    /// </summary>
    [FieldName("authorization_header_basic")]
    AuthorizationHeaderBasic = 1,

    /// <summary>
    /// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2.1"/>
    /// </summary>
    [FieldName("request_body_client_credentials")]
    RequestBodyClientCredentials = 2,
}

public static class DefaultClientSecretTypeExtensions
{
    public static string GetFieldNameAttributeValue(this DefaultClientSecretType defaultClientSecretTypeName, OAuth20ServerOptions? options)
    {
        string clientSecretTypeName = defaultClientSecretTypeName switch
        {
            DefaultClientSecretType.Undefined => defaultClientSecretTypeName.GetFieldNameAttributeValue(),
            DefaultClientSecretType.AuthorizationHeaderBasic => options?.ClientSecrets?.AuthorizationHeaderBasicClientSecretTypeName ?? defaultClientSecretTypeName.GetFieldNameAttributeValue(),
            DefaultClientSecretType.RequestBodyClientCredentials => options?.ClientSecrets?.RequestBodyClientCredentialsClientSecretTypeName ?? defaultClientSecretTypeName.GetFieldNameAttributeValue(),
            _ => throw new NotSupportedException($"{nameof(defaultClientSecretTypeName)}:{defaultClientSecretTypeName}"),
        };

        return clientSecretTypeName;
    }

    public static string GetFieldNameAttributeValue(this DefaultClientSecretType defaultClientSecretTypeName)
    {
        var member = typeof(DefaultClientSecretType).GetMember(defaultClientSecretTypeName.ToString()).First();
        string description = member.GetCustomAttribute<FieldNameAttribute>()!.Name;

        return description;
    }
}
