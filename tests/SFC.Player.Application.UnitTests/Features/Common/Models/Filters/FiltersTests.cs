//using SFC.Player.Application.Features.Common.Models.Filters;

//namespace SFC.Player.Application.UnitTests.Features.Common.Models.Filters;
//public class FiltersTests
//{
//    [Fact]
//    [Trait("Models", "Filters")]
//    public void Models_Filters_ShouldBeNotValidByDefault()
//    {
//        // Arrange
//        Filters<int> filters = new();

//        // Assert
//        Assert.False(filters.IsValid);
//    }

//    [Fact]
//    [Trait("Models", "Filters")]
//    public void Models_Filters_ShouldHaveEmptyFilterListByDefault()
//    {
//        // Arrange
//        Filters<int> filters = new();

//        // Assert
//        Assert.False(filters.FilterList.Any());
//    }

//    [Fact]
//    [Trait("Models", "Filters")]
//    public void Models_Filters_ShouldAddFilter()
//    {
//        // Arrange
//        Filters<int> filters = new();

//        // Act
//        filters.Add(true, value => false);

//        // Assert
//        Assert.True(filters.FilterList.Any());
//        Assert.True(filters.IsValid);
//    }

//    [Fact]
//    [Trait("Models", "Filters")]
//    public void Models_Filters_ShouldConstructByDefinedFilters()
//    {
//        // Arrange
//        List<Filter<int>> filtersCollection = new()
//        {
//            new Filter<int>{ Condition = true, Expression = value => false}
//        };
//        Filters<int> filters = new(filtersCollection);

//        // Assert
//        Assert.True(filters.FilterList.Any());
//        Assert.True(filters.IsValid);
//    }
//}
