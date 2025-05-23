using Grpc.AspNetCore.Server;
using Grpc.Core;
using Grpc.Net.Client.Configuration;
using Grpc.Net.ClientFactory;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

using SFC.Player.Infrastructure.Constants;
using SFC.Player.Infrastructure.Extensions.Grpc;
using SFC.Player.Infrastructure.Interceptors.Grpc.Client;
using SFC.Player.Infrastructure.Interceptors.Grpc.Server;
using SFC.Player.Infrastructure.Services.Identity;
using SFC.Player.Infrastructure.Settings.Grpc;

using static SFC.Identity.Contracts.Services.IdentityService;

namespace SFC.Player.Infrastructure.Extensions.Grpc;

public static class GrpcExtensions
{
    private const string AuthenticationScheme = "Bearer";

    #region Public

    public static void AddGrpc(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        GrpcSettings settings = configuration.GetGrpcSettings();

        // token provider (exchanger)
        services.AddScoped<ITokenProvider, TokenProvider>();

        services.AddGrpc(environment, settings)
                .AddGrpcEndpoints(settings);
    }

    public static DateTime? GetDeadLineValue(this IConfiguration configuration)
    {
        GrpcSettings settings = configuration.GetGrpcSettings();

        return settings.DeadLineInSeconds.HasValue
            ? DateTime.UtcNow.AddSeconds(settings.DeadLineInSeconds.Value) : default;
    }

    #endregion Public

    #region Private

    private static IGrpcServerBuilder AddGrpc(this IServiceCollection services, IWebHostEnvironment environment, GrpcSettings settings)
    {
        return services.AddGrpc(options =>
        {
            options.EnableDetailedErrors = environment.IsDevelopment();
            options.MaxReceiveMessageSize = settings.MaxReceiveMessageSizeInMb.ToByteSize();
            options.MaxSendMessageSize = settings.MaxSendMessageSizeInMb.ToByteSize();

            // server interceptors
            options.Interceptors.Add<ServerExceptionInterceptor>();
            options.Interceptors.Add<ServerLoggingInterceptor>();
            options.Interceptors.Add<ServerLanguageInterceptor>();
        });
    }

    private static void AddGrpcEndpoints(this IGrpcServerBuilder services, GrpcSettings settings)
    {
#pragma warning disable CA2000 // Dispose objects before losing scope
        ILoggerFactory loggerFactory = LoggerFactory.Create(b => b.AddConsole());
#pragma warning restore CA2000 // Dispose objects before losing scope

        foreach (KeyValuePair<string, GrpcEndpoint> endpoint in settings.Endpoints)
        {
            switch (endpoint.Value.Key)
            {
                case Endpoint.Identity:
                    services.Services.AddGrpcClient<IdentityServiceClient>(endpoint.Value, settings.Retry, loggerFactory);
                    break;
                default:
                    throw new NotImplementedException($"Not implemented Grpc Api for Id: {endpoint.Key}");
            };
        }
    }

    private static void AddGrpcClient<T>(this IServiceCollection services,
        GrpcEndpoint endpoint, GrpcRetrySettings? retrySettings, ILoggerFactory loggerFactory) where T : class
    {
        services.AddGrpcClient<T>(options => options.ConfigureGrpcClient(endpoint, retrySettings))
                .AddClientInterceptors(loggerFactory)
                .EnableCallContextPropagation(o => o.SuppressContextNotFoundErrors = true)
                .AddCallCredentials((context, metadata, serviceProvider) => SetTokenAsync(endpoint, context, metadata, serviceProvider));
    }

    private static void ConfigureGrpcClient(this GrpcClientFactoryOptions options, GrpcEndpoint endpoint, GrpcRetrySettings? retrySettings)
    {
        options.Address = endpoint.Uri;

        if (retrySettings is not null)
        {
            options.AddRetryPolicy(retrySettings);
        }
    }

    private static void AddRetryPolicy(this GrpcClientFactoryOptions options, GrpcRetrySettings retrySettings)
    {
        MethodConfig defaultMethodConfig = new()
        {
            Names = { MethodName.Default },
            RetryPolicy = new RetryPolicy
            {
                MaxAttempts = retrySettings.MaxAttempts,
                InitialBackoff = retrySettings.InitialBackoffInSeconds.HasValue
                    ? TimeSpan.FromSeconds(retrySettings.InitialBackoffInSeconds.Value)
                    : default,
                MaxBackoff = retrySettings.MaxBackoffInSeconds.HasValue
                    ? TimeSpan.FromSeconds(retrySettings.MaxBackoffInSeconds.Value)
                    : default,
                BackoffMultiplier = retrySettings.BackoffMultiplier,
                RetryableStatusCodes =
                {
                    StatusCode.Unavailable,
                    StatusCode.Unknown,
                    StatusCode.Internal,
                    StatusCode.ResourceExhausted
                }
            }
        };

        ServiceConfig serviceConfig = new() { MethodConfigs = { defaultMethodConfig } };

        options.ChannelOptionsActions.Add(options => options.ServiceConfig = serviceConfig);
    }

    private static IHttpClientBuilder AddClientInterceptors(this IHttpClientBuilder builder, ILoggerFactory loggerFactory)
    {
        // client interceptors
        builder.AddInterceptor(() => new ClientLoggingInterceptor(loggerFactory))
               .AddInterceptor(() => new ClientExceptionInterceptor(loggerFactory))
               .AddInterceptor(() => new ClientLanguageInterceptor());

        return builder;
    }

    private static async Task SetTokenAsync(GrpcEndpoint endpoint, AuthInterceptorContext context, Metadata metadata, IServiceProvider serviceProvider)
    {
        ITokenProvider provider = serviceProvider.GetRequiredService<ITokenProvider>();
        string token = await provider.GetTokenAsync(endpoint, context.CancellationToken)
                                     .ConfigureAwait(true);
        metadata.AddBearerToken(token);
    }

    private static void AddBearerToken(this Metadata metadata, string token) =>
        metadata.Add(HeaderNames.Authorization, $"{AuthenticationScheme} {token}");

    #endregion Private
}
