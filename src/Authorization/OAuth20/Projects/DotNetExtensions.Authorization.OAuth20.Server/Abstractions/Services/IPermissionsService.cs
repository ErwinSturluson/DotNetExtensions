// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Flows;
using DotNetExtensions.Authorization.OAuth20.Server.Models;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;

public interface IPermissionsService
{
    public Task<bool> RedirectToPermissionsRequiredAsync(EndUser endUser, Client client);

    public Task AddPermissionsRequestAsync(ScopeResult scopeResult, EndUser endUser, Client client);

    public Task<ScopeResult?> GetPermissionsRequestAsync(EndUser endUser, Client client);

    public Task AddPermissionsResultAsync(ScopeResult scopeResult, EndUser endUser, Client client);

    public Task<ScopeResult?> GetPermissionsResultAsync(EndUser endUser, Client client);

    public Task<bool> EndUserPermissionsRequiredAsync(Client client);

    public Task<IResult> RedirectToPermissionsAsync(FlowArguments flowArgs, Client client, string? state = null);
}
