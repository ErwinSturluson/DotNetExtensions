// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Interceptors;

/// <summary>
/// Service for intercepting a requested and an issued scopes.
/// </summary>
public interface IScopeInterceptor
{
    /// <summary>
    /// This method is executed when a requested scope is formed.
    /// </summary>
    public Task<string> OnExecutingAsync(string requestedScope, EndUser endUser, Client client, string? state = null);

    /// <summary>
    /// This method is executed when a requested scope is formed.
    /// </summary>
    public Task<IEnumerable<Scope>> OnExecutingAsync(IEnumerable<Scope> requestedScope, EndUser endUser, Client client, string? state = null);

    /// <summary>
    /// This method is executed when an issued scope is formed.
    /// </summary>
    public Task<IEnumerable<Scope>> OnExecutedAsync(IEnumerable<Scope> issuedScope, EndUser endUser, Client client, string? state = null);

    /// <summary>
    /// This method is executed when an issued scope is formed.
    /// </summary>
    public Task<string> OnExecutedAsync(string issuedScope, EndUser endUser, Client client, string? state = null);
}
