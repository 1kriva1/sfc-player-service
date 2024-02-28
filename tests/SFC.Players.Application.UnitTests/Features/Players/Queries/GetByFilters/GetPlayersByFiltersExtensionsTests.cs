using SFC.Players.Application.Features.Common.Models.Filters;
using SFC.Players.Application.Features.Players.Queries.GetByFilters.Dto.Filters;
using SFC.Players.Domain.Entities;
using SFC.Players.Application.Features.Players.Queries.GetByFilters;
using SFC.Players.Application.Features.Common.Dto;
using SFC.Players.Application.Features.Common.Models.Sorting;
using SFC.Players.Application.Common.Enums;

namespace SFC.Players.Application.UnitTests.Features.Players.Queries.GetByFilters;
public class GetPlayersByFiltersExtensionsTests
{
    #region Filters

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnFilters()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new();

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(28, result.Count());
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnNameFilterActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new()
        {
            Profile = new GetPlayersByFiltersProfileFilterDto
            {
                General = new GetPlayersByFiltersGeneralProfileFilterDto { Name = "Test" }
            }
        };

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[0].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnNameFilterNotActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new();

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[0].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnCityFilterActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new()
        {
            Profile = new GetPlayersByFiltersProfileFilterDto
            {
                General = new GetPlayersByFiltersGeneralProfileFilterDto { City = "Test" }
            }
        };

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[1].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnCityFilterNotActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new();

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[1].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnTagsFilterActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new()
        {
            Profile = new GetPlayersByFiltersProfileFilterDto
            {
                General = new GetPlayersByFiltersGeneralProfileFilterDto { Tags = new List<string> { "Test" } }
            }
        };

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[2].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnTagsFilterNotActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new();

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[2].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnYearsFilterActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new()
        {
            Profile = new GetPlayersByFiltersProfileFilterDto
            {
                General = new GetPlayersByFiltersGeneralProfileFilterDto
                {
                    Years = new RangeLimitDto<short?> { From = 10, To = 80 }
                }
            }
        };

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[3].Condition);
        Assert.True(result.ToArray()[4].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnYearsFilterNotActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new()
        {
            Profile = new GetPlayersByFiltersProfileFilterDto
            {
                General = new GetPlayersByFiltersGeneralProfileFilterDto
                {
                    Years = new RangeLimitDto<short?> { From = 0, To = 100 }
                }
            }
        };

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[3].Condition);
        Assert.False(result.ToArray()[4].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnAvailableTimeFilterActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new()
        {
            Profile = new GetPlayersByFiltersProfileFilterDto
            {
                General = new GetPlayersByFiltersGeneralProfileFilterDto
                {
                    Availability = new GetPlayersByFiltersAvailabilityLimitDto
                    {
                        From = new TimeSpan(),
                        To = new TimeSpan()
                    }
                }
            }
        };

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[5].Condition);
        Assert.True(result.ToArray()[6].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnAvailableTimeFilterNotActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new();

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[5].Condition);
        Assert.False(result.ToArray()[6].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnAvailableDaysFilterActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new()
        {
            Profile = new GetPlayersByFiltersProfileFilterDto
            {
                General = new GetPlayersByFiltersGeneralProfileFilterDto
                {
                    Availability = new GetPlayersByFiltersAvailabilityLimitDto
                    {
                        Days = new List<DayOfWeek> { DayOfWeek.Sunday }
                    }
                }
            }
        };

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[7].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnAvailableDaysFilterNotActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new();

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[7].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnFreePlayFilterActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new()
        {
            Profile = new GetPlayersByFiltersProfileFilterDto
            {
                General = new GetPlayersByFiltersGeneralProfileFilterDto { FreePlay = false }
            }
        };

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[8].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnFreePlayFilterNotActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new();

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[8].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnHasPhotoFilterActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new()
        {
            Profile = new GetPlayersByFiltersProfileFilterDto
            {
                General = new GetPlayersByFiltersGeneralProfileFilterDto { HasPhoto = false }
            }
        };

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[9].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnHasPhotoFilterNotActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new();

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[9].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnHeightFilterActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new()
        {
            Profile = new GetPlayersByFiltersProfileFilterDto
            {
                Football = new GetPlayersByFiltersFootballProfileFilterDto
                {
                    Height = new RangeLimitDto<short?>
                    {
                        From = 140,
                        To = 210
                    }
                }
            }
        };

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[10].Condition);
        Assert.True(result.ToArray()[11].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnHeightFilterNotActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new()
        {
            Profile = new GetPlayersByFiltersProfileFilterDto
            {
                Football = new GetPlayersByFiltersFootballProfileFilterDto
                {
                    Height = new RangeLimitDto<short?>
                    {
                        From = 1,
                        To = 300
                    }
                }
            }
        };

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[10].Condition);
        Assert.False(result.ToArray()[11].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnWeightFilterActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new()
        {
            Profile = new GetPlayersByFiltersProfileFilterDto
            {
                Football = new GetPlayersByFiltersFootballProfileFilterDto
                {
                    Weight = new RangeLimitDto<short?>
                    {
                        From = 50,
                        To = 180
                    }
                }
            }
        };

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[12].Condition);
        Assert.True(result.ToArray()[13].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnWeightFilterNotActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new()
        {
            Profile = new GetPlayersByFiltersProfileFilterDto
            {
                Football = new GetPlayersByFiltersFootballProfileFilterDto
                {
                    Weight = new RangeLimitDto<short?>
                    {
                        From = 1,
                        To = 300
                    }
                }
            }
        };

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[12].Condition);
        Assert.False(result.ToArray()[13].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnPositionsFilterActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new()
        {
            Profile = new GetPlayersByFiltersProfileFilterDto
            {
                Football = new GetPlayersByFiltersFootballProfileFilterDto
                {
                    Positions = new[] { 1 }
                }
            }
        };

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[14].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnPositionsFilterNotActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new();

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[14].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnWorkingFootFilterActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new()
        {
            Profile = new GetPlayersByFiltersProfileFilterDto
            {
                Football = new GetPlayersByFiltersFootballProfileFilterDto
                {
                    WorkingFoot = 1
                }
            }
        };

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[15].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnWorkingFootFilterNotActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new();

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[15].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnGameStylesFilterActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new()
        {
            Profile = new GetPlayersByFiltersProfileFilterDto
            {
                Football = new GetPlayersByFiltersFootballProfileFilterDto
                {
                    GameStyles = new[] { 1 }
                }
            }
        };

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[16].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnGameStylesFilterNotActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new();

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[16].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnSkillFilterActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new()
        {
            Profile = new GetPlayersByFiltersProfileFilterDto
            {
                Football = new GetPlayersByFiltersFootballProfileFilterDto
                {
                    Skill = 1
                }
            }
        };

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[17].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnSkillFilterNotActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new();

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[17].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnPhysicalConditionFilterActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new()
        {
            Profile = new GetPlayersByFiltersProfileFilterDto
            {
                Football = new GetPlayersByFiltersFootballProfileFilterDto
                {
                    PhysicalCondition = 1
                }
            }
        };

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[18].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnPhysicalConditionFilterNotActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new();

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[18].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnTotalStatsFilterActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new()
        {
            Stats = new GetPlayersByFiltersStatsFilterDto
            {
                Total = new RangeLimitDto<short?> { From = 10, To = 90 }
            }
        };

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[19].Condition);
        Assert.True(result.ToArray()[20].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnTotalStatsFilterNotActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new()
        {
            Stats = new GetPlayersByFiltersStatsFilterDto
            {
                Total = new RangeLimitDto<short?> { From = 0, To = 100 }
            }
        };

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[19].Condition);
        Assert.False(result.ToArray()[20].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnPhysicalStatsFilterActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new()
        {
            Stats = new GetPlayersByFiltersStatsFilterDto
            {
                Physical = new GetPlayersByFiltersStatsBySkillRangeLimitDto { From = 10, To = 90 }
            }
        };

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[21].Condition);
        Assert.True(result.ToArray()[22].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnPhysicalStatsFilterNotActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new()
        {
            Stats = new GetPlayersByFiltersStatsFilterDto
            {
                Physical = new GetPlayersByFiltersStatsBySkillRangeLimitDto { From = 0, To = 100 }
            }
        };

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[21].Condition);
        Assert.False(result.ToArray()[22].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnMentalStatsFilterActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new()
        {
            Stats = new GetPlayersByFiltersStatsFilterDto
            {
                Mental = new GetPlayersByFiltersStatsBySkillRangeLimitDto { From = 10, To = 90 }
            }
        };

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[23].Condition);
        Assert.True(result.ToArray()[24].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnMentalStatsFilterNotActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new()
        {
            Stats = new GetPlayersByFiltersStatsFilterDto
            {
                Mental = new GetPlayersByFiltersStatsBySkillRangeLimitDto { From = 0, To = 100 }
            }
        };

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[23].Condition);
        Assert.False(result.ToArray()[24].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnSkillStatsFilterActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new()
        {
            Stats = new GetPlayersByFiltersStatsFilterDto
            {
                Skill = new GetPlayersByFiltersStatsBySkillRangeLimitDto { From = 10, To = 90 }
            }
        };

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[25].Condition);
        Assert.True(result.ToArray()[26].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnSkillStatsFilterNotActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new()
        {
            Stats = new GetPlayersByFiltersStatsFilterDto
            {
                Skill = new GetPlayersByFiltersStatsBySkillRangeLimitDto { From = 0, To = 100 }
            }
        };

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[25].Condition);
        Assert.False(result.ToArray()[26].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnRaitingFilterActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new()
        {
            Stats = new GetPlayersByFiltersStatsFilterDto
            {
                Raiting = 3
            }
        };

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[27].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldReturnRaitingFilterNotActivated()
    {
        // Arrange
        GetPlayersByFiltersFilterDto filter = new();

        // Act
        IEnumerable<Filter<Player>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[27].Condition);
    }

    #endregion Filters

    #region Sorting

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldNotAddSorting()
    {
        // Arrange
        IEnumerable<SortingDto> sorting = new List<SortingDto>();

        // Act
        IEnumerable<Sorting<Player, dynamic>> result = sorting.BuildSearchSorting();

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Any());
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldAddSorting()
    {
        // Arrange
        IEnumerable<SortingDto> sorting = new List<SortingDto>
        {
            new() { Name = nameof(PlayerGeneralProfile.FirstName), Direction = SortingDirection.Descending },
            new() { Name = "Test", Direction = SortingDirection.Descending },
            new() { Name = nameof(PlayerGeneralProfile.LastName), Direction = SortingDirection.Ascending }
        };

        // Act
        IEnumerable<Sorting<Player, dynamic>> result = sorting.BuildSearchSorting();

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    [Trait("Extension", "GetPlayersByFilters")]
    public void Extension_GetPlayersByFilters_ShouldAddAllAvailableSortings()
    {
        // Arrange
        IEnumerable<SortingDto> sorting = new List<SortingDto>
        {
            new() { Name = nameof(PlayerGeneralProfile.FirstName), Direction = SortingDirection.Descending },
            new() { Name = nameof(PlayerGeneralProfile.LastName), Direction = SortingDirection.Ascending },
            new() { Name = nameof(PlayerFootballProfile.PhysicalCondition), Direction = SortingDirection.Descending },
            new() { Name = nameof(PlayerFootballProfile.Height), Direction = SortingDirection.Ascending },
            new() { Name = nameof(PlayerFootballProfile.Weight), Direction = SortingDirection.Descending },
            new() { Name = nameof(GetPlayersByFiltersStatsFilterDto.Raiting), Direction = SortingDirection.Ascending }
        };

        // Act
        IEnumerable<Sorting<Player, dynamic>> result = sorting.BuildSearchSorting();

        // Assert
        Assert.Equal(6, result.Count());
    }

    #endregion Sorting
}
