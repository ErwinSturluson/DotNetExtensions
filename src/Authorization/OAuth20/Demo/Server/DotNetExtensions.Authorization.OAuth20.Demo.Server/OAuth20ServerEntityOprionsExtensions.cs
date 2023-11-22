// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain.Enums;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.AuthorizationCode;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using DotNetExtensions.Authorization.OAuth20.Server.Options.Entities;

namespace DotNetExtensions.Authorization.OAuth20.Demo.Server;

public static class OAuth20ServerEntityOprionsExtensions
{
    public static OAuth20ServerOptions SetDemoEntities(this OAuth20ServerOptions oAuth20ServerOptions)
    {
        var entityOptions = oAuth20ServerOptions.Entities ?? new OAuth20ServerEntitiesOptions();

        entityOptions.SetTokenAdditionalParametersDemoEntities();
        entityOptions.SetResourcesDemoEntities();
        entityOptions.SetClientsDemoEntities();
        entityOptions.SetEndUsersDemoEntities();

        oAuth20ServerOptions.Entities = entityOptions;

        return oAuth20ServerOptions;
    }

    private static OAuth20ServerEntitiesOptions SetTokenAdditionalParametersDemoEntities(this OAuth20ServerEntitiesOptions entityOptions)
    {
        TokenAdditionalParameterEntityOptions[] tokenAdditionalParameterEntityOptionsList =
        [
            new TokenAdditionalParameterEntityOptions
            {
                Name = "test_token_parameter_1_name",
                Value = "test_token_parameter_1_value",
                Description = "test_token_parameter_1_description",
            },
            new TokenAdditionalParameterEntityOptions
            {
                Name = "test_token_parameter_2_name",
                Value = "test_token_parameter_2_value",
                Description = "test_token_parameter_2_description",
            },
        ];

        if (entityOptions.TokenAdditionalParameters is not null && entityOptions.TokenAdditionalParameters.Any())
        {
            entityOptions.TokenAdditionalParameters = [.. entityOptions.TokenAdditionalParameters, .. tokenAdditionalParameterEntityOptionsList];
        }
        else
        {
            entityOptions.TokenAdditionalParameters = tokenAdditionalParameterEntityOptionsList;
        }

        return entityOptions;
    }

    private static OAuth20ServerEntitiesOptions SetResourcesDemoEntities(this OAuth20ServerEntitiesOptions entityOptions)
    {
        ResourceEntityOptions[] resourceEntityOptionsList =
        [
            new ResourceEntityOptions
            {
                Name = "resource1",
                Scopes =
                [
                    new ScopeEntityOptions
                    {
                        Name = "resource1.scope1",
                        Description = "test descriotion of resource1.scope1"
                    }
                ],
                Description = "test descriotion of resource1",
            },
            new ResourceEntityOptions
            {
                Name = "resource2",
                Scopes =
                [
                    new ScopeEntityOptions
                    {
                        Name = "resource2.scope1",
                        Description = "test descriotion of resource2.scope1"
                    }
                ],
                Description = "test descriotion of resource2",
            },
            new ResourceEntityOptions
            {
                Name = "resource3",
                Scopes =
                [
                    new ScopeEntityOptions
                    {
                        Name = "resource3.scope1",
                        Description = "test descriotion of resource3.scope1"
                    }
                ],
                Description = "test descriotion of resource3",
            },
        ];

        if (entityOptions.Resources is not null && entityOptions.Resources.Any())
        {
            entityOptions.Resources = [.. entityOptions.Resources, .. resourceEntityOptionsList];
        }
        else
        {
            entityOptions.Resources = resourceEntityOptionsList;
        }

        return entityOptions;
    }

    private static OAuth20ServerEntitiesOptions SetClientsDemoEntities(this OAuth20ServerEntitiesOptions entityOptions)
    {
        ClientEntityOptions[] clientEntityOptionsList =
        [
            new ClientEntityOptions
            {
                ClientId = "client_id_mvc_oauth20",
                ClientSecrets =
                [
                    new ()
                    {
                        Title = "test optional title",
                        ClientSecretType = "request_body_client_credentials",
                        Content = "client_secret_mvc_oauth20",
                        Desription = "test optional description"
                    }
                ],
                ClientProfile = ClientProfile.UserAgentBasedApplication.ToString(),
                ClientType = ClientType.Confidential.ToString(),
                Flows = [ "authorization_code" ],
                LoginEndpoint = "/login",
                RedirectionEndpoints = [ "/auth/callback" ],
                TokenExpirationSeconds = 60,
                TokenType = "JWT",
                Scopes =
                [
                    "resource1.scope1",
                    "resource2.scope1",
                    "resource3.scope1"
                ]
            },
        ];

        if (entityOptions.Clients is not null && entityOptions.Clients.Any())
        {
            entityOptions.Clients = [.. entityOptions.Clients, .. clientEntityOptionsList];
        }
        else
        {
            entityOptions.Clients = clientEntityOptionsList;
        }

        return entityOptions;
    }

    private static OAuth20ServerEntitiesOptions SetEndUsersDemoEntities(this OAuth20ServerEntitiesOptions entityOptions)
    {
        EndUserEntityOptions[] endUserEntityOptionsList =
        [
            new EndUserEntityOptions
            {
                Username = "Alice",
                Password = "Alice",
                Description = "test user",
            },
            new EndUserEntityOptions
            {
                Username = "Bob",
                Password = "Bob",
                Description = "test user",
            }
        ];

        if (entityOptions.EndUsers is not null && entityOptions.EndUsers.Any())
        {
            entityOptions.EndUsers = [.. entityOptions.EndUsers, .. endUserEntityOptionsList];
        }
        else
        {
            entityOptions.EndUsers = endUserEntityOptionsList;
        }

        return entityOptions;
    }
}
