using SFC.Player.Application.Features.Common.Models.Filters;
using SFC.Player.Application.Features.Common.Models.Paging;

namespace SFC.Player.Application.UnitTests.Features.Common.Models.Paging;
public class PagedListTests
{
    [Fact]
    [Trait("Models", "Paging")]
    public void Models_Paging_ShouldSetCount()
    {
        // Arrange
        int assertCount = 10;
        Pagination pagination = new() { Page = 1, Size = 10 };
        List<int> items = new();
        PagedList<int> list = new(items, assertCount, pagination);

        // Assert
        Assert.Equal(assertCount, list.TotalCount);
    }

    [Fact]
    [Trait("Models", "Paging")]
    public void Models_Paging_ShouldSetItems()
    {
        // Arrange
        int assertCount = 10;
        Pagination pagination = new() { Page = 1, Size = 10 };
        List<int> items = new() { 1, 2, 3 };
        PagedList<int> list = new(items, assertCount, pagination);

        // Assert
        Assert.Equal(items.Count, list.Count);
    }

    [Fact]
    [Trait("Models", "Paging")]
    public void Models_Paging_ShouldSetPagination()
    {
        // Arrange
        int assertCount = 10;
        Pagination pagination = new() { Page = 1, Size = 10 };
        List<int> items = new();
        PagedList<int> list = new(items, assertCount, pagination);

        // Assert
        Assert.Equal(pagination.Size, list.PageSize);
        Assert.Equal(pagination.Page, list.CurrentPage);
    }

    [Fact]
    [Trait("Models", "Paging")]
    public void Models_Paging_ShouldCalculateTotalPages()
    {
        // Arrange
        int assertCount = 20;
        Pagination pagination = new() { Page = 1, Size = 10 };
        List<int> items = new();
        PagedList<int> list = new(items, assertCount, pagination);

        // Assert
        Assert.Equal(2, list.TotalPages);
    }
}
