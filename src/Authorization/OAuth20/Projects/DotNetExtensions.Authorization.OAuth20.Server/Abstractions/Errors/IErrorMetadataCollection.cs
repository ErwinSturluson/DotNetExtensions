// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors;

public interface IErrorMetadataCollection
{
    public IDictionary<string, ErrorMetadata> AuthorizeErrors { get; set; }

    public IDictionary<string, ErrorMetadata> TokenErrors { get; set; }
}
