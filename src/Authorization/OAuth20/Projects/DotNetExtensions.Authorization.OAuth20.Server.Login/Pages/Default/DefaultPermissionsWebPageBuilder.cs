// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.WebPageBuilders;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Login.Pages.Abstractions;
using System.Text.RegularExpressions;

namespace DotNetExtensions.Authorization.OAuth20.Server.Login.Pages.Default;

public partial class DefaultPermissionsWebPageBuilder : IPermissionsWebPageBuilder
{
    private const string _htmlContent =
"""
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Permissions - OAuth 2.0 Authorization Server</title>
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <style>
        /* Custom CSS */
        .max-width-container {
            max-width: 440px;
            margin: 0 auto;
        }

        .input-spacing {
            margin-bottom: 20px;
        }

        .header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 0 1rem;
        }

        .header-left, .header-right a, .header-right div {
            font-size: 1.2em;
        }

        .header-right {
            display: flex;
            align-items: center;
        }

        .bold-text {
            font-weight: bold;
        }

        .username {
            margin-right: 20px;
        }

        .input-btn {
            margin-top: 40px;
        }
    </style>
</head>
<body>
    <header class="bg-primary text-white py-3 header">
        <div class="header-left">
            <a href="/" class="text-white bold-text">OAuth 2.0 Authorization Server</a>
        </div>
        <div class="header-right">
            <div class="username text-white bold-text">
                Welcome, {{username}}
            </div>
            <div>
                <button id="logout-button" class="btn btn-danger btn-sm bold-text">Logout</button>
            </div>
        </div>
    </header>

    <div class="container mt-5 max-width-container">
        <h4 class="text-justify">Select the permissions you allow of requested by {{clientName}} application:</h4>
        <form class="mt-4" action="/endpoint/permissions{{query_string}}"" method="post">
            {{permissionsList}}

            <button class="btn btn-primary btn-block input-btn" type="submit">Grant Permissions</button>
        </form>
    </div>

    <script>
        document.getElementById("logout-button").addEventListener("click", function () {
            window.location.href = "endpoint/logout";
        });
    </script>
</body>
</html>
""";

    private const string _htmlElementPermission =
"""
<div class="form-check input-spacing">
                <input type="checkbox" class="form-check-input" id="{{permissionName}}" name="{{permissionName}}">
                <label class="form-check-label" for="{{permissionName}}">{{permissionName}}</label>
            </div>
""";

    private readonly IPermissionsService _permissionsService;
    private readonly IEndUserService _endUserService;
    private readonly IClientService _clientService;

    public DefaultPermissionsWebPageBuilder(
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
        if (httpContext.Request.Method != HttpMethods.Get)
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

        Dictionary<string, string> values = httpContext.Request.Query.ToDictionary(x => x.Key, x => x.Value.First()!);

        if (!values.TryGetValue("client_id", out string? clientId))
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

        IEnumerable<string> permissions = permissionsRequest.IssuedScope.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        string permissionsHtmlString = string.Empty;

        foreach (var permission in permissions)
        {
            permissionsHtmlString += PermissionNameRegex().Replace(_htmlElementPermission, permission);
        }

        string htmlResultString = PermissionsListRegex().Replace(_htmlContent, permissionsHtmlString);
        htmlResultString = ClientNameRegex().Replace(htmlResultString, client.ClientId);
        htmlResultString = UsernameRegex().Replace(htmlResultString, endUser.Username);

        string queryString = httpContext.Request.QueryString.ToString();
        htmlResultString = htmlResultString.Replace("{{query_string}}", queryString);

        HtmlPageResult htmlPageResult = new(htmlResultString);

        return htmlPageResult;
    }

    [GeneratedRegex("{{permissionName}}", RegexOptions.Compiled)]
    private static partial Regex PermissionNameRegex();

    [GeneratedRegex("{{permissionsList}}", RegexOptions.Compiled)]
    private static partial Regex PermissionsListRegex();

    [GeneratedRegex("{{clientName}}", RegexOptions.Compiled)]
    private static partial Regex ClientNameRegex();

    [GeneratedRegex("{{username}}", RegexOptions.Compiled)]
    private static partial Regex UsernameRegex();
}
