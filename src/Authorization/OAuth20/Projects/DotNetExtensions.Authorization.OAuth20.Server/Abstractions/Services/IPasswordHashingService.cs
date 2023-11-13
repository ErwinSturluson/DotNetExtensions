// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;

public interface IPasswordHashingService
{
    public Task<string?> GetPasswordHashAsync(string? password);
}
