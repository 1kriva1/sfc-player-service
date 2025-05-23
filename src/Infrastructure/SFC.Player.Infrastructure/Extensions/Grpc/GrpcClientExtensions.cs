using System.Text.Encodings.Web;
using System.Text.Json;

using Google.Protobuf;

using Grpc.Core;

using Microsoft.Extensions.Configuration;

using SFC.Identity.Contracts.Headers;
using SFC.Player.Application.Common.Dto.Common;
using SFC.Player.Infrastructure.Constants;

namespace SFC.Player.Infrastructure.Extensions.Grpc;
public static class GrpcClientExtensions
{
    #region Public

    public static Task<TResponse> CallWithDeadLineAsync<TRequest, TResponse>(
        Func<TRequest, Metadata?, DateTime?, CancellationToken, AsyncUnaryCall<TResponse>> action,
        TRequest request,
        IConfiguration configuration,
        CancellationToken cancellationToken = default)
        where TResponse : IMessage
    {
        AsyncUnaryCall<TResponse> call = action(
            request,
            null,
            configuration.GetDeadLineValue(),
            cancellationToken);

        return call.ResponseAsync;
    }

    public static AsyncUnaryCall<TResponse> CallWithAuditable<TRequest, TResponse>(
        Func<TRequest, Metadata, DateTime?, CancellationToken, AsyncUnaryCall<TResponse>> action,
        TRequest request,
        IConfiguration configuration,
        Metadata? metadata = default,
        CancellationToken cancellationToken = default)
        where TResponse : IMessage
    {
        AsyncUnaryCall<TResponse> call = action(
            request,
            metadata.AddAuditableHeader(true),
            configuration.GetDeadLineValue(),
            cancellationToken);

        return call;
    }

    public static async Task<TDto?> CallWithAuditableAsync<TRequest, TResponse, TDto>(
        Func<TRequest, Metadata, DateTime?, CancellationToken, AsyncUnaryCall<TResponse>> action,
        TRequest request,
        IConfiguration configuration,
        Func<TResponse, TDto?> mapper,
        Metadata? metadata = default,
        CancellationToken cancellationToken = default)
        where TResponse : IMessage
        where TDto : AuditableDto
    {
        AsyncUnaryCall<TResponse> call = action(
            request,
            metadata.AddAuditableHeader(true),
            configuration.GetDeadLineValue(),
            cancellationToken);

        TResponse response = await call.ResponseAsync.ConfigureAwait(true);


        return await mapper(response).SetAuditablePropertiesAsync(call)
                                     .ConfigureAwait(true);
    }

    #endregion Public

    #region Private

    private static Metadata AddAuditableHeader(this Metadata? metadata, bool auditable = default)
    {
        metadata = metadata ?? [];

        metadata.Add(CommonConstants.AuditableHeaderKey, $"{auditable}");

        return metadata;
    }

    private static async Task<AuditableHeader?> GetAuditableHeaderAsync<T>(this AsyncUnaryCall<T> call)
    {
        Metadata headers = await call.ResponseHeadersAsync.ConfigureAwait(true); ;

        string? auditableHeader = headers.GetValue(CommonConstants.AuditableHeaderKey);

        if (!string.IsNullOrEmpty(auditableHeader))
        {
#pragma warning disable CA1869 // Cache and reuse 'JsonSerializerOptions' instances
            JsonSerializerOptions options = new() { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
#pragma warning restore CA1869 // Cache and reuse 'JsonSerializerOptions' instances

            AuditableHeader? header = JsonSerializer.Deserialize<AuditableHeader>(auditableHeader, options);

            return header;
        }

        return default;
    }

    private static async Task<TDto?> SetAuditablePropertiesAsync<TR, TDto>(
        this TDto? value,
        AsyncUnaryCall<TR> call)
        where TR : IMessage
        where TDto : AuditableDto
    {
        AuditableHeader? header = await call.GetAuditableHeaderAsync()
                                            .ConfigureAwait(true);

        if (value is not null && header is not null)
        {
            value.SetAuditableProperties(header);
        }

        return value;
    }

    #endregion Private
}
