// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using System.Runtime.Serialization;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors.Exceptions.Token;

public class UnauthorizedClientExceptrion : TokenException
{
    public UnauthorizedClientExceptrion()
    {
    }

    public UnauthorizedClientExceptrion(string? message)
        : base(message)
    {
    }

    public UnauthorizedClientExceptrion(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    protected UnauthorizedClientExceptrion(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
