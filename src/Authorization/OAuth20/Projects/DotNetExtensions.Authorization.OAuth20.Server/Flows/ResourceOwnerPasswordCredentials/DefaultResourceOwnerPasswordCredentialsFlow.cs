// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Flows.ResourceOwnerPasswordCredentials.Token;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using Microsoft.Extensions.Options;

namespace DotNetExtensions.Authorization.OAuth20.Server.Flows.ResourceOwnerPasswordCredentials;

/// <summary>
/// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-4.3"/>
/// </summary>
public class DefaultResourceOwnerPasswordCredentialsFlow : IResourceOwnerPasswordCredentialsFlow
{
    private readonly IOptions<OAuth20ServerOptions> _options;

    public DefaultResourceOwnerPasswordCredentialsFlow(IOptions<OAuth20ServerOptions> options)
    {
        _options = options;
    }

    public Task<IResult> GetTokenAsync(FlowArguments args)
    {
        TokenArguments tokenArgs = TokenArguments.Create(args);

        throw new NotImplementedException();
    }
}
