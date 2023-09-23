// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using System.Runtime.Serialization;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors.Exceptions;

public class OAuth20Exception : Exception
{
    public OAuth20Exception()
    {
    }

    public OAuth20Exception(string? message)
        : base(message)
    {
    }

    public OAuth20Exception(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    protected OAuth20Exception(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
