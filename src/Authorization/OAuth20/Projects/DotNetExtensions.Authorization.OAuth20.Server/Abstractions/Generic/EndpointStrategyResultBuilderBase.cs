// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Generic;

public abstract class EndpointResultBuilderBase<TResult> : IEndpointStrategyResultBuilder<TResult>
{
    private readonly ManualResetEvent _resetEvent = new(false);
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private TResult _result = default!;
    private bool _resultReady = false;

    public async Task SetResultAsync(TResult result)
    {
        await _semaphore.WaitAsync();

        if (!_resultReady)
        {
            _result = result;
            _resultReady = true;

            _resetEvent.Set();
        }

        _semaphore.Release();
    }

    public async Task WriteResultAsync(HttpContext httpContext)
    {
        _resetEvent.WaitOne();

        await MapResultAsync(httpContext, _result);
    }

    protected abstract Task MapResultAsync(HttpContext httpContext, TResult result);
}
