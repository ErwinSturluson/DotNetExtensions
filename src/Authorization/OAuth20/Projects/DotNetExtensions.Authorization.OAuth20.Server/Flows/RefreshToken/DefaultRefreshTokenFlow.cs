// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.RefreshToken.Token;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.ResourceOwnerPasswordCredentials;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using Microsoft.Extensions.Options;

namespace DotNetExtensions.Authorization.OAuth20.Server.Flows.RefreshToken;

/// <summary>
/// Description RFC6749: https://datatracker.ietf.org/doc/html/rfc6749#section-1.5
/// </summary>
public class DefaultRefreshTokenFlow : IRefreshTokenFlow
{
    private readonly IOptions<OAuth20ServerOptions> _options;
    private readonly IErrorResultProvider _errorResultProvider;

    public DefaultRefreshTokenFlow(IOptions<OAuth20ServerOptions> options, IErrorResultProvider errorResultProvider)
    {
        _options = options;
        _errorResultProvider = errorResultProvider;
    }

    public async Task<IResult> GetTokenAsync(FlowArguments args)
    {
        TokenArguments tokenArgs = TokenArguments.Create(args);

        var result = await GetTokenAsync(tokenArgs);

        return result;
    }

    public Task<TokenResult> GetTokenAsync(TokenArguments args)
    {
        throw new NotImplementedException();
    }
}
