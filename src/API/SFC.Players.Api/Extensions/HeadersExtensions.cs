using System.Text.Encodings.Web;
using System.Text.Json;

using SFC.Players.Application.Common.Constants;
using SFC.Players.Application.Models.Common.Pagination;

namespace SFC.Players.Api.Extensions;

public static class HeadersExtensions
{
    public static void AddPaginationHeader(this HttpResponse response, PageMetadataModel metadata)
    {
        JsonSerializerOptions options = new() { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
        response.Headers.Add(CommonConstants.PAGINATION_HEADER_KEY, JsonSerializer.Serialize(metadata, options));
    }
}
