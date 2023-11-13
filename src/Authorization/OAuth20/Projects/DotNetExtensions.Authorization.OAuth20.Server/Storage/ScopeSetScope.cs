// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Domain;
using DotNetExtensions.Authorization.OAuth20.Server.Domain.Abstractions;

namespace DotNetExtensions.Authorization.OAuth20.Server.Storage;

public class ScopeSetScope : EntityBase<int>
{
    public int ScopeSetId { get; set; }

    public ScopeSet ScopeSet { get; set; } = default!;

    public int ScopeId { get; set; }

    public Scope Scope { get; set; } = default!;
}
