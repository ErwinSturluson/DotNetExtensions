// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Options.TokenBuilders;

public class OAuth20ServerTokenBuildersOptions
{
    public const string DefaultSection = "OAuth20Server:TokenBuilders";

    public string? BasicTokenTypeName { get; set; }

    public string? JwtTokenTypeName { get; set; }

    public string? MacTokenTypeName { get; set; }

    public IEnumerable<TokenBuilderOptions>? TokenBuilderList { get; set; }
}
