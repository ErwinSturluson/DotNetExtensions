// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataSources;

public interface IScopeDataSource
{
    public Task<Scope?> GetScopeAsync(string name);

    public Task<IEnumerable<Scope>> GetScopesAsync(IEnumerable<string> names);

    public Task<IEnumerable<Scope>> GetScopesAsync(Client client);

    public Task<IEnumerable<string>> GetInvalidScopesAsync(IEnumerable<string> scopes);

    public Task<bool> AllScopesValidAsync(IEnumerable<string> scopes);
}
