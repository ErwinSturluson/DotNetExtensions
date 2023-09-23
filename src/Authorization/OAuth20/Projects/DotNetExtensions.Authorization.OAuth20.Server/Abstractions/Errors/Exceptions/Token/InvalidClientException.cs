// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using System.Runtime.Serialization;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors.Exceptions.Token;

public class InvalidClientException : TokenException
{
    public InvalidClientException()
    {
    }

    public InvalidClientException(string? message)
        : base(message)
    {
    }

    public InvalidClientException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    protected InvalidClientException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
