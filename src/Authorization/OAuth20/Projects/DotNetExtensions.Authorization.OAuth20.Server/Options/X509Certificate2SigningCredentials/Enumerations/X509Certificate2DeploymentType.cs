// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Attributes;
using System.Reflection;

namespace DotNetExtensions.Authorization.OAuth20.Server.Options.X509Certificate2SigningCredentials.Enumerations;

public enum X509Certificate2DeploymentType
{
    [FieldName("undefined")]
    Undefined = 0,

    [FieldName("text")]
    Text = 1,

    [FieldName("file")]
    File = 2,
}

public static class X509Certificate2DeploymentTypeExtensions
{
    public static string GetFieldNameAttributeValue(this X509Certificate2DeploymentType x509Certificate2DeploymentTypeName, OAuth20ServerOptions? options)
    {
        string clientSecretTypeName = x509Certificate2DeploymentTypeName switch
        {
            X509Certificate2DeploymentType.Undefined => x509Certificate2DeploymentTypeName.GetFieldNameAttributeValue(),
            X509Certificate2DeploymentType.Text => options?.SigningCredentials?.X509Certificate2DeploymentTypeTextName ?? x509Certificate2DeploymentTypeName.GetFieldNameAttributeValue(),
            X509Certificate2DeploymentType.File => options?.SigningCredentials?.X509Certificate2DeploymentTypeFileName ?? x509Certificate2DeploymentTypeName.GetFieldNameAttributeValue(),
            _ => throw new NotSupportedException($"{nameof(x509Certificate2DeploymentTypeName)}:{x509Certificate2DeploymentTypeName}"),
        };

        return clientSecretTypeName;
    }

    public static string GetFieldNameAttributeValue(this X509Certificate2DeploymentType x509Certificate2DeploymentTypeName)
    {
        var member = typeof(X509Certificate2DeploymentType).GetMember(x509Certificate2DeploymentTypeName.ToString()).First();
        string description = member.GetCustomAttribute<FieldNameAttribute>()!.Name;

        return description;
    }
}
