// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Options.Entities;

public abstract class EntityOptionsBase
{
    public bool CreateIfNotExists { get; set; } = true;

    public bool UpdateIfExists { get; set; } = false;
}
