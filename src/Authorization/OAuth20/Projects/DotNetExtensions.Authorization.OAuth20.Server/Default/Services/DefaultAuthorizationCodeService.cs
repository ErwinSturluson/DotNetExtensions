// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataStorages;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Providers;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.AuthorizationCode.Authorize;
using DotNetExtensions.Authorization.OAuth20.Server.Models;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using Microsoft.Extensions.Options;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.Services;

public class DefaultAuthorizationCodeService : IAuthorizationCodeService
{
    private readonly IAuthorizationCodeStorage _authorizationCodeStorage;
    private readonly ITokenService _tokenService;
    private readonly IDateTimeService _dateTimeService;
    private readonly IEndUserService _endUserService;
    private readonly IAuthorizationCodeProvider _authorizationCodeProvider;
    private readonly IOptions<OAuth20ServerOptions> _options;

    public DefaultAuthorizationCodeService(
        IAuthorizationCodeStorage authorizationCodeStorage,
        ITokenService tokenService,
        IDateTimeService dateTimeService,
        IEndUserService endUserService,
        IAuthorizationCodeProvider authorizationCodeProvider,
        IOptions<OAuth20ServerOptions> options)
    {
        _authorizationCodeStorage = authorizationCodeStorage;
        _tokenService = tokenService;
        _dateTimeService = dateTimeService;
        _endUserService = endUserService;
        _authorizationCodeProvider = authorizationCodeProvider;
        _options = options;
    }

    public async Task<string> GetAuthorizationCodeAsync(AuthorizeArguments args, EndUser endUser, Client client, string redirectUri, string issuedScope, bool issuedScopeDifferent)
    {
        DateTime currentDateTime = _dateTimeService.GetCurrentDateTime();
        string authorizationCodeValue = _authorizationCodeProvider.GetAuthorizationCodeValue(args, endUser, client, redirectUri, issuedScope);

        AuthorizationCodeResult authorizationCode = new()
        {
            ClientId = client.ClientId,
            Username = endUser.Username,
            Exchanged = false,
            ExpiresIn = _options.Value.DefaultAuthorizationCodeExpirationSeconds ?? 60,
            IssueDateTime = currentDateTime,
            ExpirationDateTime = currentDateTime.AddSeconds(60),
            Scope = issuedScope,
            IssuedScopeDifferent = issuedScopeDifferent,
            RedirectUri = redirectUri,
            Value = authorizationCodeValue
        };

        await _authorizationCodeStorage.AddAuthorizationCodeResultAsync(authorizationCode);

        return authorizationCode.Value;
    }

    public async Task<AccessTokenResult> ExchangeAuthorizationCodeAsync(string code, Client client, string? redirectUri)
    {
        AuthorizationCodeResult? authorizationCode = await _authorizationCodeStorage.GetAuthorizationCodeResultAsync(code);
        if (authorizationCode is null)
        {
            // TODO: a more detailed error
            throw new InvalidOperationException($"{nameof(code)}:{code}");
        }

        DateTime currentDateTime = _dateTimeService.GetCurrentDateTime();
        if (authorizationCode.ExpirationDateTime >= currentDateTime)
        {
            // TODO: a more detailed error
            throw new InvalidOperationException($"{nameof(currentDateTime)}:{currentDateTime}");
        }

        if (authorizationCode.ClientId != client.ClientId)
        {
            // TODO: a more detailed error
            throw new InvalidOperationException($"{nameof(client.ClientId)}:{client.ClientId}");
        }

        if (authorizationCode.RedirectUri != redirectUri)
        {
            // TODO: a more detailed error
            throw new InvalidOperationException($"{nameof(redirectUri)}:{redirectUri}");
        }

        EndUser? endUser = await _endUserService.GetEndUserAsync(authorizationCode.Username);
        if (endUser is null)
        {
            // TODO: a more detailed error
            throw new InvalidOperationException($"{nameof(endUser.Username)}:{endUser?.Username}");
        }

        AccessTokenResult accessToken = await _tokenService.GetTokenAsync(
            authorizationCode.Scope,
            authorizationCode.IssuedScopeDifferent,
            client,
            authorizationCode.RedirectUri,
            endUser);

        return accessToken;
    }
}
