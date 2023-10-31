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
    private readonly IScopeService _scopeService;
    private readonly IOptions<OAuth20ServerOptions> _options;

    public DefaultTokenService(
        ITokenStorage tokenStorage,
        IDateTimeService dateTimeService,
        ITokenProvider tokenProvider,
        IScopeService scopeService,
        IOptions<OAuth20ServerOptions> options)
    {
        _tokenStorage = tokenStorage;
        _dateTimeService = dateTimeService;
        _tokenProvider = tokenProvider;
        _scopeService = scopeService;
        _options = options;
    }

    public async Task<AccessTokenResult> GetTokenAsync(string issuedScope, bool issuedScopeDifferent, Client client, string redirectUri, EndUser? endUser = null)
    {
        DateTime currentDateTime = _dateTimeService.GetCurrentDateTime();
        TokenType tokenType = await _tokenProvider.GetTokenTypeAsync(client);
        IEnumerable<Scope> scopeList = await _scopeService.GetScopeListAsync(issuedScope);
        string tokenValue = _tokenProvider.GetTokenValue(tokenType, scopeList, client, redirectUri, endUser);

        long? tokenExpirationSeconds = client.TokenExpirationSeconds ?? _options.Value.DefaultTokenExpirationSeconds;
        DateTime? expirationDateTime = tokenExpirationSeconds is null ? null : currentDateTime.AddSeconds(Convert.ToDouble(tokenExpirationSeconds));

        AccessTokenResult token = new()
        {
            ClientId = client.ClientId,
            Username = endUser?.Username,
            ExpiresIn = tokenExpirationSeconds,
            IssueDateTime = currentDateTime,
            ExpirationDateTime = expirationDateTime,
            Scope = issuedScope,
            IssuedScopeDifferent = issuedScopeDifferent,
            RedirectUri = redirectUri,
            Value = tokenValue,
            Type = tokenType.Name
        };

        await _tokenStorage.AddTokenAsync(token);

        return token;
    }
}
