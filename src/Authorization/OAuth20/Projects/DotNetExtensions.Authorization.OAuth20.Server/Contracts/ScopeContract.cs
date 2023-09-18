// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Contracts;

public class ScopeContract
{
    public string Name { get; set; } = default!;

    public string ResourceName { get; set; } = default!;
}
