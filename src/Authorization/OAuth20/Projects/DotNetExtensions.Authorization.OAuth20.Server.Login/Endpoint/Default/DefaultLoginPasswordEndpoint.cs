// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataSources;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;
using DotNetExtensions.Authorization.OAuth20.Server.Default.Endpoints;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Login.Endpoint.Abstractions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;

namespace DotNetExtensions.Authorization.OAuth20.Server.Login.Endpoint.Default;

public class DefaultLoginPasswordEndpoint : ILoginPasswordEndpoint
{
    private readonly IEndUserDataSource _endUserDataSource;
    private readonly IPasswordHashingService _passwordHashingService;

    public DefaultLoginPasswordEndpoint(IEndUserDataSource endUserDataSource, IPasswordHashingService passwordHashingService)
    {
        _endUserDataSource = endUserDataSource;
        _passwordHashingService = passwordHashingService;
    }

    public async Task<IResult> InvokeAsync(HttpContext httpContext)
    {
        if (httpContext.Request.Method != HttpMethods.Post)
        {
            throw new Exception(); // TODO: detailed error
        }

        Dictionary<string, string> values = httpContext.Request.Form.ToDictionary(x => x.Key, x => x.Value.First()!);

        if (!values.TryGetValue("username", out string? username))
        {
            throw new Exception(); // TODO: detailed error
        }

        values.TryGetValue("password", out string? password);

        //if (!)
        //{
        //    throw new Exception(); // TODO: detailed error
        //}

        string? passwordHash = await _passwordHashingService.GetPasswordHashAsync(password);

        EndUser? endUser = await _endUserDataSource.GetEndUserAsync(username, passwordHash);

        if (endUser is null)
        {
            throw new Exception(); // TODO: detailed error
        }

        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, endUser.Username),
                // Add more claims if needed
            };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true
            // Customize authentication properties if needed
        };

        await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

        IResult result;

        Dictionary<string, string>? additionalQueryParameters = null;

        if (httpContext.Request.QueryString.HasValue)
        {
            additionalQueryParameters = QueryHelpers.ParseQuery(httpContext.Request.QueryString.Value)
                .ToDictionary(x => x.Key, x => x.Value.ToString());
        }

        Dictionary<string, string> queryParameters = new()
        {
            // { "username", Uri.EscapeDataString(username) }
        };

        if (additionalQueryParameters != null && additionalQueryParameters.Count != 0)
        {
            queryParameters = queryParameters.Concat(additionalQueryParameters).ToDictionary();
        }

        if (queryParameters is not null && queryParameters.Any())
        {
            //result = new DefaultRedirectResult("/login/successful", queryParameters);
            result = new DefaultRedirectResult("/oauth/authorize", queryParameters);
        }
        else
        {
            result = new DefaultRedirectResult("/main");
        }

        return result;
    }
}
