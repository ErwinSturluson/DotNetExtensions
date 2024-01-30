using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataSources;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;
using DotNetExtensions.Authorization.OAuth20.Server.Account.Client.Abstractions;
using DotNetExtensions.Authorization.OAuth20.Server.Account.Client.Models;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Diagnostics;

namespace DotNetExtensions.Authorization.OAuth20.Server.Account.Default;

public class ServerEndUserAuthenticationService : IEndUserAuthenticationService
{
    private readonly IEndUserDataSource _endUserDataSource;
    private readonly IPasswordHashingService _passwordHashingService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ServerEndUserAuthenticationService(
        IEndUserDataSource endUserDataSource,
        IPasswordHashingService passwordHashingService,
        IHttpContextAccessor httpContextAccessor)
    {
        _endUserDataSource = endUserDataSource;
        _passwordHashingService = passwordHashingService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<LoginResultModel> AuthenticateAsync(LoginModel loginModel)
    {
        LoginResultModel loginResultModel = new();

        if (loginModel.Username is null || loginModel.Password is null)
        {
            loginResultModel.Successful = false;
            return loginResultModel;
        }

        string? passwordHash = await _passwordHashingService.GetPasswordHashAsync(loginModel.Password);

        EndUser? endUser = await _endUserDataSource.GetEndUserAsync(loginModel.Username, passwordHash);
        if (endUser is null)
        {
            loginResultModel.Successful = false;
            return loginResultModel;
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

        if (_httpContextAccessor.HttpContext is null)
        {
            loginResultModel.Successful = false;
            return loginResultModel;
        }

        try
        {
            await _httpContextAccessor.HttpContext!.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        loginResultModel.Successful = true;

        return loginResultModel;
    }

    public async Task<LogoutResultModel> LogoutAsync(LogoutModel logoutModel)
    {
        await _httpContextAccessor.HttpContext!.SignOutAsync();

        return new LogoutResultModel { Successful = true };
    }
}
