// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

namespace DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Generic;

public abstract class EndpointStrategyArgumentsBuilderBase<TResult> : IEndpointStrategyArgumentsBuilder<TResult>
{
    private readonly ManualResetEvent _resetEvent = new(false);
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private TResult _arguments = default!;
    private bool _argumentsReady = false;

    public async Task<TResult> GetArgumentsAsync()
    {
        _resetEvent.WaitOne();

        return await Task.FromResult(_arguments);
    }

    public async Task ReadArgumentsAsync(HttpContext httpContext)
    {
        await _semaphore.WaitAsync();

        if (!_argumentsReady)
        {
            _arguments = await MapArgumentsAsync(httpContext);

            _resetEvent.Set();
        }

        _semaphore.Release();
    }

    protected abstract Task<TResult> MapArgumentsAsync(HttpContext httpContext);
}
