// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Flows.AuthorizationCode.Authorize;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.AuthorizationCode.Token;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using Microsoft.Extensions.Options;

namespace DotNetExtensions.Authorization.OAuth20.Server.Flows.AuthorizationCode;

/// <summary>
/// Description RFC6749: <see cref=""/>
/// </summary>
public class DefaultAuthorizationCodeFlow : IAuthorizationCodeFlow
{
    private readonly IOptions<OAuth20ServerOptions> _options;

    public DefaultAuthorizationCodeFlow(IOptions<OAuth20ServerOptions> options)
    {
        _options = options;
    }

    public Task<IResult> AuthorizeAsync(FlowArguments args)
    {
        var authArgs = AuthorizeArguments.Create(args);

        if (_options.Value.AuthorizationRequestStateRequired && authArgs.State is null)
        {
            throw new ArgumentNullException(nameof(authArgs.State));
        }

        throw new NotImplementedException();
    }

    public Task<IResult> GetTokenAsync(FlowArguments args)
    {
        var tokenArgs = TokenArguments.Create(args);

        throw new NotImplementedException();
    }
}
