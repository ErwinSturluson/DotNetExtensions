// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Models;
using System.Text;

namespace DotNetExtensions.Authorization.OAuth20.Server.TokenBuilders.Basic;

/// <summary>
/// Description RFC7519: <see cref="https://datatracker.ietf.org/doc/html/rfc7617"/>
/// </summary>
public class DefaultBasicTokenBuilder : IBasicTokenBuilder
{
    public Task<string> BuildTokenAsync(TokenContext tokenBuilderContext)
    {
        StringBuilder sb = new("Type:Basic::Encoding:Base64");

        if (tokenBuilderContext.Issuer is not null)
            sb.AppendFormat("::Issuer:{0}", tokenBuilderContext.Issuer);

        if (tokenBuilderContext.Audiences is null || tokenBuilderContext.Audiences.Any())
        {
            throw new Exception(); // TODO: detailed error
        }

        sb.Append("::Audience");

        foreach (var audience in tokenBuilderContext.Audiences)
        {
            sb.AppendFormat($":{0}", audience);
        }

        if (tokenBuilderContext.Scopes is null || tokenBuilderContext.Scopes.Any())
        {
            throw new Exception(); // TODO: detailed error
        }

        sb.Append("::Scope");

        foreach (Scope scope in tokenBuilderContext.Scopes)
        {
            sb.AppendFormat($":{0}", scope.Name);
        }

        if (tokenBuilderContext.ActivationDateTime is not null)
            sb.AppendFormat("::NotBefore:{0}", tokenBuilderContext.ActivationDateTime?.UtcDateTime.ToUniversalTime());

        if (tokenBuilderContext.ExpirationDateTime is not null)
            sb.AppendFormat("::Expires:{0}", tokenBuilderContext.ExpirationDateTime?.UtcDateTime.ToUniversalTime());

        if (tokenBuilderContext.CreationDateTime is not null)
            sb.AppendFormat("::IssuedAt:{0}", tokenBuilderContext.CreationDateTime?.UtcDateTime.ToUniversalTime());

        string originAccessToken = sb.ToString();
        byte[] binaryAccessToken = Encoding.ASCII.GetBytes(originAccessToken);
        string accessToken = Convert.ToBase64String(binaryAccessToken);

        return Task.FromResult(accessToken);
    }
}
