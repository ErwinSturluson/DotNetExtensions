// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Flows;

public interface IFlowMetadataCollection
{
    public IDictionary<string, FlowMetadata> Flows { get; set; }

    public IDictionary<string, FlowMetadata> FlowsWithGrantType { get; set; }

    public IDictionary<string, FlowMetadata> FlowsWithResponseType { get; set; }
}
