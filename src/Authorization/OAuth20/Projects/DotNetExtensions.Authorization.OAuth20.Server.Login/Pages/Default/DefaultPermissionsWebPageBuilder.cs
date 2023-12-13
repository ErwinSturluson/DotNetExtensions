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
    <title>Checkbox Form</title>
    <style>
        body {
            margin: 0;
            font-family: Arial, sans-serif;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            background-color: #f0f0f0;
        }

        .checkbox-container {
            background-color: #fff;
            padding: 20px;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            display: flex;
            flex-direction: column;
            align-items: center;
        }

        .checkbox-form {
            width: 100%;
            max-width: 300px;
            display: flex;
            flex-direction: column;
            align-items: flex-start;
        }

        .checkbox-group {
            margin-bottom: 15px;
        }

            .checkbox-group label {
                margin-bottom: 5px;
                display: block;
            }

            .checkbox-group input[type="checkbox"] {
                margin-right: 5px;
            }

        button {
            padding: 10px;
            border: none;
            border-radius: 3px;
            background-color: #007bff;
            color: #fff;
            cursor: pointer;
            transition: background-color 0.3s;
            width: 100%;
            max-width: 200px;
        }

            button:hover {
                background-color: #0056b3;
            }
    </style>
</head>
<body>
    <div class="checkbox-container">
        <form class="checkbox-form" action="/endpoint/permissions{{query_string}}"" method="post">
            <h2><b>{{username}}</b>, application <b>{{clientName}}</b> requests the following permissions:</h2>
            {{permissionsList}}
            <button type="submit">Grant selected permissions</button>
        </form>
    </div>
</body>
</html>
""";

    private const string _htmlElementPermission =
"""
<div class="checkbox-group">
    <label for="{{permissionName}}">
        <input type="checkbox" id="{{permissionName}}" name="{{permissionName}}">
        {{permissionName}}
    </label>
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
