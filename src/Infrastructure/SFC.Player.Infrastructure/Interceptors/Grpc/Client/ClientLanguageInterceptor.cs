using Grpc.Core.Interceptors;
using Grpc.Core;
using Microsoft.Net.Http.Headers;

namespace SFC.Player.Infrastructure.Interceptors.Grpc.Client;
public class ClientLanguageInterceptor() : Interceptor
{
    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
        TRequest request,
        ClientInterceptorContext<TRequest, TResponse> context,
        AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        Metadata headers = context.Options.Headers ?? [];

        headers.Add(HeaderNames.AcceptLanguage, Thread.CurrentThread.CurrentCulture.Name);

        CallOptions options = context.Options.WithHeaders(headers);

        ClientInterceptorContext<TRequest, TResponse> newContext = new(context.Method, context.Host, options);

        return base.AsyncUnaryCall(request, newContext, continuation);
    }
}
