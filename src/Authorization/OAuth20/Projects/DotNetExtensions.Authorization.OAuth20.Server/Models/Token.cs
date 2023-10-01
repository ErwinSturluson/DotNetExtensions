// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain.Abstractions;

namespace DotNetExtensions.Authorization.OAuth20.Server.Models;

public class Token : EntityBase<int>
{
    public string Value { get; set; } = default!;

    public string Type { get; set; } = default!;

    public int ScopeResultId { get; set; }

    public ScopeResult ScopeResult { get; set; } = default!;

    public string Username { get; set; } = default!;

    public string ClientId { get; set; } = default!;

    public string RedirectUri { get; set; } = default!;

    public DateTime IssuanceDateTime { get; set; }

    public long? ExpiresIn { get; set; }

    public DateTime? ExpirationDateTime { get; set; }
}
