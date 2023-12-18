// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;
using DotNetExtensions.Authorization.OAuth20.Server.Default.Endpoints;
using DotNetExtensions.Authorization.OAuth20.Server.Login.Endpoint.Abstractions;
using Microsoft.AspNetCore.Authentication;

namespace DotNetExtensions.Authorization.OAuth20.Server.Login.Endpoint.Default;

public class DefaultLogoutEndpoint : ILogoutEndpoint
{
    private readonly IEndUserService _endUserService;

    public DefaultLogoutEndpoint(IEndUserService endUserService)
    {
        _endUserService = endUserService;
    }

    public async Task<IResult> InvokeAsync(HttpContext httpContext)
    {
        //if (httpContext.Request.Method != HttpMethods.Post)
        //{
        //    throw new Exception(); // TODO: detailed error
        //}

        if (!_endUserService.IsAuthenticated())
        {
            throw new Exception(); // TODO: detailed error or redirect to another page
        }

        var endUser = await _endUserService.GetCurrentEndUserAsync();
        if (endUser is null)
        {
            throw new Exception(); // TODO: detailed error
        }

        await httpContext.SignOutAsync();

        return new DefaultRedirectResult("/main");
    }
}
