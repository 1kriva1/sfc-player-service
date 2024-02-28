using System.Web;

using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

using SFC.Players.Application.Features.Common.Dto.Pagination;
using SFC.Players.Application.Interfaces.Common;

namespace SFC.Players.Infrastructure.Services;
public class UriService : IUriService
{
    private readonly string _baseUri;
    private readonly string _pageKey = $"Pagination.{nameof(PaginationDto.Page)}";

    public UriService(string baseUri)
    {
        _baseUri = baseUri;
    }

    public Uri GetPageUri(string queryString, string route, int page)
    {
        Dictionary<string, StringValues>? queryParameters = QueryHelpers.ParseNullableQuery(queryString);

        if (queryParameters!.TryGetValue(_pageKey, out _))
        {
            queryParameters[_pageKey] = page.ToString();
        }

        return new Uri(QueryHelpers.AddQueryString(string.Concat(_baseUri, route), queryParameters!));
    }
}
