//using SFC.Player.Application.Common.Enums;
//using SFC.Player.Application.Features.Common.Models.Sorting;

//namespace SFC.Player.Application.UnitTests.Features.Common.Models.Sorting;
//public class SortingsTests
//{
//    [Fact]
//    [Trait("Models", "Sortings")]
//    public void Models_Sortings_ShouldBeNotValidByDefault()
//    {
//        // Arrange
//        Sortings<int> sortings = new();

//        // Assert
//        Assert.False(sortings.IsValid);
//    }

//    [Fact]
//    [Trait("Models", "Sortings")]
//    public void Models_Sortings_ShouldBeValid()
//    {
//        // Arrange
//        List<Sorting<int, dynamic>> sorting = new()
//        {
//            new Sorting<int, dynamic>{ Condition = true}
//        };
//        Sortings<int> sortings = new(sorting);

//        // Assert
//        Assert.True(sortings.IsValid);
//    }

//    [Fact]
//    [Trait("Models", "Sortings")]
//    public void Models_Sortings_ShouldReturnEmpty()
//    {
//        // Arrange
//        Sortings<int> sortings = new();

//        // Act
//        IEnumerable<Sorting<int, dynamic>> result = sortings.Get();

//        // Assert
//        Assert.False(result.Any());
//    }

//    [Fact]
//    [Trait("Models", "Sortings")]
//    public void Models_Sortings_ShouldReturnItems()
//    {
//        // Arrange
//        Sortings<int> sortings = new();

//        // Act
//        sortings.Add<int>(true, (value) => true);
//        IEnumerable<Sorting<int, dynamic>> result = sortings.Get();

//        // Assert
//        Assert.True(result.Any());
//    }

//    [Fact]
//    [Trait("Models", "Sortings")]
//    public void Models_Sortings_ShouldNotApply()
//    {
//        // Arrange
//        IQueryable<int> query = new List<int> { 2, 1 }.AsQueryable();
//        IEnumerable<Sorting<int, dynamic>> sorting = new List<Sorting<int, dynamic>>();

//        // Act
//        IQueryable<int> result = Sortings<int>.ApplySort(query, sorting);

//        // Assert
//        Assert.Equal(2, result.FirstOrDefault());
//    }

//    [Fact]
//    [Trait("Models", "Sortings")]
//    public void Models_Sortings_ShouldApply()
//    {
//        // Arrange
//        IQueryable<int> query = new List<int> { 2, 1 }.AsQueryable();
//        IEnumerable<Sorting<int, dynamic>> sorting = new List<Sorting<int, dynamic>> {
//            new() { Condition = true, Expression = value => value }
//        };

//        // Act
//        IQueryable<int> result = Sortings<int>.ApplySort(query, sorting);

//        // Assert
//        Assert.Equal(1, result.FirstOrDefault());
//    }

//    [Fact]
//    [Trait("Models", "Sortings")]
//    public void Models_Sortings_ShouldApplyDescending()
//    {
//        // Arrange
//        IQueryable<int> query = new List<int> { 1, 2 }.AsQueryable();
//        IEnumerable<Sorting<int, dynamic>> sorting = new List<Sorting<int, dynamic>> {
//            new() { Condition = true, Direction = SortingDirection.Descending, Expression = value => value }
//        };

//        // Act
//        IQueryable<int> result = Sortings<int>.ApplySort(query, sorting);

//        // Assert
//        Assert.Equal(2, result.FirstOrDefault());
//    }
//}
