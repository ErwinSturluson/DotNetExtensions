// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;
using DotNetExtensions.Authorization.OAuth20.Server.Flows;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using Microsoft.Extensions.Options;

namespace DotNetExtensions.Authorization.OAuth20.Server.Services;

public class DefaultLoginService : ILoginService
{
    private readonly IOptions<OAuth20ServerOptions> _options;
    private readonly IClientService _clientService;
    private readonly IErrorResultProvider _errorResultProvider;

    public DefaultLoginService(IOptions<OAuth20ServerOptions> options, IClientService clientService, IErrorResultProvider errorResultProvider)
    {
        _options = options;
        _clientService = clientService;
        _errorResultProvider = errorResultProvider;
    }

    public async Task<IResult> RedirectToLoginAsync(FlowArguments args)
    {
        if (!args.Values.TryGetValue("state", out string? state) && _options.Value.AuthorizationRequestStateRequired)
        {
            return _errorResultProvider.GetAuthorizeErrorResult(DefaultAuthorizeErrorType.InvalidRequest, state: null, "Missing request parameter: [state]", _options.Value);
        }

        if (!args.Values.TryGetValue("client_id", out string? clientId))
        {
            return _errorResultProvider.GetAuthorizeErrorResult(DefaultAuthorizeErrorType.InvalidRequest, state: state, "Missing request parameter: [client_id]", _options.Value);
        }

        var client = await _clientService.GetClientAsync(clientId);
        if (client is null)
        {
            return _errorResultProvider.GetAuthorizeErrorResult(DefaultAuthorizeErrorType.UnauthorizedClient, state: state, $"Client with [client_id] = [{clientId}] doesn't exist.", _options.Value);
        }

        string? loginEndpoint = client.LoginEndpoint ?? _options.Value.DefaultLoginEndpoint;
        if (loginEndpoint is null)
        {
            return _errorResultProvider.GetAuthorizeErrorResult(DefaultAuthorizeErrorType.ServerError, state: state, nameof(loginEndpoint), _options.Value);
        }

        LoginRedirectResult result = new(loginEndpoint, args);

        return result;
    }
}
