// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.DataStorages;

public interface IDataStorageContext
{
    public Type AccessTokenStorageType { get; set; }

    public Type AuthorizationCodeStorageType { get; set; }

    public Type RefreshTokenStorageType { get; set; }
}
