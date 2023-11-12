// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataSources;

namespace DotNetExtensions.Authorization.OAuth20.Server.InMemory.DataSources;

internal class InMemoryDataSourceContext : IDataSourceContext
{
    public IClientDataSource ClientDataSource { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public IClientSecretDataSource ClientSecretDataSource { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public IEndUserDataSource EndUserDataSource { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public IFlowDataSource FlowDataSource { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public IResourceDataSource ResourceDataSource { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public IScopeDataSource ScopeDataSource { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public ITokenTypeDataSource TokenTypeDataSource { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
