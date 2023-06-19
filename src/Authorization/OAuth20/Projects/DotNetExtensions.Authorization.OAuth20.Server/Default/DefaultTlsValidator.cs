// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default;

public class DefaultTlsValidator : ITlsValidator
{
    public ValidationResult TryValidate(HttpContext httpContext) => new() { Success = true };
}
