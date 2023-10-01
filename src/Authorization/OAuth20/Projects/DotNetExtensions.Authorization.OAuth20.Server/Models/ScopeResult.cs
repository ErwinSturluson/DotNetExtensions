﻿// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Models;

public class ScopeResult
{
    public string? RequestedScope { get; set; }

    public bool IssuedScopeResponseIncludeRequired { get; set; } = false;

    public string IssuedScope { get; set; } = default!;
}
