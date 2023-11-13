// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using System.Runtime.Serialization;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors.Exceptions.Authorize;

public class AccessDeniedException : AuthorizeException
{
    public AccessDeniedException()
    {
    }

    public AccessDeniedException(string? message, string? state = null)
        : base(message, state)
    {
    }

    public AccessDeniedException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    protected AccessDeniedException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
