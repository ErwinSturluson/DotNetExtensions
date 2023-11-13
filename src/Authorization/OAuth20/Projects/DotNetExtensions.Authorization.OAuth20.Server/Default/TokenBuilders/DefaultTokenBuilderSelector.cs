// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.TokenBuilders;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.TokenBuilders;

public class DefaultTokenBuilderSelector : ITokenBuilderSelector
{
    private readonly ITokenBuilderProvider _tokenBuilderProvider;

    public DefaultTokenBuilderSelector(ITokenBuilderProvider tokenBuilderProvider)
    {
        _tokenBuilderProvider = tokenBuilderProvider;
    }

    public bool TryGetTokenBuilder(string type, out ITokenBuilder? tokenBuilder)
    {
        if (_tokenBuilderProvider.TryGetTokenBuilderInstanceByType(type, out ITokenBuilder? tokenBuilderInstance))
        {
            tokenBuilder = tokenBuilderInstance!;
            return true;
        }
        else
        {
            tokenBuilder = null;
            return false;
        }
    }
}
