// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataStorages;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Providers;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Models;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using Microsoft.Extensions.Options;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.Services;

public class DefaultTokenService : ITokenService
{
    private readonly ITokenStorage _tokenStorage;
    private readonly IDateTimeService _dateTimeService;
    private readonly ITokenProvider _tokenProvider;
    private readonly IOptions<OAuth20ServerOptions> _options;

    public DefaultTokenService(
        ITokenStorage tokenStorage,
        IDateTimeService dateTimeService,
        ITokenProvider tokenProvider,
        IOptions<OAuth20ServerOptions> options)
    {
        _tokenStorage = tokenStorage;
        _dateTimeService = dateTimeService;
        _tokenProvider = tokenProvider;
        _options = options;
    }

    public async Task<Token> GetTokenAsync(ScopeResult scopeResult, EndUser endUser, Client client, string redirectUri)
    {
        DateTime currentDateTime = _dateTimeService.GetCurrentDateTime();
        string tokenType = _tokenProvider.GetTokenType(endUser, client, redirectUri);
        string tokenValue = _tokenProvider.GetTokenValue(tokenType, scopeResult, endUser, client, redirectUri);

        long? tokenExpirationSeconds = client.TokenExpirationSeconds ?? _options.Value.DefaultTokenExpirationSeconds;
        DateTime? expirationDateTime = tokenExpirationSeconds is null ? null : currentDateTime.AddSeconds(Convert.ToDouble(tokenExpirationSeconds));

        Token token = new()
        {
            ClientId = client.ClientId,
            Username = endUser.Username,
            ExpiresIn = tokenExpirationSeconds,
            IssuanceDateTime = currentDateTime,
            ExpirationDateTime = expirationDateTime,
            ScopeResult = scopeResult,
            RedirectUri = redirectUri,
            Value = tokenValue,
            Type = tokenType
        };

        await _tokenStorage.AddTokenAsync(token);

        return token;
    }
}
