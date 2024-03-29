﻿// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors.Exceptions.Common;
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

    public DefaultTokenProvider(
        ITokenBuilderSelector tokenBuilderSelector,
        IClientService clientService)
    {
        _tokenBuilderSelector = tokenBuilderSelector;
        _clientService = clientService;
    }

    public async Task<TokenType> GetTokenTypeAsync(Client client)
    {
        return await _clientService.GetTokenType(client);
    }

    public async Task<string> GetTokenValueAsync(TokenType tokenType, TokenContext tokenContext)
    {
        if (!_tokenBuilderSelector.TryGetTokenBuilder(tokenType.Name, out ITokenBuilder? tokenBuilder))
        {
            throw new ServerConfigurationErrorException(
                $"There are no registered token builder for the specified token type " +
                $"[{tokenType.Name}] in this server instance.");
        }

        return await tokenBuilder!.BuildTokenAsync(tokenContext);
    }
}
