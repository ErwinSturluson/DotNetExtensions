﻿// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Providers;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.AuthorizationCode.Authorize;
using System.Text;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.Providers;

public class EncryptedGuidAuthorizationCodeProvider : IAuthorizationCodeProvider
{
    private static readonly string _authorizationCodeSalt = Guid.NewGuid().ToString("N");
    private static readonly string _encryptionKey = Guid.NewGuid().ToString("N");

    public string GetAuthorizationCodeValue(AuthorizeArguments args, EndUser endUser, Client client, string redirectUri, string scope)
    {
        string guid = Guid.NewGuid().ToString("N");

        string originCode = $"{_authorizationCodeSalt}{guid}";

        StringBuilder sb = new();

        for (int i = 0, j = 0; i < originCode.Length; i++, j++)
        {
            if (j == _encryptionKey.Length) j = 0;

            int encryptedSymbol = originCode[i] ^ _encryptionKey[j];

            sb.Append(encryptedSymbol);
        }

        string encryptedCode = sb.ToString();

        return encryptedCode;
    }
}
