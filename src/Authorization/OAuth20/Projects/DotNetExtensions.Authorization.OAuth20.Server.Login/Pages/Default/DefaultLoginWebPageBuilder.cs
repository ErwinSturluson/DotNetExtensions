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
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Login - OAuth 2.0 Authorization Server</title>
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <style>
        /* Custom CSS */
        .max-width-container {
            max-width: 400px;
            margin: 0 auto;
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

        .bold-text {
            font-weight: bold;
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
    </header>

    <div class="container mt-5 max-width-container">
        <h1 class="text-center">Login</h1>
        <form class="mt-4" action="/endpoint/login/password{{query_string}}" method="post">
            <div class="form-group">
                <label for="username">Username:</label>
                <input type="text" class="form-control" id="username" name="username" required>
            </div>

            <div class="form-group">
                <label for="password">Password:</label>
                <input type="password" class="form-control" id="password" name="password" required>
            </div>

            <button class="btn btn-primary btn-block input-btn" type="submit">Login</button>
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
