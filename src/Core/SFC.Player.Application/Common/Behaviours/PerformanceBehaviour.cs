using System.Diagnostics;

using MediatR;

using Microsoft.Extensions.Logging;

using SFC.Player.Application.Features.Common.Base;

namespace SFC.Player.Application.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, BaseRequest
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;

    public PerformanceBehaviour(ILogger<TRequest> logger)
    {
        _timer = new Stopwatch();
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse? response;

        _timer.Start();

        try
        {
            response = await next();
        }
        finally
        {
            _timer.Stop();

            _logger.LogDebug(request.EventId, $"Execution time for {typeof(TRequest).Name} is {_timer.ElapsedMilliseconds}ms.");
        }

        return response;
    }
}
