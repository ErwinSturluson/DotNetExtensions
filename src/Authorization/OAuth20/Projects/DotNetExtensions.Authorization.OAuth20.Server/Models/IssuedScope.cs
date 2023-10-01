// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Models;

public class IssuedScope
{
    public bool ResponseIncludeRequired { get; set; } = false;

    public string Value { get; set; } = default!;
}
