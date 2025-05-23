using System;

using Google.Rpc;

using Grpc.Core;
using Grpc.Core.Interceptors;

using Microsoft.Extensions.Logging;

using SFC.Player.Application.Common.Exceptions;

using Status = Google.Rpc.Status;

namespace SFC.Player.Infrastructure.Interceptors.Grpc.Client;
public class ClientExceptionInterceptor(ILoggerFactory loggerFactory) : Interceptor
{
    private readonly ILogger<ClientExceptionInterceptor> _logger = loggerFactory.CreateLogger<ClientExceptionInterceptor>();

    #region Public

    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
        TRequest request,
        ClientInterceptorContext<TRequest, TResponse> context,
        AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        AsyncUnaryCall<TResponse> call = continuation(request, context);

        return new AsyncUnaryCall<TResponse>(
            HandleResponse(call.ResponseAsync, context.Method),
            call.ResponseHeadersAsync,
            call.GetStatus,
            call.GetTrailers,
            call.Dispose);
    }

    #endregion Public

    #region Private

    private async Task<TResponse> HandleResponse<TRequest, TResponse>(Task<TResponse> inner, Method<TRequest, TResponse> method)
    {
        try
        {
            return await inner.ConfigureAwait(true);
        }
        catch (RpcException exception) when (exception.StatusCode == StatusCode.DeadlineExceeded)
        {
            Action<ILogger, Exception> logRequest = LoggerMessage.Define(
                LogLevel.Error,
                new EventId(),
                $"Deadline exceeded during Grpc call. Name: {method.Name}. Type: {method.Type}. Request: {typeof(TRequest)}. Response: {typeof(TResponse)}"
            );
            logRequest(_logger, exception);

            throw new TimeOutException("Deadline exceeded during Grpc call", exception);
        }
        catch (RpcException exception)
        {
            Action<ILogger, Exception> logRequest = LoggerMessage.Define(
                LogLevel.Warning,
                new EventId(),
                $"Some Grpc error occured during call. Status code: {exception.Status.StatusCode}, Message: {exception.Status.Detail}"
            );
            logRequest(_logger, exception);

            throw HandleExceptionAsync(exception);
        }
        catch (Exception exception)
        {
            Action<ILogger, Exception> logRequest = LoggerMessage.Define(
               LogLevel.Error,
               new EventId(),
               $"Error occured during Grpc call. Name: {method.Name}. Type: {method.Type}. Request: {typeof(TRequest)}. Response: {typeof(TResponse)}"
            );
            logRequest(_logger, exception);

            throw new CommunicationException("Error occured during Grpc call", exception);
        }
    }

    private Exception HandleExceptionAsync(RpcException exception)
    {
        return exception.StatusCode switch
        {
            StatusCode.Unauthenticated or StatusCode.PermissionDenied => HandleAuthorizationException(exception),
            StatusCode.NotFound => HandleNotFoundException(exception),
            StatusCode.InvalidArgument => HandleInvalidArgumentException(exception),
            _ => HandleInternalException(exception),
        };
    }

    private AuthorizationException HandleAuthorizationException(RpcException exception)
    {
        Action<ILogger, Exception> logRequest = LoggerMessage.Define(
               LogLevel.Error,
               new EventId(),
               $"The request does not have valid authentication credentials or does not have permission to execute the specified operation."
        );
        logRequest(_logger, exception);

        return new AuthorizationException(exception.Message);
    }

    private NotFoundException HandleNotFoundException(RpcException exception)
    {
        Action<ILogger, Exception> logRequest = LoggerMessage.Define(
                LogLevel.Warning,
                new EventId(),
                $"Some requested entity (e.g., file or directory) was not found."
        );
        logRequest(_logger, exception);

        return new NotFoundException(exception.Message);
    }

    private BadRequestException HandleInvalidArgumentException(RpcException exception)
    {
        Action<ILogger, Exception> logRequest = LoggerMessage.Define(
               LogLevel.Error,
               new EventId(),
               $"Client specified an invalid argument."
        );
        logRequest(_logger, exception);

        BadRequest? badRequest = exception.GetRpcStatus()?.GetDetail<BadRequest>();

        if (badRequest != null)
        {
            Dictionary<string, IEnumerable<string>> errors = badRequest.FieldViolations
                .GroupBy(field => field.Field)
                .ToDictionary(group => group.Key, group => group.Select(field => field.Description));
            return new BadRequestException(exception.Message, errors);
        }

        return new BadRequestException(exception.Message, new Dictionary<string, IEnumerable<string>>());
    }

    private CommunicationException HandleInternalException(RpcException exception)
    {
        Action<ILogger, Exception> logRequest = LoggerMessage.Define(
               LogLevel.Error,
               new EventId(),
               $"Internal errors. Means some invariants expected by underlying system has been broken."
        );
        logRequest(_logger, exception);

        return new CommunicationException(exception.Message);
    }

    #endregion Private
}
