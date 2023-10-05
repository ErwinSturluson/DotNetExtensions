// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataSources;

public interface ITokenTypeDataSource
{
    public Task<TokenType?> GetTokenTypeAsync(string name);

    public Task<TokenType?> GetTokenTypeAsync(Client client);
}
