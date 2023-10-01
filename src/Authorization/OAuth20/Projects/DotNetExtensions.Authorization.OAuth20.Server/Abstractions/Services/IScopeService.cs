// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Models;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;

public interface IScopeService
{
    /// <summary>
    /// Description RFC6749: <see cref="https://datatracker.ietf.org/doc/html/rfc6749#section-3.3"/>фыыф
    /// </summary>
    public Task<ScopeResult> GetScopeAsync(string? requestedScope, EndUser endUser, Client client, string? state = null);

    public bool ScopesEqual(string scope1, string? scope2);
}
