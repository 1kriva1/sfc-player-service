using SFC.Player.Application.Features.Common.Models.Filters;
using SFC.Player.Application.Features.Players.Queries.Find.Dto.Filters;
using SFC.Player.Domain.Entities;
using SFC.Player.Application.Features.Players.Queries.Find;
using SFC.Player.Application.Features.Common.Dto;
using SFC.Player.Application.Features.Common.Models.Sorting;
using SFC.Player.Application.Common.Enums;
using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.UnitTests.Features.Player.Queries.Find;
public class GetPlayersExtensionsTests
{
    #region Filters

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnFilters()
    {
        // Arrange
        GetPlayersFilterDto filter = new();

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(28, result.Count());
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnNameFilterActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new()
        {
            Profile = new GetPlayersProfileFilterDto
            {
                General = new GetPlayersGeneralProfileFilterDto { Name = "Test" }
            }
        };

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[0].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnNameFilterNotActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new();

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[0].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnCityFilterActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new()
        {
            Profile = new GetPlayersProfileFilterDto
            {
                General = new GetPlayersGeneralProfileFilterDto { City = "Test" }
            }
        };

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[1].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnCityFilterNotActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new();

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[1].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnTagsFilterActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new()
        {
            Profile = new GetPlayersProfileFilterDto
            {
                General = new GetPlayersGeneralProfileFilterDto { Tags = new List<string> { "Test" } }
            }
        };

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[2].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnTagsFilterNotActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new();

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[2].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnYearsFilterActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new()
        {
            Profile = new GetPlayersProfileFilterDto
            {
                General = new GetPlayersGeneralProfileFilterDto
                {
                    Years = new RangeLimitDto<short?> { From = 10, To = 80 }
                }
            }
        };

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[3].Condition);
        Assert.True(result.ToArray()[4].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnYearsFilterNotActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new()
        {
            Profile = new GetPlayersProfileFilterDto
            {
                General = new GetPlayersGeneralProfileFilterDto
                {
                    Years = new RangeLimitDto<short?> { From = 0, To = 100 }
                }
            }
        };

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[3].Condition);
        Assert.False(result.ToArray()[4].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnAvailableTimeFilterActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new()
        {
            Profile = new GetPlayersProfileFilterDto
            {
                General = new GetPlayersGeneralProfileFilterDto
                {
                    Availability = new GetPlayersAvailabilityLimitDto
                    {
                        From = new TimeSpan(),
                        To = new TimeSpan()
                    }
                }
            }
        };

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[5].Condition);
        Assert.True(result.ToArray()[6].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnAvailableTimeFilterNotActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new();

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[5].Condition);
        Assert.False(result.ToArray()[6].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnAvailableDaysFilterActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new()
        {
            Profile = new GetPlayersProfileFilterDto
            {
                General = new GetPlayersGeneralProfileFilterDto
                {
                    Availability = new GetPlayersAvailabilityLimitDto
                    {
                        Days = new List<DayOfWeek> { DayOfWeek.Sunday }
                    }
                }
            }
        };

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[7].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnAvailableDaysFilterNotActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new();

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[7].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnFreePlayFilterActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new()
        {
            Profile = new GetPlayersProfileFilterDto
            {
                General = new GetPlayersGeneralProfileFilterDto { FreePlay = false }
            }
        };

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[8].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnFreePlayFilterNotActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new();

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[8].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnHasPhotoFilterActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new()
        {
            Profile = new GetPlayersProfileFilterDto
            {
                General = new GetPlayersGeneralProfileFilterDto { HasPhoto = false }
            }
        };

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[9].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnHasPhotoFilterNotActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new();

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[9].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnHeightFilterActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new()
        {
            Profile = new GetPlayersProfileFilterDto
            {
                Football = new GetPlayersFootballProfileFilterDto
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
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[10].Condition);
        Assert.True(result.ToArray()[11].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnHeightFilterNotActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new()
        {
            Profile = new GetPlayersProfileFilterDto
            {
                Football = new GetPlayersFootballProfileFilterDto
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
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[10].Condition);
        Assert.False(result.ToArray()[11].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnWeightFilterActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new()
        {
            Profile = new GetPlayersProfileFilterDto
            {
                Football = new GetPlayersFootballProfileFilterDto
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
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[12].Condition);
        Assert.True(result.ToArray()[13].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnWeightFilterNotActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new()
        {
            Profile = new GetPlayersProfileFilterDto
            {
                Football = new GetPlayersFootballProfileFilterDto
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
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[12].Condition);
        Assert.False(result.ToArray()[13].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnPositionsFilterActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new()
        {
            Profile = new GetPlayersProfileFilterDto
            {
                Football = new GetPlayersFootballProfileFilterDto
                {
                    Positions = new[] { 1 }
                }
            }
        };

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[14].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnPositionsFilterNotActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new();

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[14].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnWorkingFootFilterActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new()
        {
            Profile = new GetPlayersProfileFilterDto
            {
                Football = new GetPlayersFootballProfileFilterDto
                {
                    WorkingFoot = 1
                }
            }
        };

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[15].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnWorkingFootFilterNotActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new();

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[15].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnGameStylesFilterActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new()
        {
            Profile = new GetPlayersProfileFilterDto
            {
                Football = new GetPlayersFootballProfileFilterDto
                {
                    GameStyles = new[] { 1 }
                }
            }
        };

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[16].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnGameStylesFilterNotActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new();

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[16].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnSkillFilterActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new()
        {
            Profile = new GetPlayersProfileFilterDto
            {
                Football = new GetPlayersFootballProfileFilterDto
                {
                    Skill = 1
                }
            }
        };

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[17].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnSkillFilterNotActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new();

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[17].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnPhysicalConditionFilterActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new()
        {
            Profile = new GetPlayersProfileFilterDto
            {
                Football = new GetPlayersFootballProfileFilterDto
                {
                    PhysicalCondition = 1
                }
            }
        };

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[18].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnPhysicalConditionFilterNotActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new();

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[18].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnTotalStatsFilterActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new()
        {
            Stats = new GetPlayersStatsFilterDto
            {
                Total = new RangeLimitDto<short?> { From = 10, To = 90 }
            }
        };

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[19].Condition);
        Assert.True(result.ToArray()[20].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnTotalStatsFilterNotActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new()
        {
            Stats = new GetPlayersStatsFilterDto
            {
                Total = new RangeLimitDto<short?> { From = 0, To = 100 }
            }
        };

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[19].Condition);
        Assert.False(result.ToArray()[20].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnPhysicalStatsFilterActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new()
        {
            Stats = new GetPlayersStatsFilterDto
            {
                Physical = new GetPlayersStatsBySkillRangeLimitDto { From = 10, To = 90 }
            }
        };

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[21].Condition);
        Assert.True(result.ToArray()[22].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnPhysicalStatsFilterNotActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new()
        {
            Stats = new GetPlayersStatsFilterDto
            {
                Physical = new GetPlayersStatsBySkillRangeLimitDto { From = 0, To = 100 }
            }
        };

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[21].Condition);
        Assert.False(result.ToArray()[22].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnMentalStatsFilterActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new()
        {
            Stats = new GetPlayersStatsFilterDto
            {
                Mental = new GetPlayersStatsBySkillRangeLimitDto { From = 10, To = 90 }
            }
        };

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[23].Condition);
        Assert.True(result.ToArray()[24].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnMentalStatsFilterNotActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new()
        {
            Stats = new GetPlayersStatsFilterDto
            {
                Mental = new GetPlayersStatsBySkillRangeLimitDto { From = 0, To = 100 }
            }
        };

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[23].Condition);
        Assert.False(result.ToArray()[24].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnSkillStatsFilterActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new()
        {
            Stats = new GetPlayersStatsFilterDto
            {
                Skill = new GetPlayersStatsBySkillRangeLimitDto { From = 10, To = 90 }
            }
        };

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[25].Condition);
        Assert.True(result.ToArray()[26].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnSkillStatsFilterNotActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new()
        {
            Stats = new GetPlayersStatsFilterDto
            {
                Skill = new GetPlayersStatsBySkillRangeLimitDto { From = 0, To = 100 }
            }
        };

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[25].Condition);
        Assert.False(result.ToArray()[26].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnRaitingFilterActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new()
        {
            Stats = new GetPlayersStatsFilterDto
            {
                Raiting = 3
            }
        };

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.True(result.ToArray()[27].Condition);
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldReturnRaitingFilterNotActivated()
    {
        // Arrange
        GetPlayersFilterDto filter = new();

        // Act
        IEnumerable<Filter<PlayerEntity>> result = filter.BuildSearchFilters(DateTime.Now);

        // Assert
        Assert.False(result.ToArray()[27].Condition);
    }

    #endregion Filters

    #region Sorting

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldNotAddSorting()
    {
        // Arrange
        IEnumerable<SortingDto> sorting = new List<SortingDto>();

        // Act
        IEnumerable<Sorting<PlayerEntity, dynamic>> result = sorting.BuildSearchSorting();

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Any());
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldAddSorting()
    {
        // Arrange
        IEnumerable<SortingDto> sorting = new List<SortingDto>
        {
            new() { Name = nameof(PlayerGeneralProfile.FirstName), Direction = SortingDirection.Descending },
            new() { Name = "Test", Direction = SortingDirection.Descending },
            new() { Name = nameof(PlayerGeneralProfile.LastName), Direction = SortingDirection.Ascending }
        };

        // Act
        IEnumerable<Sorting<PlayerEntity, dynamic>> result = sorting.BuildSearchSorting();

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    [Trait("Extension", "GetPlayers")]
    public void Extension_GetPlayers_ShouldAddAllAvailableSortings()
    {
        // Arrange
        IEnumerable<SortingDto> sorting = new List<SortingDto>
        {
            new() { Name = nameof(PlayerGeneralProfile.FirstName), Direction = SortingDirection.Descending },
            new() { Name = nameof(PlayerGeneralProfile.LastName), Direction = SortingDirection.Ascending },
            new() { Name = nameof(PlayerFootballProfile.PhysicalCondition), Direction = SortingDirection.Descending },
            new() { Name = nameof(PlayerFootballProfile.Height), Direction = SortingDirection.Ascending },
            new() { Name = nameof(PlayerFootballProfile.Weight), Direction = SortingDirection.Descending },
            new() { Name = nameof(GetPlayersStatsFilterDto.Raiting), Direction = SortingDirection.Ascending }
        };

        // Act
        IEnumerable<Sorting<PlayerEntity, dynamic>> result = sorting.BuildSearchSorting();

        // Assert
        Assert.Equal(6, result.Count());
    }

    #endregion Sorting
}
