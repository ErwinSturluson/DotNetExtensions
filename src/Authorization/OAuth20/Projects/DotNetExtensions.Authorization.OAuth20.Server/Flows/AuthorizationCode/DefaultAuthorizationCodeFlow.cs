// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.AuthorizationCode.Authorize;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.AuthorizationCode.Token;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.Implicit;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using Microsoft.Extensions.Options;

namespace DotNetExtensions.Authorization.OAuth20.Server.Flows.AuthorizationCode;

/// <summary>
/// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-4.1"/>
/// </summary>
public class DefaultAuthorizationCodeFlow : IAuthorizationCodeFlow
{
    private readonly IOptions<OAuth20ServerOptions> _options;
    private readonly IErrorResultProvider _errorResultProvider;

    public DefaultAuthorizationCodeFlow(IOptions<OAuth20ServerOptions> options, IErrorResultProvider errorResultProvider)
    {
        _options = options;
        _errorResultProvider = errorResultProvider;
    }

    public async Task<IResult> AuthorizeAsync(FlowArguments args)
    {
        var authArgs = AuthorizeArguments.Create(args);

        if (_options.Value.AuthorizationRequestStateRequired && authArgs.State is null)
        {
            throw new ArgumentNullException(nameof(authArgs.State));
        }

        var result = await AuthorizeAsync(authArgs);

        return result;
    }

    public async Task<IResult> GetTokenAsync(FlowArguments args)
    {
        var tokenArgs = TokenArguments.Create(args);

        var result = await ((IAuthorizationCodeFlow)this).GetTokenAsync(tokenArgs);

        return result;
    }

    public Task<AuthorizeResult> AuthorizeAsync(AuthorizeArguments args)
    {
        throw new NotImplementedException();
    }

    public Task<TokenResult> GetTokenAsync(TokenArguments args)
    {
        throw new NotImplementedException();
    }
}
