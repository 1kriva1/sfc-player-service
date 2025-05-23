using System.Globalization;

using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

using SFC.Player.Application.Features.Common.Dto.Pagination;
using SFC.Player.Application.Interfaces.Common;

namespace SFC.Player.Infrastructure.Services.Common;
#pragma warning disable CA1054 // URI-like parameters should not be strings
public class UriService(string baseUri) : IUriService
#pragma warning restore CA1054 // URI-like parameters should not be strings
{
    private readonly string _baseUri = baseUri;
    private readonly string _pageKey = $"Pagination.{nameof(PaginationDto.Page)}";

    public Uri GetPageUri(string queryString, string route, int page)
    {
        Dictionary<string, StringValues> queryParameters = QueryHelpers.ParseNullableQuery(queryString)
            ?? [];

        if (queryParameters.TryGetValue(_pageKey, out _))
        {
            queryParameters[_pageKey] = page.ToString(CultureInfo.InvariantCulture);
        }

        return new Uri(QueryHelpers.AddQueryString(string.Concat(_baseUri, route), queryParameters));
    }
}