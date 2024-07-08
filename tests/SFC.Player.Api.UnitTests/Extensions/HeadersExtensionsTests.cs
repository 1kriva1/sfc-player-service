using Microsoft.AspNetCore.Http;

using Moq;

using SFC.Player.Api.Extensions;
using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Models.Common.Pagination;

namespace SFC.Player.Api.UnitTests.Extensions;
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
        Assert.NotEqual(0, mockResponse.Object.Headers[CommonConstants.PAGINATION_HEADER_KEY].Count);
    }
}
