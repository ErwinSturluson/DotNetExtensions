// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain.Abstractions;

namespace DotNetExtensions.Authorization.OAuth20.Server.Domain;

public class ClientSecret : EntityBase<int>
{
    public string Content { get; set; } = default!;

    public string? Title { get; set; }

    public string? Desription { get; set; }

    public int ClientId { get; set; }

    public Client Client { get; set; } = default!;

    public int ClientSecretTypeId { get; set; }

    public ClientSecretType ClientSecretType { get; set; } = default!;
}
