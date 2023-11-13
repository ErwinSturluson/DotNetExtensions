// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors;

public class ErrorMetadata
{
    protected ErrorMetadata(string code, string? description = null, string? uri = null)
    {
        Code = code;
        Description = description;
        Uri = uri;
    }

    public string Code { get; set; } = default!;

    public string? Description { get; set; }

    public string? Uri { get; set; }

    public static ErrorMetadata Create(string code, string? description = null, string? uri = null)
        => new(code, description, uri);
}
