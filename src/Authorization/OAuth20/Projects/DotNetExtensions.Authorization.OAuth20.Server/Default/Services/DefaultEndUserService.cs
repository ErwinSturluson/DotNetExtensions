// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataSources;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors.Exceptions.Authorize;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.Services;

public class DefaultEndUserService : IEndUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEndUserDataSource _endUserDataSource;
    private readonly IPasswordHashingService _passwordHashingService;

    public DefaultEndUserService(
        IHttpContextAccessor httpContextAccessor,
        IEndUserDataSource endUserDataSource,
        IPasswordHashingService passwordHashingService)
    {
        _httpContextAccessor = httpContextAccessor;
        _endUserDataSource = endUserDataSource;
        _passwordHashingService = passwordHashingService;
    }

    public async Task<EndUser?> GetCurrentEndUserAsync(string? state = null)
    {
        string? username = _httpContextAccessor.HttpContext?.User.Identity?.Name;

        if (username is null) throw new AccessDeniedException($"It isn't specified the {username} (Probably the user isn't authenticated).", state);

        return await GetEndUserAsync(username);
    }

    public async Task<EndUser?> GetEndUserAsync(string username)
    {
        return await _endUserDataSource.GetEndUserAsync(username);
    }

    public async Task<EndUser?> GetEndUserAsync(string username, string password)
    {
        string? passwordHash = await _passwordHashingService.GetPasswordHashAsync(password);

        return await _endUserDataSource.GetEndUserAsync(username, passwordHash);
    }

    public bool IsAuthenticated()
    {
        var authenticated = _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated;

        return !(authenticated != true);
    }
}
