using SFC.Player.Application.Features.Common.Dto.Pagination;

namespace SFC.Player.Application.UnitTests.Features.Common.Dto.Pagination;
public class PageMetadataDtoTests
{
    [Fact]
    [Trait("Dto", "PageMetadata")]
    public void Dto_PageMetadata_ShouldNotHavePreviousPage()
    {
        // Arrange
        PageMetadataDto pageMetadata = new() { CurrentPage = 1 };

        // Assert
        Assert.False(pageMetadata.HasPreviousPage);
    }

    [Fact]
    [Trait("Dto", "PageMetadata")]
    public void Dto_PageMetadata_ShouldHavePreviousPage()
    {
        // Arrange
        PageMetadataDto pageMetadata = new() { CurrentPage = 2 };

        // Assert
        Assert.True(pageMetadata.HasPreviousPage);
    }

    [Fact]
    [Trait("Dto", "PageMetadata")]
    public void Dto_PageMetadata_ShouldNotHaveNextPage()
    {
        // Arrange
        PageMetadataDto pageMetadata = new() { CurrentPage = 2, TotalPages = 2 };

        // Assert
        Assert.False(pageMetadata.HasNextPage);
    }

    [Fact]
    [Trait("Dto", "PageMetadata")]
    public void Dto_PageMetadata_ShouldHaveNextPage()
    {
        // Arrange
        PageMetadataDto pageMetadata = new() { CurrentPage = 1, TotalPages = 2 };

        // Assert
        Assert.True(pageMetadata.HasNextPage);
    }
}
