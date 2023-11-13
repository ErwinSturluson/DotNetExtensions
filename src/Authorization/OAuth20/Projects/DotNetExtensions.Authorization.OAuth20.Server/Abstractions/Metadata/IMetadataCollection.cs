// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Metadata;

public interface IMetadataCollection<TMetadata>
    where TMetadata : class
{
    public IDictionary<string, TMetadata> Items { get; set; }
}
