using System.Text.Encodings.Web;
using System.Text.Json;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Models.Common.Pagination;

namespace SFC.Player.Api.Extensions;

public static class HeadersExtensions
{
    public static void AddPaginationHeader(this HttpResponse response, PageMetadataModel metadata)
    {
        JsonSerializerOptions options = new() { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
        response.Headers.Add(CommonConstants.PAGINATION_HEADER_KEY, JsonSerializer.Serialize(metadata, options));
    }
}
