// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Options.Entities;

public class TokenAdditionalParameterEntityOptions
{
    public string Key { get; set; } = default!;

    public string Value { get; set; } = default!;

    public string? Description { get; set; }
}
