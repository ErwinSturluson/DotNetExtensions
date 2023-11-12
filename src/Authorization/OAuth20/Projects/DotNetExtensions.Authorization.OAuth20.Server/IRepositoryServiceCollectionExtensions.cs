﻿// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Reporitories;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using Microsoft.Extensions.Options;

namespace DotNetExtensions.Authorization.OAuth20.Server;

public static class IRepositoryServiceCollectionExtensions
{
    public static IServiceCollection SetOAuth20DataRepositories(this IServiceCollection services, IRepositoryContext repositoryContext)
    {
        repositoryContext.SetRepositories(services);

        return services;
    }

    public static IServiceCollection SetOAuth20EntitiesFromOptions(this IServiceCollection services, IRepositoryContext repositoryContext)
    {
        services.SetOAuth20EntitiesFromOptions(repositoryContext);

        services.SetOAuth20SigningCredentialsAlgorithmEntitiesFromOptions();
        services.SetOAuth20ClientSecretTypeEntitiesFromOptions();
        services.SetOAuth20TokenAdditionalParameterEntitiesFromOptions();
        services.SetOAuth20TokenTypeEntitiesFromOptions();
        services.SetOAuth20FlowEntitiesFromOptions();
        services.SetOAuth20ResourceEntitiesFromOptions();
        services.SetOAuth20ClientEntitiesFromOptions();
        services.SetOAuth20EndUserEntitiesFromOptions();

        return services;
    }

    public static IServiceCollection SetOAuth20SigningCredentialsAlgorithmEntitiesFromOptions(this IServiceCollection services)
    {
        var options = services.BuildServiceProvider().GetRequiredService<IOptions<OAuth20ServerOptions>>().Value;
        var repository = services.BuildServiceProvider().GetRequiredService<IInt32IdNamedRepository<SigningCredentialsAlgorithm>>();

        var scaOptionsList = options.Entities?.SigningCredentialsAlgorithms;

        if (scaOptionsList is null || !scaOptionsList.Any()) return services;

        foreach (var scaOptions in scaOptionsList)
        {
            var existingEntity = repository.GetByNameAsync(scaOptions.Name).GetAwaiter().GetResult();

            if (existingEntity is null)
            {
                SigningCredentialsAlgorithm scaEntity = new()
                {
                    Name = scaOptions.Name,
                    Description = scaOptions.Description,
                };

                repository.AddAsync(scaEntity).GetAwaiter().GetResult();
            }
        }

        return services;
    }

    public static IServiceCollection SetOAuth20ClientSecretTypeEntitiesFromOptions(this IServiceCollection services)
    {
        var options = services.BuildServiceProvider().GetRequiredService<IOptions<OAuth20ServerOptions>>().Value;
        var repository = services.BuildServiceProvider().GetRequiredService<IInt32IdNamedRepository<ClientSecretType>>();

        var cstOptionsList = options.Entities?.ClientSecretTypes;

        if (cstOptionsList is null || !cstOptionsList.Any()) return services;

        foreach (var scaOptions in cstOptionsList)
        {
            var existingCstEntity = repository.GetByNameAsync(scaOptions.Name).GetAwaiter().GetResult();

            if (existingCstEntity is null)
            {
                ClientSecretType cstEntity = new()
                {
                    Name = scaOptions.Name,
                    Description = scaOptions.Description,
                };

                repository.AddAsync(cstEntity).GetAwaiter().GetResult();
            }
        }

        return services;
    }

    public static IServiceCollection SetOAuth20TokenAdditionalParameterEntitiesFromOptions(this IServiceCollection services)
    {
        var options = services.BuildServiceProvider().GetRequiredService<IOptions<OAuth20ServerOptions>>().Value;
        var repository = services.BuildServiceProvider().GetRequiredService<IInt32IdNamedRepository<TokenAdditionalParameter>>();

        var tapOptionsList = options.Entities?.TokenAdditionalParameters;

        if (tapOptionsList is null || !tapOptionsList.Any()) return services;

        foreach (var tapOptions in tapOptionsList)
        {
            var existingTapEntity = repository.GetByNameAsync(tapOptions.Name).GetAwaiter().GetResult();

            if (existingTapEntity is null)
            {
                TokenAdditionalParameter tapEntity = new()
                {
                    Name = tapOptions.Name,
                    Value = tapOptions.Value,
                    Description = tapOptions.Description,
                };

                repository.AddAsync(tapEntity).GetAwaiter().GetResult();
            }
        }

        return services;
    }

    public static IServiceCollection SetOAuth20TokenTypeEntitiesFromOptions(this IServiceCollection services)
    {
        var options = services.BuildServiceProvider().GetRequiredService<IOptions<OAuth20ServerOptions>>().Value;
        var ttRepository = services.BuildServiceProvider().GetRequiredService<IInt32IdNamedRepository<TokenType>>();
        var tapRepository = services.BuildServiceProvider().GetRequiredService<IInt32IdNamedRepository<TokenAdditionalParameter>>();
        var ttTapRepository = services.BuildServiceProvider().GetRequiredService<IInt32IdRepository<TokenTypeTokenAdditionalParameter>>();

        var ttOptionsList = options.Entities?.TokenTypes;

        if (ttOptionsList is null || !ttOptionsList.Any()) return services;

        foreach (var ttOptions in ttOptionsList)
        {
            var existingTtEntity = ttRepository.GetByNameAsync(ttOptions.Name).GetAwaiter().GetResult();

            if (existingTtEntity is null)
            {
                TokenType ttEntity = new()
                {
                    Name = ttOptions.Name,
                    Description = ttOptions.Description,
                };

                int ttEntityId = ttRepository.AddAsync(ttEntity).GetAwaiter().GetResult();

                if (ttOptions.AdditionalParameters is not null && ttOptions.AdditionalParameters.Any())
                {
                    foreach (var tapKey in ttOptions.AdditionalParameters)
                    {
                        var tapEntity = tapRepository.GetByNameAsync(tapKey).GetAwaiter().GetResult();

                        if (tapEntity is null) throw new ArgumentException($"{nameof(tapKey)}:{tapKey}"); // TODO: detailed error

                        TokenTypeTokenAdditionalParameter ttTapEntity = new()
                        {
                            TokenTypeId = ttEntityId,
                            TokenAdditionalParameterId = tapEntity.Id
                        };

                        ttTapRepository.AddAsync(ttTapEntity).GetAwaiter().GetResult();
                    }
                }
            }
        }

        return services;
    }

    public static IServiceCollection SetOAuth20FlowEntitiesFromOptions(this IServiceCollection services)
    {
        var options = services.BuildServiceProvider().GetRequiredService<IOptions<OAuth20ServerOptions>>().Value;
        var repository = services.BuildServiceProvider().GetRequiredService<IInt32IdNamedRepository<Flow>>();

        var flowOptionsList = options.Entities?.Flows;

        if (flowOptionsList is null || !flowOptionsList.Any()) return services;

        foreach (var flowOptions in flowOptionsList)
        {
            var existingFlowEntity = repository.GetByNameAsync(flowOptions.Name).GetAwaiter().GetResult();

            if (existingFlowEntity is null)
            {
                Flow flowEntity = new()
                {
                    Name = flowOptions.Name,
                    Description = flowOptions.Description,
                    GrantTypeName = flowOptions.GrantTypeName,
                    ResponseTypeName = flowOptions.ResponseTypeName,
                };

                repository.AddAsync(flowEntity).GetAwaiter().GetResult();
            }
        }

        return services;
    }

    public static IServiceCollection SetOAuth20ResourceEntitiesFromOptions(this IServiceCollection services)
    {
        var options = services.BuildServiceProvider().GetRequiredService<IOptions<OAuth20ServerOptions>>().Value;
        var resourceRepository = services.BuildServiceProvider().GetRequiredService<IInt32IdNamedRepository<Resource>>();
        var scopeRepository = services.BuildServiceProvider().GetRequiredService<IInt32IdNamedRepository<Scope>>();
        var scaRepository = services.BuildServiceProvider().GetRequiredService<IInt32IdNamedRepository<SigningCredentialsAlgorithm>>();
        var rScaRepository = services.BuildServiceProvider().GetRequiredService<IInt32IdRepository<ResourceSigningCredentialsAlgorithm>>();

        var resourceOptionsList = options.Entities?.Resources;

        if (resourceOptionsList is null || !resourceOptionsList.Any()) return services;

        foreach (var resourceOptions in resourceOptionsList)
        {
            var existingResourceEntity = resourceRepository.GetByNameAsync(resourceOptions.Name).GetAwaiter().GetResult();

            if (existingResourceEntity is null)
            {
                Resource resourceEntity = new()
                {
                    Name = resourceOptions.Name,
                    Description = resourceOptions.Description,
                };

                int resourceEntityId = resourceRepository.AddAsync(resourceEntity).GetAwaiter().GetResult();

                if (resourceOptions.Scopes is not null && resourceOptions.Scopes.Any())
                {
                    foreach (var scopeOptions in resourceOptions.Scopes)
                    {
                        var scopeExistingEntity = scopeRepository.GetByNameAsync(scopeOptions.Name).GetAwaiter().GetResult();

                        if (scopeExistingEntity is not null)
                        {
                            throw new ArgumentException($"{nameof(scopeExistingEntity.Name)}:{scopeExistingEntity.Name}"); // TODO: detailed error
                        }

                        Scope scopeEntity = new()
                        {
                            Name = scopeOptions.Name,
                            Description = scopeOptions.Description,
                            ResourceId = resourceEntityId,
                        };

                        scopeRepository.AddAsync(scopeEntity).GetAwaiter().GetResult();
                    }
                }

                if (resourceOptions.SigningCredentialsAlgorithms is not null && resourceOptions.SigningCredentialsAlgorithms.Any())
                {
                    foreach (var scaName in resourceOptions.SigningCredentialsAlgorithms)
                    {
                        var scaEntity = scaRepository.GetByNameAsync(scaName).GetAwaiter().GetResult();

                        if (scaEntity is null) throw new ArgumentException($"{nameof(scaName)}:{scaName}"); // TODO: detailed error

                        ResourceSigningCredentialsAlgorithm rScaEntity = new()
                        {
                            ResourceId = resourceEntityId,
                            SigningCredentialsAlgorithmId = scaEntity.Id,
                        };

                        rScaRepository.AddAsync(rScaEntity).GetAwaiter().GetResult();
                    }
                }

                resourceRepository.AddAsync(resourceEntity).GetAwaiter().GetResult();
            }
        }

        return services;
    }

    public static IServiceCollection SetOAuth20ClientEntitiesFromOptions(this IServiceCollection services)
    {
        var options = services.BuildServiceProvider().GetRequiredService<IOptions<OAuth20ServerOptions>>().Value;
        var clientRepository = services.BuildServiceProvider().GetRequiredService<IClientRepository>();
        var clientTypeRepository = services.BuildServiceProvider().GetRequiredService<INamedRepository<ClientType, Domain.Enums.ClientType>>();
        var clientProfileRepository = services.BuildServiceProvider().GetRequiredService<INamedRepository<ClientProfile, Domain.Enums.ClientProfile>>();
        var clientRedirectionEndpointRepository = services.BuildServiceProvider().GetRequiredService<IInt32IdRepository<ClientRedirectionEndpoint>>();
        var tokenTypeRepository = services.BuildServiceProvider().GetRequiredService<IInt32IdNamedRepository<TokenType>>();
        var scopeRepository = services.BuildServiceProvider().GetRequiredService<IInt32IdNamedRepository<Scope>>();
        var clientScopeRepository = services.BuildServiceProvider().GetRequiredService<IInt32IdRepository<ClientScope>>();
        var flowRepository = services.BuildServiceProvider().GetRequiredService<IInt32IdNamedRepository<Flow>>();
        var clientFlowRepository = services.BuildServiceProvider().GetRequiredService<IInt32IdRepository<ClientFlow>>();
        var clientSecretRepository = services.BuildServiceProvider().GetRequiredService<IInt32IdRepository<ClientSecret>>();
        var clientSecretTypeRepository = services.BuildServiceProvider().GetRequiredService<IInt32IdNamedRepository<ClientSecretType>>();

        var clientOptionsList = options.Entities?.Clients;

        if (clientOptionsList is null || !clientOptionsList.Any()) return services;

        foreach (var clientOptions in clientOptionsList)
        {
            var existingClientEntity = clientRepository.GetByClientIdAsync(clientOptions.ClientId).GetAwaiter().GetResult();

            if (existingClientEntity is null)
            {
                Client clientEntity = new()
                {
                    ClientId = clientOptions.ClientId,
                    LoginEndpoint = clientOptions.LoginEndpoint,
                    TokenExpirationSeconds = clientOptions.TokenExpirationSeconds
                };

                var clientType = clientTypeRepository.GetByNameAsync(clientOptions.ClientType).GetAwaiter().GetResult();
                if (clientType is null) throw new ArgumentException($"{nameof(clientType)}:{clientType}"); // TODO: detailed error
                clientEntity.ClientTypeId = clientType.Id;
                clientEntity.ClientType = clientType;

                var clientProfile = clientProfileRepository.GetByNameAsync(clientOptions.ClientProfile).GetAwaiter().GetResult();
                if (clientProfile is null) throw new ArgumentException($"{nameof(clientProfile)}:{clientProfile}"); // TODO: detailed error
                clientEntity.ClientProfileId = clientProfile.Id;
                clientEntity.ClientProfile = clientProfile;

                if (clientOptions.TokenType is not null)
                {
                    var tokenType = tokenTypeRepository.GetByNameAsync(clientOptions.TokenType).GetAwaiter().GetResult();
                    if (tokenType is null) throw new ArgumentException($"{nameof(tokenType)}:{tokenType}"); // TODO: detailed error
                    clientEntity.TokenTypeId = tokenType.Id;
                    clientEntity.TokenType = tokenType;
                }

                int clientEntityId = clientRepository.AddAsync(clientEntity).GetAwaiter().GetResult();

                if (clientOptions.RedirectionEndpoints is not null && clientOptions.RedirectionEndpoints.Any())
                {
                    foreach (var redirectionEndpointValue in clientOptions.RedirectionEndpoints)
                    {
                        ClientRedirectionEndpoint clientRedirectionEndpoint = new()
                        {
                            Value = redirectionEndpointValue,
                            ClientId = clientEntityId,
                        };
                    }
                }

                if (clientOptions.Scopes is not null && clientOptions.Scopes.Any())
                {
                    foreach (var scopeName in clientOptions.Scopes)
                    {
                        var scopeEntity = scopeRepository.GetByNameAsync(scopeName).GetAwaiter().GetResult();
                        if (scopeEntity is null) throw new ArgumentException($"{nameof(scopeEntity)}:{scopeEntity}"); // TODO: detailed error

                        ClientScope clientScopeEntity = new()
                        {
                            ClientId = clientEntityId,
                            ScopeId = scopeEntity.Id
                        };

                        clientScopeRepository.AddAsync(clientScopeEntity).GetAwaiter().GetResult();
                    }
                }

                if (clientOptions.Flows is not null && clientOptions.Flows.Any())
                {
                    foreach (var flowName in clientOptions.Flows)
                    {
                        var flowEntity = flowRepository.GetByNameAsync(flowName).GetAwaiter().GetResult();
                        if (flowEntity is null) throw new ArgumentException($"{nameof(flowEntity)}:{flowEntity}"); // TODO: detailed error

                        ClientFlow clientFlowEntity = new()
                        {
                            ClientId = clientEntityId,
                            FlowId = flowEntity.Id
                        };

                        clientFlowRepository.AddAsync(clientFlowEntity).GetAwaiter().GetResult();
                    }
                }

                if (clientOptions.ClientSecrets is not null && clientOptions.ClientSecrets.Any())
                {
                    foreach (var clientSecretOptions in clientOptions.ClientSecrets)
                    {
                        ClientSecret clientSecret = new()
                        {
                            ClientId = clientEntityId,
                            Title = clientSecretOptions.Title,
                            Content = clientSecretOptions.Content,
                            Desription = clientSecretOptions.Desription
                        };

                        var clientSecretType = clientSecretTypeRepository.GetByNameAsync(clientSecretOptions.ClientSecretType).GetAwaiter().GetResult();
                        if (clientSecretType is null) throw new ArgumentException($"{nameof(clientSecretType)}:{clientSecretType}"); // TODO: detailed error
                        clientSecret.ClientSecretTypeId = clientSecretType.Id;
                        clientSecret.ClientSecretType = clientSecretType;

                        clientSecretRepository.AddAsync(clientSecret).GetAwaiter().GetResult();
                    }
                }
            }
        }

        return services;
    }

    public static IServiceCollection SetOAuth20EndUserEntitiesFromOptions(this IServiceCollection services)
    {
        var options = services.BuildServiceProvider().GetRequiredService<IOptions<OAuth20ServerOptions>>().Value;
        var endUserRepository = services.BuildServiceProvider().GetRequiredService<IEndUserRepository>();
        var endUserInfoRepository = services.BuildServiceProvider().GetRequiredService<IInt32IdRepository<EndUserInfo>>();

        var endUserOptionsList = options.Entities?.EndUsers;

        if (endUserOptionsList is null || !endUserOptionsList.Any()) return services;

        foreach (var endUserOptions in endUserOptionsList)
        {
            var existingEndUserEntity = endUserRepository.GetByUsernameAsync(endUserOptions.Username).GetAwaiter().GetResult();

            if (existingEndUserEntity is null)
            {
                EndUser endUserEntity = new()
                {
                    Username = endUserOptions.Username,
                    PasswordHash = endUserOptions.Password,
                };

                int endUserEntityId = endUserRepository.AddAsync(endUserEntity).GetAwaiter().GetResult();

                EndUserInfo endUserInfo = new()
                {
                    EndUserId = endUserEntityId,
                    Description = endUserOptions.Description,
                };

                int endUserInfoEntityId = endUserInfoRepository.AddAsync(endUserInfo).GetAwaiter().GetResult();

                endUserEntity.EndUserInfoId = endUserInfoEntityId;
                endUserEntity.EndUserInfo = endUserInfo;

                endUserRepository.UpdateAsync(endUserEntity).GetAwaiter().GetResult();
            }
        }

        return services;
    }
}
