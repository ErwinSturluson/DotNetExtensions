﻿// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using System.Runtime.Serialization;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors.Exceptions.Authorize;

public class AuthorizeException : OAuth20Exception
{
    public AuthorizeException()
    {
    }

    public AuthorizeException(string? message, string? state = null)
        : base(message, state)
    {
    }

    public AuthorizeException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}
