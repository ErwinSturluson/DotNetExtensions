// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Flows;
using System.Text;

namespace DotNetExtensions.Authorization.OAuth20.Server.Flows;

public class LoginRedirectResult : RedirectResultBase
{
    public LoginRedirectResult(string loginEndpoint)
        : base(loginEndpoint)
    {
    }

    public LoginRedirectResult(string loginEndpoint, FlowArguments flowArguments, IDictionary<string, string>? additionalParameters = null)
        : base(loginEndpoint)
    {
        FlowArguments = flowArguments;
        AdditionalParameters = additionalParameters;
    }

    public FlowArguments FlowArguments { get; set; } = default!;

    public IDictionary<string, string>? AdditionalParameters { get; }

    public override Task ExecuteAsync(HttpContext httpContext)
    {
        StringBuilder stringBuilder = new(RedirectUri);

        IEnumerable<KeyValuePair<string, string>> localDictionary = FlowArguments.Values;

        if (AdditionalParameters is not null)
        {
            localDictionary = localDictionary.Concat(AdditionalParameters);
        }

        if (localDictionary.Any())
        {
            stringBuilder.Append('?');

            bool firstElement = true;

            foreach (var item in localDictionary)
            {
                if (firstElement)
                {
                    stringBuilder.AppendFormat("{0}={1}", item.Key, item.Value);
                    firstElement = false;
                }
                else
                {
                    stringBuilder.AppendFormat("&{0}={1}", item.Key, item.Value);
                }
            }
        }

        string redirectLocation = stringBuilder.ToString();

        httpContext.Response.Redirect(redirectLocation, false, false);

        return Task.CompletedTask;
    }
}
