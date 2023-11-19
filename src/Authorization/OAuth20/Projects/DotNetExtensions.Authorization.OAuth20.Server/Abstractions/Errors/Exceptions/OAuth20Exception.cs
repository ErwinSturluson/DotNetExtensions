// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors.Exceptions;

public class OAuth20Exception : Exception
{
    public OAuth20Exception()
    {
    }

    public OAuth20Exception(string? message, string? state = null)
        : base(message)
    {
        State = state;
    }

    public OAuth20Exception(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    public string? State { get; set; }
}
