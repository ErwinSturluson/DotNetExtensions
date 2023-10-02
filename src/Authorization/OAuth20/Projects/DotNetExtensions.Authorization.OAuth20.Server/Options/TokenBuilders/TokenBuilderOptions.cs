// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Options.TokenBuilders;

/// <summary>
/// TODO: more advanced business validation.
/// </summary>
public class TokenBuilderOptions
{
    public string Type { get; set; } = default!;

    public string? Description { get; set; }

    public TokenBuilderTypeOptions? Abstraction { get; set; }

    public TokenBuilderTypeOptions? Implementation { get; set; }
}
