using System.Net;
using System.Text.Json;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Common.Exceptions;

using Localization = SFC.Player.Application.Common.Constants.Localization;

using ExceptionType = System.Exception;
using SFC.Player.Api.Infrastructure.Models.Base;
using SFC.Player.Infrastructure.Constants;

namespace SFC.Player.Api.Infrastructure.Middlewares;

using Handler = Func<ExceptionType, ExceptionResponse>;

internal record struct ExceptionResponse(HttpStatusCode StatusCode, BaseResponse Result) { }

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    private readonly Dictionary<Type, Handler> _exceptionHandlers;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _exceptionHandlers = new Dictionary<Type, Handler>
        {
            { typeof(BadRequestException), HandleBadRequestException },
            { typeof(NotFoundException), HandleNotFoundException },
            { typeof(AuthorizationException), HandleAuthorizationException },
            { typeof(ConfigurationException), HandleInternalException },
            { typeof(TokenExchangeException), HandleInternalException },
            { typeof(CommunicationException), HandleInternalException }
        };
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context).ConfigureAwait(false);
        }
        catch (ExceptionType ex)
        {
            await HandleExceptionAsync(context, ex).ConfigureAwait(false);
            throw;
        }
    }

    private Task HandleExceptionAsync(HttpContext context, ExceptionType exception)
    {
        Type exceptionType = exception.GetType();

        ExceptionResponse response = _exceptionHandlers.TryGetValue(exceptionType, out Handler? handler)
            ? handler.Invoke(exception)
            : HandleInternalException(exception);

        context.Response.StatusCode = (int)response.StatusCode;

        context.Response.ContentType = context.Request.ContentType ?? CommonConstants.ContentType;

        return context.Response.WriteAsync(JsonSerializer.Serialize(response.Result));
    }

    private ExceptionResponse HandleBadRequestException(ExceptionType exception)
    {
        Dictionary<string, IEnumerable<string>> validationErrors = ((BadRequestException)exception).Errors;

        return new ExceptionResponse(HttpStatusCode.BadRequest, new BaseErrorResponse(exception.Message, validationErrors));
    }

    private ExceptionResponse HandleNotFoundException(ExceptionType exception)
    {
        return new ExceptionResponse(HttpStatusCode.NotFound, new BaseResponse(exception.Message, false));
    }

    private ExceptionResponse HandleAuthorizationException(ExceptionType exception)
    {
        return new ExceptionResponse(HttpStatusCode.Unauthorized, new BaseResponse(exception.Message, false));
    }

    private ExceptionResponse HandleInternalException(ExceptionType exception)
    {
        return new(HttpStatusCode.InternalServerError, new BaseResponse(Localization.FailedResult, false));
    }
}
