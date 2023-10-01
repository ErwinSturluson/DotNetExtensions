// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Services;

namespace DotNetExtensions.Authorization.OAuth20.Server.Default.Services;

public class UtcDateTimeService : IDateTimeService
{
    public DateTime GetCurrentDateTime() => DateTime.Now;
}
