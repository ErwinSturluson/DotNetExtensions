// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataSources;

public interface IDataSourceContext
{
    public IClientDataSource ClientDataSource { get; set; }

    public IClientSecretDataSource ClientSecretDataSource { get; set; }

    public IEndUserDataSource EndUserDataSource { get; set; }

    public IFlowDataSource FlowDataSource { get; set; }

    public IResourceDataSource ResourceDataSource { get; set; }

    public IScopeDataSource ScopeDataSource { get; set; }

    public ITokenTypeDataSource TokenTypeDataSource { get; set; }
}
