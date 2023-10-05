// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Providers;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.TokenBuilders;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Models;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.Providers;

public class DefaultTokenProvider : ITokenProvider
{
    private readonly ITokenBuilderSelector _tokenBuilderSelector;
    private readonly IClientService _clientService;

    public DefaultTokenProvider(ITokenBuilderSelector tokenBuilderSelector, IClientService clientService)
    {
        _tokenBuilderSelector = tokenBuilderSelector;
        _clientService = clientService;
    }

    public async Task<string> GetTokenTypeAsync(EndUser endUser, Client client, string redirectUri)
    {
        var tokenType = await _clientService.GetTokenType(client);

        return tokenType.Name;
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
