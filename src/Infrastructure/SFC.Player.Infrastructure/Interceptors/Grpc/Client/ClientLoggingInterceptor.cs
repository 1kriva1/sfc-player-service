using Grpc.Core.Interceptors;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace SFC.Player.Infrastructure.Interceptors.Grpc.Client;
public class ClientLoggingInterceptor(ILoggerFactory loggerFactory) : Interceptor
{
    private readonly ILogger<ClientLoggingInterceptor> _logger = loggerFactory.CreateLogger<ClientLoggingInterceptor>();

    #region Public

    public override TResponse BlockingUnaryCall<TRequest, TResponse>(
        TRequest request,
        ClientInterceptorContext<TRequest, TResponse> context,
        BlockingUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        LogRequest(context.Method);
        AddCallerMetadata(ref context);

        return base.BlockingUnaryCall(request, context, continuation);
    }

    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
        TRequest request,
        ClientInterceptorContext<TRequest, TResponse> context,
        AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        LogRequest(context.Method);
        AddCallerMetadata(ref context);

        AsyncUnaryCall<TResponse> call = continuation(request, context);

        AsyncUnaryCall<TResponse> response =
            new(LogResponse<TRequest, TResponse>(call.ResponseAsync),
            call.ResponseHeadersAsync,
            call.GetStatus,
            call.GetTrailers,
            call.Dispose);

        return response;
    }

    public override AsyncClientStreamingCall<TRequest, TResponse> AsyncClientStreamingCall<TRequest, TResponse>(
        ClientInterceptorContext<TRequest, TResponse> context,
        AsyncClientStreamingCallContinuation<TRequest, TResponse> continuation)
    {
        LogRequest(context.Method);
        AddCallerMetadata(ref context);

        return base.AsyncClientStreamingCall(context, continuation);
    }

    public override AsyncServerStreamingCall<TResponse> AsyncServerStreamingCall<TRequest, TResponse>(
        TRequest request,
        ClientInterceptorContext<TRequest, TResponse> context,
        AsyncServerStreamingCallContinuation<TRequest, TResponse> continuation)
    {
        LogRequest(context.Method);
        AddCallerMetadata(ref context);

        return base.AsyncServerStreamingCall(request, context, continuation);
    }

    public override AsyncDuplexStreamingCall<TRequest, TResponse> AsyncDuplexStreamingCall<TRequest, TResponse>(
        ClientInterceptorContext<TRequest, TResponse> context,
        AsyncDuplexStreamingCallContinuation<TRequest, TResponse> continuation)
    {
        LogRequest(context.Method);
        AddCallerMetadata(ref context);

        return base.AsyncDuplexStreamingCall(context, continuation);
    }

    #endregion Public

    #region Private

    private void LogRequest<TRequest, TResponse>(Method<TRequest, TResponse> method)
        where TRequest : class
        where TResponse : class
    {
        Action<ILogger, Exception?> logRequest = LoggerMessage.Define(
                LogLevel.Information,
                new EventId(),
                $"Starting call. Name: {method.Name}. Type: {method.Type}. Request: {typeof(TRequest)}. Response: {typeof(TResponse)}"
        );
        logRequest(_logger, null);
    }

    private static void AddCallerMetadata<TRequest, TResponse>(ref ClientInterceptorContext<TRequest, TResponse> context)
            where TRequest : class
            where TResponse : class
    {
        Metadata? headers = context.Options.Headers;

        // Call doesn't have a headers collection to add to.
        // Need to create a new context with headers for the call.
        if (headers == null)
        {
            headers = new Metadata();
            CallOptions options = context.Options.WithHeaders(headers);
            context = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, options);
        }

        // Add caller metadata to call headers
        headers.Add("caller-user", Environment.UserName);
        headers.Add("caller-machine", Environment.MachineName);
        headers.Add("caller-os", Environment.OSVersion.ToString());
    }

    private async Task<TResponse> LogResponse<TRequest, TResponse>(Task<TResponse> task)
        where TRequest : class
        where TResponse : class
    {
        TResponse? response = await task.ConfigureAwait(true);

        Action<ILogger, Exception?> logRequest = LoggerMessage.Define(
                LogLevel.Information,
                new EventId(),
                $"Response received: {"response"}. Request: {typeof(TRequest)}. Response: {typeof(TResponse)}"
        );
        logRequest(_logger, null);

        return response;
    }

    #endregion Private
}
