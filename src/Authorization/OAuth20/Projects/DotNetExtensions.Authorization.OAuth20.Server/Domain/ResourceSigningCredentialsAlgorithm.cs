// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain.Abstractions;

namespace DotNetExtensions.Authorization.OAuth20.Server.Domain;

public class ResourceSigningCredentialsAlgorithm : Int32IdEntityBase
{
    public int ResourceId { get; set; }

    public Resource Resource { get; set; } = default!;

    public int SigningCredentialsAlgorithmId { get; set; }

    public SigningCredentialsAlgorithm SigningCredentialsAlgorithm { get; set; } = default!;
}
