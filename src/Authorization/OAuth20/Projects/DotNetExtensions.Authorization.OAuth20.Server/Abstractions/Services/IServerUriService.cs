// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;

public interface IServerUriService
{
    public Task<string?> GetServerRelativeUriPrefix();

    public Task<Uri> GetServerAbsoluteUri(Uri relativeUri);

    public Task<Uri> GetServerAbsoluteUri(string relativeUri);

    public Task<Uri> GetServerRelativeUri(Uri relativeUri);

    public Task<Uri> GetServerRelativeUri(string relativeUri);
}
