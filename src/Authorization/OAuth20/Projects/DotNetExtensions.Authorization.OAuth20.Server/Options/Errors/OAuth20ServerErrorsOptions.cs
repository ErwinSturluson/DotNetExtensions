// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Options.Errors;

public class OAuth20ServerErrorsOptions
{
    public const string DefaultSection = "OAuth20Server:Errors";

    public string? TokenInvalidRequestErrorCode { get; set; }

    public string? TokenInvalidClientErrorCode { get; set; }

    public string? TokenInvalidGrantErrorCode { get; set; }

    public string? TokenUnauthorizedClientErrorCode { get; set; }

    public string? TokenUnsupportedGrantTypeErrorCode { get; set; }

    public string? TokenInvalidScopeErrorCode { get; set; }

    public string? AuthorizeInvalidRequestErrorCode { get; set; }

    public string? AuthorizeUnauthorizedClientErrorCode { get; set; }

    public string? AuthorizeAccessDeniedErrorCode { get; set; }

    public string? AuthorizeUnsupportedResponseTypeErrorCode { get; set; }

    public string? AuthorizeInvalidScopeErrorCode { get; set; }

    public string? AuthorizeServerErrorErrorCode { get; set; }

    public string? AuthorizeTemporarilyUnavailableErrorCode { get; set; }

    public IEnumerable<ErrorOptions>? AuthorizeErrorList { get; set; }

    public IEnumerable<ErrorOptions>? TokenErrorList { get; set; }
}
