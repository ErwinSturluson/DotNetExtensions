// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using System.Runtime.Serialization;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors.Exceptions.Token;

public class UnauthorizedClientException : TokenException
{
    public UnauthorizedClientException()
    {
    }

    public UnauthorizedClientException(string? message)
        : base(message)
    {
    }

    public UnauthorizedClientException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    protected UnauthorizedClientException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
