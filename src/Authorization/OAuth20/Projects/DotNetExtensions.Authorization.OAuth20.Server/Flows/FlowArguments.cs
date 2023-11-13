// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Flows;

public class FlowArguments
{
    public HttpRequest HttpRequest { get; set; } = default!;

    public Dictionary<string, string> Values { get; set; } = default!;

    public Dictionary<string, string> Headers { get; set; } = default!;
}
