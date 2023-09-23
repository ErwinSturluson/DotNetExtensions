// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using System.Runtime.Serialization;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors.Exceptions.Token;

public class TokenException : OAuth20Exception
{
    public TokenException()
    {
    }

    public TokenException(string? message)
        : base(message)
    {
    }

    public TokenException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    protected TokenException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
