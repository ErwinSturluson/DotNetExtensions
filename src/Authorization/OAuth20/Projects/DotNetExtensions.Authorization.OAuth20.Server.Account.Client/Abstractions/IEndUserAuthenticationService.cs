using DotNetExtensions.Authorization.OAuth20.Server.Account.Client.Models;

namespace DotNetExtensions.Authorization.OAuth20.Server.Account.Client.Abstractions;

public interface IEndUserAuthenticationService
{
    public Task<LoginResultModel> AuthenticateAsync(LoginModel loginModel);

    public Task<LogoutResultModel> LogoutAsync(LogoutModel loginModel);
}
