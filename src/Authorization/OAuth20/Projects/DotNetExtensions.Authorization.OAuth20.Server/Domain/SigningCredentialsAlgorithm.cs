// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain.Abstractions;

namespace DotNetExtensions.Authorization.OAuth20.Server.Domain;

public class SigningCredentialsAlgorithm : Int32IdEntityBase, INamedEntity
{
    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public IEnumerable<ResourceSigningCredentialsAlgorithm>? Resources { get; set; }
}
