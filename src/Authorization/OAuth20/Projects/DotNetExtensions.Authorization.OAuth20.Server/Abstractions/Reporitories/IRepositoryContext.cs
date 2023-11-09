// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Reporitories;

public interface IRepositoryContext
{
    public IRepository<Client> ClientRepository { get; set; }

    public IRepository<ClientFlow> ClientFlowRepository { get; set; }

    public IRepository<ClientProfile> ClientProfileRepository { get; set; }

    public IRepository<ClientScope> ClientScopeRepository { get; set; }

    public IRepository<ClientSecret> ClientSecretRepository { get; set; }

    public IRepository<ClientSecretType> ClientSecretTypeRepository { get; set; }

    public IRepository<ClientType> ClientTypeRepository { get; set; }

    public IRepository<EndUser> EndUserRepository { get; set; }

    public IRepository<EndUserInfo> EndUserInfoRepository { get; set; }

    public IRepository<Flow> FlowRepository { get; set; }

    public IRepository<Resource> ResourceRepository { get; set; }

    public IRepository<ResourceSigningCredentialsAlgorithm> ResourceSigningCredentialsAlgorithmRepository { get; set; }

    public IRepository<Scope> ScopeRepository { get; set; }

    public IRepository<SigningCredentialsAlgorithm> SigningCredentialsAlgorithmRepository { get; set; }

    public IRepository<TokenAdditionalParameter> TokenAdditionalParameterRepository { get; set; }

    public IRepository<TokenType> TokenTypeRepository { get; set; }

    public IRepository<TokenTypeTokenAdditionalParameter> TokenTypeTokenAdditionalParameterRepository { get; set; }
}
