// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Providers;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.TokenBuilders;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;

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

    public async Task<TokenType> GetTokenTypeAsync(Client client)
    {
        return await _clientService.GetTokenType(client);
    }

    public string GetTokenValue(TokenType tokenType, IEnumerable<Scope> scope, Client client, string redirectUri, EndUser? endUser = null)
    {
        if (!_tokenBuilderSelector.TryGetTokenBuilder(tokenType.Name, out ITokenBuilder? tokenBuilder))
        {
            throw new NotSupportedException($"{nameof(tokenType)}{tokenType}");
        }

        return tokenBuilder!.BuildToken(null!);
    }
}
