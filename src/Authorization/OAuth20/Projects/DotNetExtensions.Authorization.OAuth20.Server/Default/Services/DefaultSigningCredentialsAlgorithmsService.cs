﻿// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors.Exceptions.Common;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.Services;

public class DefaultSigningCredentialsAlgorithmsService : ISigningCredentialsAlgorithmsService
{
    private readonly IResourceService _resourceService;

    public DefaultSigningCredentialsAlgorithmsService(IResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    public async Task<IEnumerable<SigningCredentialsAlgorithm>> GetSigningCredentialsAlgorithmsForScopesAsync(IEnumerable<Scope> scopes)
    {
        // TODO: Can be refactored
        IEnumerable<Resource> resources = await _resourceService.GetResourcesByScopesAsync(scopes);

        IEnumerable<SigningCredentialsAlgorithm> signingCredentialsAlgorithms = Enumerable.Empty<SigningCredentialsAlgorithm>();

        foreach (var resource in resources)
        {
            IEnumerable<SigningCredentialsAlgorithm> resourceSigningCredentialsAlgorithms = await _resourceService.GetResourceSigningCredentialsAlgorithmsAsync(resource);

            if (!resourceSigningCredentialsAlgorithms.Any())
            {
                continue;
            }

            // TODO: figure how to get initial signing credentials out
            signingCredentialsAlgorithms = signingCredentialsAlgorithms.IntersectBy(resourceSigningCredentialsAlgorithms.Select(x => x.Id), x => x.Id);

            if (!signingCredentialsAlgorithms.Any())
            {
                throw new InvalidRequestException(
                    $"There are no registered required Signing Credentials for " +
                    $"the requested resource [{resource.Name}] in this server instance.");
            }
        }

        return signingCredentialsAlgorithms;
    }
}
