﻿using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Data;

namespace DotNetExtensions.Authorization.OAuth20.Server.Domain;

public class ClientProfile : EntityBase
{
    public ClientProfile(
        int id,
        Guid externalId,
        DateTime createdDateTime,
        string name,
        IEnumerable<Client>? clients)
        : base(id, externalId, createdDateTime)
    {
        Name = name;
        Clients = clients;
    }

    protected ClientProfile()
    {
    }

    public string Name { get; private set; } = default!;

    public IEnumerable<Client>? Clients { get; private set; }
}
