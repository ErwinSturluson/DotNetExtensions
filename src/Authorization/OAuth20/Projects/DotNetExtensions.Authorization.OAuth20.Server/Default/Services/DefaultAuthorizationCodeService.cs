// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataStorages;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Providers;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.AuthorizationCode.Authorize;
using DotNetExtensions.Authorization.OAuth20.Server.Models;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.Services;

public class DefaultAuthorizationCodeService : IAuthorizationCodeService
{
    private readonly IAuthorizationCodeStorage _authorizationCodeStorage;
    private readonly ITokenService _tokenService;
    private readonly IDateTimeService _dateTimeService;
    private readonly IAuthorizationCodeProvider _authorizationCodeProvider;

    public DefaultAuthorizationCodeService(
        IAuthorizationCodeStorage authorizationCodeStorage,
        ITokenService tokenService,
        IDateTimeService dateTimeService,
        IAuthorizationCodeProvider authorizationCodeProvider)
    {
        _authorizationCodeStorage = authorizationCodeStorage;
        _tokenService = tokenService;
        _dateTimeService = dateTimeService;
        _authorizationCodeProvider = authorizationCodeProvider;
    }

    public async Task<string> GetAuthorizationCodeAsync(AuthorizeArguments args, EndUser endUser, Client client, string redirectUri, ScopeResult scopeResult)
    {
        DateTime currentDateTime = _dateTimeService.GetCurrentDateTime();
        string authorizationCodeValue = _authorizationCodeProvider.GetAuthorizationCodeValue(args, endUser, client, redirectUri, scopeResult); //  Guid.NewGuid().ToString();

        AuthorizationCode authorizationCode = new()
        {
            ClientId = client.ClientId,
            Username = endUser.Username,
            Exchanged = false,
            ExpiresIn = 60,
            IssuanceDateTime = currentDateTime,
            ExpirationDateTime = currentDateTime.AddSeconds(60),
            ScopeResult = scopeResult,
            RedirectUri = redirectUri,
            Value = authorizationCodeValue
        };

        await _authorizationCodeStorage.AddAuthorizationCodeAsync(authorizationCode);

        return authorizationCode.Value;
    }

    public async Task<Token> ExchangeAuthorizationCodeAsync(string code, EndUser endUser, Client client, string? redirectUri)
    {
        DateTime currentDateTime = _dateTimeService.GetCurrentDateTime();
        AuthorizationCode authorizationCode = await _authorizationCodeStorage.GetAuthorizationCodeAsync(code);

        if (authorizationCode.ExpirationDateTime >= currentDateTime)
        {
            throw new InvalidOperationException($"{nameof(currentDateTime)}:{currentDateTime}");
        }

        if (authorizationCode.Username != endUser.Username)
        {
            throw new InvalidOperationException($"{nameof(endUser.Username)}:{endUser.Username}");
        }

        if (authorizationCode.ClientId != client.ClientId)
        {
            throw new InvalidOperationException($"{nameof(client.ClientId)}:{client.ClientId}");
        }

        if (authorizationCode.RedirectUri != redirectUri)
        {
            throw new InvalidOperationException($"{nameof(redirectUri)}:{redirectUri}");
        }

        Token token = await _tokenService.GetTokenAsync(
            authorizationCode.ScopeResult,
            endUser,
            client,
            authorizationCode.RedirectUri);

        return token;
    }
}
