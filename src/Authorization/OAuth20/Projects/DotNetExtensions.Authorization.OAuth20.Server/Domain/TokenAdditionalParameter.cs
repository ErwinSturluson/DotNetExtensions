// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain.Abstractions;

namespace DotNetExtensions.Authorization.OAuth20.Server.Domain;

public class TokenAdditionalParameter : Int32IdEntityBase, INamedEntity
{
    public string Name { get; set; } = default!;

    public string Value { get; set; } = default!;

    public string? Description { get; set; }

    public IEnumerable<TokenTypeTokenAdditionalParameter>? TokenTypeTokenAdditionalParameters { get; set; }
}
