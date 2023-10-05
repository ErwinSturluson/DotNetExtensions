﻿// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain.Abstractions;

namespace DotNetExtensions.Authorization.OAuth20.Server.Domain;

public class TokenTypeTokenAdditionalParameter : EntityBase<int>
{
    public int TokenTypeId { get; set; }

    public TokenType TokenType { get; set; } = default!;

    public int TokenAdditionalParameterId { get; set; }

    public TokenAdditionalParameter TokenAdditionalParameter { get; set; } = default!;
}
