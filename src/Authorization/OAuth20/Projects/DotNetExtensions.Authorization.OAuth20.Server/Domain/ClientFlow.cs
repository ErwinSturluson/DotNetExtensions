// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain.Abstractions;

namespace DotNetExtensions.Authorization.OAuth20.Server.Domain;

public class ClientFlow : Int32IdEntityBase
{
    public int ClientId { get; set; }

    public Client Client { get; set; } = default!;

    public int FlowId { get; set; }

    public Flow Flow { get; set; } = default!;
}
