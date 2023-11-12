// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataSources;
using DotNetExtensions.Authorization.OAuth20.Server.InMemory.DataSources;

namespace DotNetExtensions.Authorization.OAuth20.Server.InMemory;

public class InMemoryDataSourceContext : IDataSourceContext
{
    public Type ClientDataSourceType { get; set; } = typeof(InMemoryClientDataSource);

    public Type ClientSecretDataSourceType { get; set; } = typeof(InMemoryClientSecretDataSource);

    public Type EndUserDataSourceType { get; set; } = typeof(InMemoryEndUserDataSource);

    public Type FlowDataSourceType { get; set; } = typeof(InMemoryFlowDataSource);

    public Type ResourceDataSourceType { get; set; } = typeof(InMemoryResourceDataSource);

    public Type ScopeDataSourceType { get; set; } = typeof(InMemoryScopeDataSource);

    public Type TokenTypeDataSourceType { get; set; } = typeof(InMemoryTokenTypeDataSource);
}
