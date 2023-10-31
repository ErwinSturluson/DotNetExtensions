﻿// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

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
        IEnumerable<Resource> resources = await _resourceService.GetResourcesByScopesAsync(scopes);

        IEnumerable<SigningCredentialsAlgorithm> signingCredentialsAlgorithms = Enumerable.Empty<SigningCredentialsAlgorithm>();

        foreach (var resource in resources)
        {
            IEnumerable<SigningCredentialsAlgorithm> resourceSigningCredentialsAlgorithms = await _resourceService.GetResourceSigningCredentialsAlgorithmsAsync(resource);

            if (!resourceSigningCredentialsAlgorithms.Any())
            {
                continue;
            }

            signingCredentialsAlgorithms = signingCredentialsAlgorithms.IntersectBy(resourceSigningCredentialsAlgorithms.Select(x => x.Id), x => x.Id);

            if (!signingCredentialsAlgorithms.Any())
            {
                // TODO: formatted exception
                throw new Exception();
            }
        }

        return signingCredentialsAlgorithms;
    }
}
