// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.TokenBuilders;

public class TokenBuilderContext
{
    public IEnumerable<Scope> Scopes { get; set; } = default!;

    public Client Client { get; set; } = default!;

    public DateTimeOffset? CreationDateTime { get; set; }

    public DateTimeOffset? ActivationDateTime { get; set; }

    public DateTimeOffset? ExpirationDateTime { get; set; }

    public long? LifetimeSeconds { get; set; }

    public string? Issuer { get; set; }

    public IEnumerable<string>? Audiences { get; set; }

    public IDictionary<string, string>? AdditionalParameters { get; set; }

    public EndUser? EndUser { get; set; }
}
