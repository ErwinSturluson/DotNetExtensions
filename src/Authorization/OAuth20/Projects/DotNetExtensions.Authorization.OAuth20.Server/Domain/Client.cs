// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Data;

namespace DotNetExtensions.Authorization.OAuth20.Server.Domain;

public class Client : EntityBase
{
    public Client(
        int id,
        Guid externalId,
        DateTime createdDateTime,
        string clientId,
        string? clientSecret,
        ClientType clientType,
        ClientProfile clientProfile,
        string[]? redirectionEndpoints,
        string? loginPageEndpoint,
        IEnumerable<ClientScope>? clientScopes,
        IEnumerable<ClientFlow>? clientFlows)
        : base(id, externalId, createdDateTime)
    {
        ClientId = clientId;
        ClientSecret = clientSecret;
        ClientType = clientType;
        ClientTypeId = clientType.Id;
        ClientProfile = clientProfile;
        ClientProfileId = clientProfile.Id;
        RedirectionEndpoints = redirectionEndpoints;
        LoginPageEndpoint = loginPageEndpoint;
        ClientScopes = clientScopes;
        ClientFlows = clientFlows;
    }

    protected Client()
    {
    }

    public string ClientId { get; private set; } = default!;

    public string? ClientSecret { get; private set; }

    public int ClientTypeId { get; private set; }

    public ClientType ClientType { get; private set; } = default!;

    public int ClientProfileId { get; private set; }

    public ClientProfile ClientProfile { get; private set; } = default!;

    public string[]? RedirectionEndpoints { get; private set; }

    public string? LoginPageEndpoint { get; private set; }

    public IEnumerable<ClientScope>? ClientScopes { get; private set; }

    public IEnumerable<ClientFlow>? ClientFlows { get; private set; }
}
