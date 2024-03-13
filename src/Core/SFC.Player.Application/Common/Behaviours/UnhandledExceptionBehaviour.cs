using MediatR;

using Microsoft.Extensions.Logging;

using SFC.Player.Application.Features.Common.Base;

namespace SFC.Player.Application.Common.Behaviours;

public class UnhandledExceptionBehaviour<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, BaseRequest
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            string message = $"Unhandled Exception for {typeof(TRequest).Name}";

            _logger.LogError(request.EventId, ex, message);

            throw;
        }
    }
}
