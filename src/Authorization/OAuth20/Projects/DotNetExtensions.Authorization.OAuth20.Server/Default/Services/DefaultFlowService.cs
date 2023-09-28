// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataSources;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Flows;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;
using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.AuthorizationCode;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.ClientCredentials;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.Implicit;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.ResourceOwnerPasswordCredentials;
using DotNetExtensions.Authorization.OAuth20.Server.Options;
using Microsoft.Extensions.Options;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.Services;

public class DefaultFlowService : IFlowService
{
    public DefaultFlowService(IFlowDataSource flowDataSource, IOptions<OAuth20ServerOptions> options)
    {
        FlowDataSource = flowDataSource;
        Options = options;
    }

    protected IFlowDataSource FlowDataSource { get; set; }

    protected IOptions<OAuth20ServerOptions> Options { get; }

    public async Task<Flow?> GetFlowAsync(string name)
    {
        return await FlowDataSource.GetFlowAsync(name);
    }

    public virtual async Task<Flow?> GetFlowAsync<T>(T implementation) where T : IFlow
    {
        string? flowName = implementation switch
        {
            IAuthorizationCodeFlow => Options.Value.Flows?.AuthorizationCodeFlowName ?? "authorization_code",
            IImplicitFlow => Options.Value.Flows?.ImplicitFlowName ?? "implicit",
            IClientCredentialsFlow => Options.Value.Flows?.ClientCredentialsFlowName ?? "client_credentials",
            IResourceOwnerPasswordCredentialsFlow => Options.Value.Flows?.ResourceOwnerPasswordCredentialsFlowName ?? "password",
            _ => null
        };

        if (flowName is not null)
        {
            return await FlowDataSource.GetFlowAsync(flowName);
        }
        else
        {
            return null;
        }
    }

    public virtual async Task<Flow?> GetFlowAsync(Type type)
    {
        string? flowName;

        if (type.IsAssignableTo(typeof(IAuthorizationCodeFlow))) flowName = Options.Value.Flows?.AuthorizationCodeFlowName ?? "authorization_code";
        else if (type.IsAssignableTo(typeof(IAuthorizationCodeFlow))) flowName = Options.Value.Flows?.ImplicitFlowName ?? "implicit";
        else if (type.IsAssignableTo(typeof(IAuthorizationCodeFlow))) flowName = Options.Value.Flows?.ClientCredentialsFlowName ?? "client_credentials";
        else if (type.IsAssignableTo(typeof(IAuthorizationCodeFlow))) flowName = Options.Value.Flows?.ResourceOwnerPasswordCredentialsFlowName ?? "password";
        else flowName = null;

        if (flowName is not null)
        {
            return await FlowDataSource.GetFlowAsync(flowName);
        }
        else
        {
            return null;
        }
    }
}
