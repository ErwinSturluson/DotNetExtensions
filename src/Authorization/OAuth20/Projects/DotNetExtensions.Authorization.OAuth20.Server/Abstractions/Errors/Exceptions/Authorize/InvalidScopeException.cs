// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using System.Runtime.Serialization;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors.Exceptions.Authorize;

public class InvalidScopeException : AuthorizeException
{
    public InvalidScopeException()
    {
    }

    public InvalidScopeException(string? message, string? state = null)
        : base(message, state)
    {
    }

    public InvalidScopeException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}
