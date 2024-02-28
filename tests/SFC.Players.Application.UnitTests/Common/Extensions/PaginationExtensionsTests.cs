using Moq;

using SFC.Players.Application.Features.Common.Dto.Pagination;
using SFC.Players.Application.Interfaces.Common;
using SFC.Players.Application.Common.Extensions;

namespace SFC.Players.Application.UnitTests.Common.Extensions;
public class PaginationExtensionsTests
{
    private readonly Uri AssertUri = new("https://localhost:7366/api/Players/byfilters");
    private readonly Mock<IUriService> _mockUriService = new();

    public PaginationExtensionsTests()
    {

        _mockUriService.Setup(r => r.GetPageUri(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns(AssertUri);
    }

    [Fact]
    [Trait("Extension", "Validation")]
    public void Extension_Pagination_ShouldSetLinksForFirstAndLastPage()
    {
        // Arrange

        PageMetadataDto page = new() { CurrentPage = 1, TotalPages = 8 };

        // Act
        PageMetadataDto updatedPage = page.SetLinks(_mockUriService.Object, "queryString", "route");

        // Assert
        Assert.NotNull(updatedPage.Links);
        Assert.NotNull(updatedPage.Links.FirstPage);
        Assert.NotNull(updatedPage.Links.LastPage);
        Assert.Equal(AssertUri, updatedPage.Links.FirstPage);
        Assert.Equal(AssertUri, updatedPage.Links.LastPage);
    }

    [Fact]
    [Trait("Extension", "Validation")]
    public void Extension_Pagination_ShouldNotSetLinksForNextPage()
    {
        // Arrange
        Uri assertUri = new("https://localhost:7366/api/Players/byfilters");
        _mockUriService.Setup(r => r.GetPageUri(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns(assertUri);
        PageMetadataDto page = new() { CurrentPage = 8, TotalPages = 8, };

        // Act
        PageMetadataDto updatedPage = page.SetLinks(_mockUriService.Object, "queryString", "route");

        // Assert
        Assert.Null(updatedPage.Links.NextPage);
    }

    [Fact]
    [Trait("Extension", "Validation")]
    public void Extension_Pagination_ShouldSetLinksForNextPage()
    {
        // Arrange
        Uri assertUri = new("https://localhost:7366/api/Players/byfilters");
        _mockUriService.Setup(r => r.GetPageUri(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns(assertUri);
        PageMetadataDto page = new() { CurrentPage = 1, TotalPages = 8, };

        // Act
        PageMetadataDto updatedPage = page.SetLinks(_mockUriService.Object, "queryString", "route");

        // Assert
        Assert.NotNull(updatedPage.Links.NextPage);
        Assert.Equal(assertUri, updatedPage.Links.NextPage);
    }

    [Fact]
    [Trait("Extension", "Validation")]
    public void Extension_Pagination_ShouldNotSetLinksForPreviousPage()
    {
        // Arrange
        Uri assertUri = new("https://localhost:7366/api/Players/byfilters");
        _mockUriService.Setup(r => r.GetPageUri(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns(assertUri);
        PageMetadataDto page = new() { CurrentPage = 1, TotalPages = 8, };

        // Act
        PageMetadataDto updatedPage = page.SetLinks(_mockUriService.Object, "queryString", "route");

        // Assert
        Assert.Null(updatedPage.Links.PreviousPage);
    }

    [Fact]
    [Trait("Extension", "Validation")]
    public void Extension_Pagination_ShouldSetLinksForPreviousPage()
    {
        // Arrange
        Uri assertUri = new("https://localhost:7366/api/Players/byfilters");
        _mockUriService.Setup(r => r.GetPageUri(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns(assertUri);
        PageMetadataDto page = new() { CurrentPage = 2, TotalPages = 8, };

        // Act
        PageMetadataDto updatedPage = page.SetLinks(_mockUriService.Object, "queryString", "route");

        // Assert
        Assert.NotNull(updatedPage.Links.PreviousPage);
        Assert.Equal(assertUri, updatedPage.Links.PreviousPage);
    }
}
