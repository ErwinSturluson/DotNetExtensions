// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.ClientCredentials.Token;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.ResourceOwnerPasswordCredentials;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using Microsoft.Extensions.Options;

namespace DotNetExtensions.Authorization.OAuth20.Server.Flows.ClientCredentials;

/// <summary>
/// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-4.4"/>
/// </summary>
public class DefaultClientCredentialsFlow : IClientCredentialsFlow
{
    private readonly IOptions<OAuth20ServerOptions> _options;
    private readonly IErrorResultProvider _errorResultProvider;

    public DefaultClientCredentialsFlow(IOptions<OAuth20ServerOptions> options, IErrorResultProvider errorResultProvider)
    {
        _options = options;
        _errorResultProvider = errorResultProvider;
    }

    public async Task<IResult> GetTokenAsync(FlowArguments args)
    {
        var tokenArgs = TokenArguments.Create(args);

        var result = await ((IClientCredentialsFlow)this).GetTokenAsync(tokenArgs);

        return result;
    }

    Task<TokenResult> IClientCredentialsFlow.GetTokenAsync(TokenArguments args)
    {
        throw new NotImplementedException();
    }
}
