// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataStorages;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Providers;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Models;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.Services;

public class DefaultTokenService : ITokenService
{
    private readonly ITokenStorage _tokenStorage;
    private readonly IDateTimeService _dateTimeService;
    private readonly ITokenProvider _tokenProvider;

    public DefaultTokenService(ITokenStorage tokenStorage, IDateTimeService dateTimeService, ITokenProvider tokenProvider)
    {
        _tokenStorage = tokenStorage;
        _dateTimeService = dateTimeService;
        _tokenProvider = tokenProvider;
    }

    public async Task<Token> GetTokenAsync(ScopeResult scopeResult, EndUser endUser, Client client, string redirectUri)
    {
        DateTime currentDateTime = _dateTimeService.GetCurrentDateTime();
        string tokenValue = _tokenProvider.GetTokenValue(scopeResult, endUser, client, redirectUri); // TODO: determining of the reuired token type
        string tokenType = _tokenProvider.GetTokenType(endUser, client, redirectUri); // TODO:

        Token token = new()
        {
            ClientId = client.ClientId,
            Username = endUser.Username,
            ExpiresIn = 60,
            IssuanceDateTime = currentDateTime,
            ExpirationDateTime = currentDateTime.AddSeconds(60),
            ScopeResult = scopeResult,
            RedirectUri = redirectUri,
            Value = tokenValue,
            Type = tokenType
        };

        await _tokenStorage.AddTokenAsync(token);

        return token;
    }
}
