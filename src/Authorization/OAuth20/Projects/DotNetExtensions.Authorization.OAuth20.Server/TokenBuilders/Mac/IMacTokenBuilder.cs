﻿// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.TokenBuilders;

namespace DotNetExtensions.Authorization.OAuth20.Server.TokenBuilders.Mac;

/// <summary>
/// Description RFC7519: <see cref="https://datatracker.ietf.org/doc/html/draft-ietf-oauth-v2-http-mac-05"/>
/// </summary>
public interface IMacTokenBuilder : ITokenBuilder
{
}
