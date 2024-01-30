using DotNetExtensions.Authorization.OAuth20.Server.Account.Client.Abstractions;
using DotNetExtensions.Authorization.OAuth20.Server.Account.Client.Models;
using System.Net.Http.Json;

namespace DotNetExtensions.Authorization.OAuth20.Server.Account.Client.Default;

public class WasmEndUserAuthenticationService : IEndUserAuthenticationService
{
    private readonly HttpClient _httpClient;

    public WasmEndUserAuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<LoginResultModel> AuthenticateAsync(LoginModel loginModel)
    {
        await Console.Out.WriteLineAsync(nameof(WasmEndUserAuthenticationService));

        var response = await _httpClient.PostAsJsonAsync("auth/login", loginModel);

        LoginResultModel loginResultModel = new();

        if (response.IsSuccessStatusCode)
        {
            loginResultModel.Successful = true;
        }
        else
        {
            loginResultModel.Successful = false;
        }

        return loginResultModel;
    }

    public async Task<LogoutResultModel> LogoutAsync(LogoutModel logoutModel)
    {
        await Console.Out.WriteLineAsync(nameof(WasmEndUserAuthenticationService));

        var response = await _httpClient.PostAsJsonAsync("auth/logout", logoutModel);

        LogoutResultModel loginResultModel = new();

        if (response.IsSuccessStatusCode)
        {
            loginResultModel.Successful = true;
        }
        else
        {
            loginResultModel.Successful = false;
        }

        return loginResultModel;
    }
}
