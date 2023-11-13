// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Interceptors;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using System.Text.Json;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.Interceptors;

public class LoggingScopeInterceptor : IScopeInterceptor
{
    private readonly ILogger<LoggingScopeInterceptor> _logger;

    public LoggingScopeInterceptor(ILogger<LoggingScopeInterceptor> logger)
    {
        _logger = logger;
    }

    public Task<IEnumerable<Scope>> OnExecutedAsync(IEnumerable<Scope> issuedScope, Client client, EndUser? endUser = null, string? state = null)
    {
        _logger.LogInformation("Issued Scope Models for State [{state}], Client [{client}] and EndUser [{endUser}]: [{issuedScope}]", state, client, endUser, JsonSerializer.Serialize(issuedScope));

        return Task.FromResult(issuedScope);
    }

    public Task<string> OnExecutedAsync(string issuedScope, Client client, EndUser? endUser = null, string? state = null)
    {
        _logger.LogInformation("Issued Scope Models for State [{state}], Client [{client}] and EndUser [{endUser}]: [{issuedScope}]", state, client, endUser, issuedScope);

        return Task.FromResult(issuedScope);
    }

    public Task<string?> OnExecutingAsync(string? requestedScope, Client client, EndUser? endUser = null, string? state = null)
    {
        _logger.LogInformation("Issued Scope Models for State [{state}], Client [{client}] and EndUser [{endUser}]: [{requestedScope}]", state, client, endUser, requestedScope);

        return Task.FromResult(requestedScope);
    }

    public Task<IEnumerable<Scope>> OnExecutingAsync(IEnumerable<Scope> requestedScope, Client client, EndUser? endUser = null, string? state = null)
    {
        _logger.LogInformation("Issued Scope Models for State [{state}], Client [{client}] and EndUser [{endUser}]: [{requestedScope}]", state, client, endUser, JsonSerializer.Serialize(requestedScope));

        return Task.FromResult(requestedScope);
    }
}
