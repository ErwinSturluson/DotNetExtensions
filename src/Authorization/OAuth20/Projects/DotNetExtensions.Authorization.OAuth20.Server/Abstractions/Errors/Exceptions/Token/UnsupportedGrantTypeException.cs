﻿// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using System.Runtime.Serialization;

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Errors.Exceptions.Token;

public class UnsupportedGrantTypeException : TokenException
{
    public UnsupportedGrantTypeException()
    {
    }

    public UnsupportedGrantTypeException(string? message, string? state = null)
        : base(message, state)
    {
    }

    public UnsupportedGrantTypeException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}
