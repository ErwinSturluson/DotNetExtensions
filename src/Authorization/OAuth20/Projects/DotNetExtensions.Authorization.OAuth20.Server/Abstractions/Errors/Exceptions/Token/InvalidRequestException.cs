// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using System.Runtime.Serialization;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors.Exceptions.Token;

public class InvalidRequestException : TokenException
{
    public InvalidRequestException()
    {
    }

    public InvalidRequestException(string? message, string? state = null)
        : base(message)
    {
        State = state;
    }

    public InvalidRequestException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    protected InvalidRequestException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
