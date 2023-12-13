/// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;
using DotNetExtensions.Authorization.OAuth20.Server.Default.Endpoints;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Login.Endpoint.Abstractions;
using DotNetExtensions.Authorization.OAuth20.Server.Models;
using Microsoft.AspNetCore.WebUtilities;

namespace DotNetExtensions.Authorization.OAuth20.Server.Login.Endpoint.Default;

public class DefaultPermissionsEndpoint : IPermissionsEndpoint
{
    private readonly IPermissionsService _permissionsService;
    private readonly IEndUserService _endUserService;
    private readonly IClientService _clientService;

    public DefaultPermissionsEndpoint(
        IPermissionsService permissionsService,
        IEndUserService endUserService,
        IClientService clientService)
    {
        _permissionsService = permissionsService;
        _endUserService = endUserService;
        _clientService = clientService;
    }

    public async Task<IResult> InvokeAsync(HttpContext httpContext)
    {
        if (httpContext.Request.Method != HttpMethods.Post)
        {
            throw new Exception(); // TODO: detailed error
        }

        if (!_endUserService.IsAuthenticated())
        {
            throw new Exception(); // TODO: detailed error or redirect to another page
        }

        var endUser = await _endUserService.GetCurrentEndUserAsync();
        if (endUser is null)
        {
            throw new Exception(); // TODO: detailed error
        }

        Dictionary<string, string> queryValues = httpContext.Request.Query.ToDictionary(x => x.Key, x => x.Value.First()!);

        if (!queryValues.TryGetValue("client_id", out string? clientId))
        {
            throw new Exception(); // TODO: detailed error
        }

        Client? client = await _clientService.GetClientAsync(clientId);
        if (client is null)
        {
            throw new Exception(); // TODO: detailed error
        }

        var permissionsRequest = await _permissionsService.GetPermissionsRequestAsync(endUser, client);
        if (permissionsRequest is null)
        {
            throw new Exception(); // TODO: detailed error
        }

        IEnumerable<string> serverAllowedScopes = permissionsRequest.IssuedScope.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        IEnumerable<string> endUserIssuedScopes = httpContext.Request.Form.Select(x => x.Key);

        IEnumerable<string> invalidScopes = endUserIssuedScopes.Except(serverAllowedScopes);
        if (invalidScopes.Any())
        {
            throw new Exception();
        }

        string endUserissuedScope = string.Join(' ', endUserIssuedScopes);

        ScopeResult permissionsResult = new()
        {
            RequestedScope = permissionsRequest.RequestedScope,
            IssuedScopeDifferent = permissionsRequest.IssuedScopeDifferent,
            IssuedScope = endUserissuedScope
        };

        await _permissionsService.AddPermissionsResultAsync(permissionsResult, endUser, client);

        Dictionary<string, string>? queryParameters = null;

        if (httpContext.Request.QueryString.HasValue)
        {
            queryParameters = QueryHelpers.ParseQuery(httpContext.Request.QueryString.Value)
                .ToDictionary(x => x.Key, x => x.Value.ToString());
        }

        IResult result = new DefaultRedirectResult("/oauth/authorize", queryParameters);

        return result;
    }
}
