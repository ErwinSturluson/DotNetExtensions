// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Models.Generic;

namespace DotNetExtensions.Authorization.OAuth20.Server.Flows;

public class FlowArgumentsBuilder : IArgumentsBuilder<FlowArguments>
{
    public ValueTask<FlowArguments> BuildArgumentsAsync(HttpContext httpContext)
    {
        HttpRequest httpRequest = httpContext.Request;

        Dictionary<string, string> values;

        if (httpRequest.Method == HttpMethods.Post)
        {
            values = httpRequest.Form.ToDictionary(x => x.Key, x => x.Value.First()!);
        }
        else if (httpRequest.Method == HttpMethods.Get)
        {
            values = httpRequest.Query.ToDictionary(x => x.Key, x => x.Value.First()!);
        }
        else
        {
            throw new NotSupportedException($"{nameof(httpRequest.Method)}:{httpRequest.Method}");
        }

        Dictionary<string, string> headers = httpRequest.Headers.ToDictionary(x => x.Key, x => x.Value.First())!;

        FlowArguments flowArguments = new()
        {
            Values = values,
            Headers = headers
        };

        return ValueTask.FromResult(flowArguments);
    }
}
