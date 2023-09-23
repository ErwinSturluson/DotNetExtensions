// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using System.Runtime.Serialization;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors.Exceptions.Authorize;

public class TemporarilyUnavailableException : AuthorizeException
{
    public TemporarilyUnavailableException()
    {
    }

    public TemporarilyUnavailableException(string? message)
        : base(message)
    {
    }

    public TemporarilyUnavailableException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    protected TemporarilyUnavailableException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
