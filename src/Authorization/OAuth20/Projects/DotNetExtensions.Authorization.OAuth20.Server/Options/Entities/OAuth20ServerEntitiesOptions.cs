// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Options.Entities;

public class OAuth20ServerEntitiesOptions
{
    public const string DefaultSection = "OAuth20Server:Entities";

    public ResourceEntityOptions[]? Resources { get; set; }

    public ClientEntityOptions[]? Clients { get; set; }

    public EndUserEntityOptions[]? EndUsers { get; set; }

    public FlowEntityOptions[]? Flows { get; set; }

    public TokenTypeEntityOptions[]? TokenTypes { get; set; }

    public TokenAdditionalParameterEntityOptions[]? TokenAdditionalParameters { get; set; }

    public ClientSecretTypeEntityOptions[]? ClientSecretTypes { get; set; }

    public SigningCredentialsAlgorithmEntityOptions[]? SigningCredentialsAlgorithms { get; set; }

    public bool RemoveAllExsistingEntities { get; set; } = false;

    public bool RemoveAllExsistingEntitiesConfirmed { get; set; } = false;
}
