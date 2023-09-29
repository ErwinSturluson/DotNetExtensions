// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using System.Runtime.Serialization;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors.Exceptions.Common;

public class CommonException : OAuth20Exception
{
    public CommonException()
    {
    }

    public CommonException(string? message, string? state = null)
        : base(message, state)
    {
    }

    public CommonException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    protected CommonException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
