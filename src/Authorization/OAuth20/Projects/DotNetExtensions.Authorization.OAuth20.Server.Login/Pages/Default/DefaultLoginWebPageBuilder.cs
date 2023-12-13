// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.WebPageBuilders;
using DotNetExtensions.Authorization.OAuth20.Server.Login.Pages.Abstractions;

namespace DotNetExtensions.Authorization.OAuth20.Server.Login.Pages.Default;

public class DefaultLoginWebPageBuilder : ILoginWebPageBuilder
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
        <form class="login-form" action="/endpoint/login/password{{query_string}}" method="post">
            <h2>Login</h2>
            <div class="input-group">
                <label for="username">Username:</label>
                <input type="text" id="username" name="username" required>
            </div>
            <div class="input-group">
                <label for="password">Password:</label>
                <input type="password" id="password" name="password" required>
            </div>
            <button type="submit">Login</button>
        </form>
    </div>
</body>
</html>
""";

    public Task<IResult> InvokeAsync(HttpContext httpContext)
    {
        if (httpContext.Request.Method != HttpMethods.Get)
        {
            throw new Exception(); // TODO: detailed error
        }

        string queryString = httpContext.Request.QueryString.ToString();
        string htmlResultString = _htmlContent.Replace("{{query_string}}", queryString);

        HtmlPageResult htmlPageResult = new(htmlResultString);

        return Task.FromResult<IResult>(htmlPageResult);
    }
}
