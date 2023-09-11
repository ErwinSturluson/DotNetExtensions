// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Options.Entities;

public class OAuth20ServerEntitiesOptions
{
    public const string DefaultSection = "OAuth20Server:Entities";

    public ResourceEntityOptions[]? ResourceList { get; set; }

    public ClientEntityOptions[]? ClientList { get; set; }

    public EndUserEntityOptions[]? EndUserList { get; set; }
}
