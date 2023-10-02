// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.TokenBuilders;

public class TokenBuilderMetadata
{
    protected TokenBuilderMetadata(string type, Type abstraction, string? description = null)
    {
        Type = type;
        Abstraction = abstraction;
        Description = description;
    }

    public virtual string Type { get; set; } = default!;

    public virtual Type Abstraction { get; set; } = default!;

    public virtual string? Description { get; set; }

    public static TokenBuilderMetadata Create<TAbstraction>(string type, string? description = null)
        where TAbstraction : ITokenBuilder
        => Create(type, typeof(TAbstraction), description);

    public static TokenBuilderMetadata Create(string type, Type abstraction, string? description = null)
    {
        if (!abstraction.IsAssignableTo(typeof(ITokenBuilder)))
        {
            throw new ArgumentException(nameof(abstraction));
        }

        return new(type, abstraction, description);
    }
}
