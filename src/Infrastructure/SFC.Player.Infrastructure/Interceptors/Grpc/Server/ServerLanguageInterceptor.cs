using System.Globalization;
using Grpc.Core.Interceptors;
using Grpc.Core;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Infrastructure.Constants;
using Microsoft.Net.Http.Headers;

namespace SFC.Player.Infrastructure.Interceptors.Grpc.Server;
public class ServerLanguageInterceptor : Interceptor
{
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        Metadata.Entry? languageHeader = context.RequestHeaders.FirstOrDefault(t =>
            t.Key.Equals(HeaderNames.AcceptLanguage, StringComparison.OrdinalIgnoreCase));

        if (languageHeader != null)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(languageHeader.Value);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageHeader.Value);
        }

        return await continuation(request, context).ConfigureAwait(false);
    }
}
