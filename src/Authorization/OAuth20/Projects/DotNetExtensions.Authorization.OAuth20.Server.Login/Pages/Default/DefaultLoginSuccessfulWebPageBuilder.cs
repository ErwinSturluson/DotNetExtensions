// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.WebPageBuilders;
using DotNetExtensions.Authorization.OAuth20.Server.Login.Pages.Abstractions;
using Microsoft.AspNetCore.WebUtilities;

namespace DotNetExtensions.Authorization.OAuth20.Server.Login.Pages.Default;

public class DefaultLoginSuccessfulWebPageBuilder : ILoginSuccessfulWebPageBuilder
{
    private static readonly string _htmlContent =
"""
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Login Page</title>
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

        .login-container {
            background-color: #fff;
            padding: 20px;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        .login-form {
            display: flex;
            flex-direction: column;
        }

            .login-form h2 {
                text-align: center;
                margin-bottom: 20px;
            }

        .input-group {
            margin-bottom: 15px;
        }

            .input-group label {
                font-weight: bold;
                margin-bottom: 5px;
                display: block;
            }

            .input-group input {
                padding: 8px;
                border-radius: 3px;
                border: 1px solid #ccc;
                width: 100%;
            }

        button {
            padding: 10px;
            border: none;
            border-radius: 3px;
            background-color: #007bff;
            color: #fff;
            cursor: pointer;
            transition: background-color 0.3s;
        }

            button:hover {
                background-color: #0056b3;
            }
    </style>
</head>
<body>
    <div class="login-container">
            <h2>Welcome {{username}}, you are successfully logged in!</h2>
            {{oauth20_server_redirect}}
    </div>
</body>
</html>
""";

    private static readonly string _htmlRedirectElement =
        """
            <form class="login-form" action="{{redirectLocation}}" method="get">
                <label for="param1">Parameter 1:</label>
                <input type="text" id="param1" name="param1">
                <br>
                <label for="param2">Parameter 2:</label>
                <input type="text" id="param2" name="param2">
                <br>
                <input type="submit" value="Submit">Return to the Authorization</input>
            </form>
        """;

    public Task<IResult> InvokeAsync(HttpContext httpContext)
    {
        if (httpContext.Request.Method != HttpMethods.Get)
        {
            throw new Exception(); // TODO: detailed error
        }

        string oauth20ServerRedirect = string.Empty;

        if (!httpContext.Request.QueryString.HasValue)
        {
            throw new Exception(); // TODO: detailed error
        }

        Dictionary<string, string>? queryParameters = QueryHelpers.ParseQuery(httpContext.Request.QueryString.Value)
            .ToDictionary(x => x.Key, x => x.Value.ToString());

        queryParameters.TryGetValue("username", out string? username);

        if (queryParameters is not null && queryParameters.Any())
        {
            if (queryParameters.TryGetValue("oauth20_server_redirect", out string? redirectLocation))
            {
                var additionalQueryParameters = queryParameters.Where(x => x.Key != "oauth20_server_redirect" || x.Key != "username");

                if (additionalQueryParameters.Any())
                {
                    string queryString = string.Join("&", additionalQueryParameters.Select(x => $"{Uri.EscapeDataString(x.Key)}={Uri.EscapeDataString(x.Value)}"));
                    redirectLocation = redirectLocation + "?" + queryString;

                    oauth20ServerRedirect = _htmlRedirectElement.Replace("{{redirectLocation}}", redirectLocation);
                }
            }
        }

        string htmlResultString = _htmlContent
            .Replace("{{oauth20_server_redirect}}", oauth20ServerRedirect)
            .Replace("{{username}}", username);

        HtmlPageResult htmlPageResult = new(htmlResultString);

        return Task.FromResult<IResult>(htmlPageResult);
    }
}
