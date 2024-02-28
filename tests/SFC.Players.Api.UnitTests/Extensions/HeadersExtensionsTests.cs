using Microsoft.AspNetCore.Http;

using Moq;

using SFC.Players.Api.Extensions;
using SFC.Players.Application.Common.Constants;
using SFC.Players.Application.Models.Common.Pagination;

namespace SFC.Players.Api.UnitTests.Extensions;
public class HeadersExtensionsTests
{
    [Fact]
    [Trait("Extension", "Headers")]
    public void Extension_Headers_ShouldAddPagination()
    {
        // Arrange
        Mock<HttpResponse> mockResponse = new();
        mockResponse.Setup(x => x.Headers).Returns(new HeaderDictionary());
        PageMetadataModel metadata = new();

        // Act
        mockResponse.Object.AddPaginationHeader(metadata);

        // Assert
        Assert.True(mockResponse.Object.Headers.ContainsKey(CommonConstants.PAGINATION_HEADER_KEY));
        Assert.NotEmpty(mockResponse.Object.Headers[CommonConstants.PAGINATION_HEADER_KEY]);
    }
}
