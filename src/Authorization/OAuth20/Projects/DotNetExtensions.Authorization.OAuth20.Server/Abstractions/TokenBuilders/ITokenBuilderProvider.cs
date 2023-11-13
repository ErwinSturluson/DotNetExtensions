// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.TokenBuilders;

public interface ITokenBuilderProvider
{
    public bool TryGetTokenBuilderInstanceByType(string type, out ITokenBuilder? tokenBuilder);
}
