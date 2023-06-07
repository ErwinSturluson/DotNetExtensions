﻿// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions;

public class EndpointMetadata
{
    protected EndpointMetadata(string route, Type abstraction, string? description = null)
    {
        Route = route;
        Abstraction = abstraction;
        Description = description;
    }

    public virtual string Route { get; set; } = default!;

    public virtual Type Abstraction { get; set; } = default!;

    public virtual string? Description { get; set; }

    public static EndpointMetadata Create<TAbstraction>(string route, string? description = null)
        where TAbstraction : IEndpoint
        => new(route, typeof(TAbstraction), description);

    public static EndpointMetadata Create(string route, Type abstraction, string? description = null)
    {
        if (!abstraction.IsAssignableTo(typeof(IEndpoint)))
        {
            throw new ArgumentException(nameof(abstraction));
        }

        return new EndpointMetadata(route, abstraction, description);
    }
}
