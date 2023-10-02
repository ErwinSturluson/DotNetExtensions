// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Providers;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.TokenBuilders;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Models;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.Providers;

public class DefaultTokenProvider : ITokenProvider
{
    private readonly ITokenBuilderSelector _tokenBuilderSelector;

    public DefaultTokenProvider(ITokenBuilderSelector tokenBuilderSelector)
    {
        _tokenBuilderSelector = tokenBuilderSelector;
    }

    public string GetTokenType(EndUser endUser, Client client, string redirectUri)
    {
        return client.TokenType;
    }

    public string GetTokenValue(string tokenType, ScopeResult scopeResult, EndUser endUser, Client client, string redirectUri)
    {
        if (!_tokenBuilderSelector.TryGetTokenBuilder(tokenType, out ITokenBuilder? tokenBuilder))
        {
            throw new NotSupportedException($"{nameof(tokenType)}{tokenType}");
        }

        return tokenBuilder!.BuildToken(null!);
    }
}
