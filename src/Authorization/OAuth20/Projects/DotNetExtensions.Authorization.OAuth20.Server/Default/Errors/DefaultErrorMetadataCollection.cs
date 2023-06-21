// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors;
using System.Collections.Concurrent;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.Errors;

public class DefaultErrorMetadataCollection : IErrorMetadataCollection
{
    public IDictionary<string, ErrorMetadata> AuthorizeErrors { get; set; } = new ConcurrentDictionary<string, ErrorMetadata>();

    public IDictionary<string, ErrorMetadata> TokenErrors { get; set; } = new ConcurrentDictionary<string, ErrorMetadata>();
}
