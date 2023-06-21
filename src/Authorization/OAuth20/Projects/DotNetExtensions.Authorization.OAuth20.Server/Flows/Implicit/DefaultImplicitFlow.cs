// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.Implicit.Mixed;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using Microsoft.Extensions.Options;

namespace DotNetExtensions.Authorization.OAuth20.Server.Flows.Implicit;

/// <summary>
/// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-4.2"/>
/// </summary>
public class DefaultImplicitFlow : IImplicitFlow
{
    private readonly IOptions<OAuth20ServerOptions> _options;
    private readonly IErrorResultProvider _errorResultProvider;

    public DefaultImplicitFlow(IOptions<OAuth20ServerOptions> options, IErrorResultProvider errorResultProvider)
    {
        _options = options;
        _errorResultProvider = errorResultProvider;
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
}
