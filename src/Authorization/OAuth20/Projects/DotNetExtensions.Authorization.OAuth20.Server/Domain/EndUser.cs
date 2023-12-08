// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain.Abstractions;

namespace DotNetExtensions.Authorization.OAuth20.Server.Domain;

public class EndUser : Int32IdEntityBase
{
    public string Username { get; set; } = default!;

    public string? PasswordHash { get; set; }

    public int? EndUserInfoId { get; set; }

    public EndUserInfo? EndUserInfo { get; set; }
}
