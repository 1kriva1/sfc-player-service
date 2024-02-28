using System.Runtime.Serialization;

using AutoMapper;
using AutoMapper.Internal;

using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Common.Dto;
using SFC.Players.Application.Features.Common.Dto.Pagination;
using SFC.Players.Application.Features.Common.Models.Paging;
using SFC.Players.Application.Features.Players.Commands.Create;
using SFC.Players.Application.Features.Players.Commands.Update;
using SFC.Players.Application.Features.Players.Common.Dto;
using SFC.Players.Application.Features.Players.Queries.Get;
using SFC.Players.Application.Features.Players.Queries.GetByFilters;
using SFC.Players.Application.Features.Players.Queries.GetByFilters.Dto.Filters;
using SFC.Players.Application.Features.Players.Queries.GetByFilters.Dto.Result;
using SFC.Players.Application.Features.Players.Queries.GetByUser.Dto;
using SFC.Players.Application.Models.Common;
using SFC.Players.Application.Models.Common.Pagination;
using SFC.Players.Application.Models.Players.Common;
using SFC.Players.Application.Models.Players.Create;
using SFC.Players.Application.Models.Players.Get;
using SFC.Players.Application.Models.Players.GetByFilters;
using SFC.Players.Application.Models.Players.GetByFilters.Filters;
using SFC.Players.Application.Models.Players.GetByFilters.Result;
using SFC.Players.Application.Models.Players.GetByUser.Result;
using SFC.Players.Application.Models.Players.Update;
using SFC.Players.Domain.Entities;
using SFC.Players.Domain.Entities.Data;

namespace SFC.Players.Application.UnitTests.Common.Mappings;
public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config =>
        {
            config.Internal().AllowAdditiveTypeMapCreation = true;
            config.AddProfile<MappingProfile>();
        });

        _mapper = _configuration.CreateMapper();
    }

    [Fact]
    [Trait("Mapping", "Profile")]
    public void Mapping_Profile_ShouldHaveValidConfiguration()
    {
        // Assert
        _configuration.AssertConfigurationIsValid();
    }

    [Theory]
    [Trait("Mapping", "CreatePlayer")]
    [InlineData(typeof(CreatePlayerRequest), typeof(CreatePlayerCommand))]
    [InlineData(typeof(CreatePlayerDto), typeof(Player))]
    [InlineData(typeof(Player), typeof(CreatePlayerViewModel))]
    [InlineData(typeof(CreatePlayerModel), typeof(CreatePlayerDto))]
    [InlineData(typeof(CreatePlayerViewModel), typeof(CreatePlayerResponse))]
    public void Mapping_CreatePlayer_ShouldHaveValidConfiguration(Type source, Type destination)
    {
        // Arrange
        object instance = GetInstanceOf(source);

        // Assert
        _mapper.Map(instance, source, destination);
    }

    [Theory]
    [Trait("Mapping", "UpdatePlayer")]
    [InlineData(typeof(UpdatePlayerRequest), typeof(UpdatePlayerCommand))]
    [InlineData(typeof(UpdatePlayerDto), typeof(Player))]
    [InlineData(typeof(UpdatePlayerModel), typeof(UpdatePlayerDto))]
    public void Mapping_UpdatePlayer_ShouldHaveValidConfiguration(Type source, Type destination)
    {
        // Arrange
        object instance = GetInstanceOf(source);

        // Assert
        _mapper.Map(instance, source, destination);
    }

    [Theory]
    [Trait("Mapping", "GetPlayer")]
    [InlineData(typeof(Player), typeof(GetPlayerViewModel))]
    [InlineData(typeof(GetPlayerViewModel), typeof(GetPlayerResponse))]
    public void Mapping_GetPlayer_ShouldHaveValidConfiguration(Type source, Type destination)
    {
        // Arrange
        object instance = GetInstanceOf(source);

        // Assert
        _mapper.Map(instance, source, destination);
    }

    [Theory]
    [Trait("Mapping", "GetPlayerByUser")]
    [InlineData(typeof(Player), typeof(GetPlayerByUserViewModel))]
    [InlineData(typeof(GetPlayerByUserViewModel), typeof(GetPlayerByUserResponse))]
    [InlineData(typeof(Player), typeof(PlayerByUserDto))]
    [InlineData(typeof(PlayerFootballProfile), typeof(PlayerFootballProfileByUserDto))]
    [InlineData(typeof(Player), typeof(PlayerGeneralProfileByUserDto))]
    [InlineData(typeof(Player), typeof(PlayerProfileByUserDto))]
    [InlineData(typeof(PlayerByUserDto), typeof(PlayerByUserModel))]
    [InlineData(typeof(PlayerFootballProfileByUserDto), typeof(PlayerFootballProfileByUserModel))]
    [InlineData(typeof(PlayerGeneralProfileByUserDto), typeof(PlayerGeneralProfileByUserModel))]
    [InlineData(typeof(PlayerProfileByUserDto), typeof(PlayerProfileByUserModel))]
    public void Mapping_GetPlayerByUser_ShouldHaveValidConfiguration(Type source, Type destination)
    {
        // Arrange
        object instance = GetInstanceOf(source);

        // Assert
        _mapper.Map(instance, source, destination);
    }

    [Theory]
    [Trait("Mapping", "GetPlayersByFilters")]
    [InlineData(typeof(GetPlayersByFiltersAvailabilityLimitModel), typeof(GetPlayersByFiltersAvailabilityLimitDto))]
    [InlineData(typeof(GetPlayersByFiltersFilterModel), typeof(GetPlayersByFiltersFilterDto))]
    [InlineData(typeof(GetPlayersByFiltersFootballProfileFilterModel), typeof(GetPlayersByFiltersFootballProfileFilterDto))]
    [InlineData(typeof(GetPlayersByFiltersGeneralProfileFilterModel), typeof(GetPlayersByFiltersGeneralProfileFilterDto))]
    [InlineData(typeof(GetPlayersByFiltersProfileFilterModel), typeof(GetPlayersByFiltersProfileFilterDto))]
    [InlineData(typeof(GetPlayersByFiltersStatsBySkillRangeLimitModel), typeof(GetPlayersByFiltersStatsBySkillRangeLimitDto))]
    [InlineData(typeof(GetPlayersByFiltersStatsFilterModel), typeof(GetPlayersByFiltersStatsFilterDto))]
    [InlineData(typeof(Player), typeof(PlayerByFiltersDto))]
    [InlineData(typeof(PlayerFootballProfile), typeof(PlayerByFiltersFootballProfileDto))]
    [InlineData(typeof(Player), typeof(PlayerByFiltersGeneralProfileDto))]
    [InlineData(typeof(Player), typeof(PlayerByFiltersProfileDto))]
    [InlineData(typeof(Player), typeof(PlayerByFiltersStatsDto))]
    [InlineData(typeof(GetPlayersByFiltersRequest), typeof(GetPlayersByFiltersQuery))]
    [InlineData(typeof(PlayerByFiltersFootballProfileDto), typeof(PlayerByFiltersFootballProfileModel))]
    [InlineData(typeof(PlayerByFiltersGeneralProfileDto), typeof(PlayerByFiltersGeneralProfileModel))]
    [InlineData(typeof(PlayerByFiltersDto), typeof(PlayerByFiltersModel))]
    [InlineData(typeof(PlayerByFiltersProfileDto), typeof(PlayerByFiltersProfileModel))]
    [InlineData(typeof(PlayerByFiltersStatsDto), typeof(PlayerByFiltersStatsModel))]
    [InlineData(typeof(GetPlayersByFiltersViewModel), typeof(GetPlayersByFiltersResponse))]
    public void Mapping_GetPlayersByFilters_ShouldHaveValidConfiguration(Type source, Type destination)
    {
        // Arrange
        object instance = GetInstanceOf(source);

        // Assert
        _mapper.Map(instance, source, destination);
    }

    [Theory]
    [Trait("Mapping", "SimpleTypes")]
    [InlineData(typeof(DayOfWeek), typeof(PlayerAvailableDay))]
    [InlineData(typeof(PlayerAvailableDay), typeof(DayOfWeek))]
    [InlineData(typeof(string), typeof(PlayerTag))]
    [InlineData(typeof(PlayerTag), typeof(string))]
    [InlineData(typeof(string), typeof(PlayerPhotoDto))]
    [InlineData(typeof(PlayerPhotoDto), typeof(string))]
    [InlineData(typeof(int), typeof(StatType))]
    [InlineData(typeof(StatType), typeof(int))]
    public void Mapping_SimpleTypes_ShouldHaveValidConfiguration(Type source, Type destination)
    {
        // Arrange
        object instance = GetInstanceOf(source);

        // Assert
        _mapper.Map(instance, source, destination);
    }

    [Theory]
    [Trait("Mapping", "ComplexTypes")]
    [InlineData(typeof(PlayerGeneralProfileDto), typeof(PlayerGeneralProfile))]
    [InlineData(typeof(Player), typeof(PlayerGeneralProfileDto))]
    [InlineData(typeof(PlayerPhotoDto), typeof(PlayerPhoto))]
    [InlineData(typeof(BasePlayerDto), typeof(Player))]
    [InlineData(typeof(PlayerAvailability), typeof(PlayerAvailabilityDto))]
    [InlineData(typeof(PlayerAvailabilityDto), typeof(PlayerAvailability))]
    [InlineData(typeof(PlayerAvailabilityDto), typeof(PlayerAvailabilityModel))]
    [InlineData(typeof(PlayerFootballProfile), typeof(PlayerFootballProfileDto))]
    [InlineData(typeof(PlayerFootballProfileDto), typeof(PlayerFootballProfileModel))]
    [InlineData(typeof(PlayerFootballProfileDto), typeof(PlayerFootballProfile))]
    [InlineData(typeof(PlayerGeneralProfileModel), typeof(PlayerGeneralProfileDto))]
    [InlineData(typeof(PlayerStatPoints), typeof(PlayerStatPointsDto))]
    [InlineData(typeof(PlayerStatPointsDto), typeof(PlayerStatPoints))]
    [InlineData(typeof(PlayerStatPointsDto), typeof(PlayerStatPointsModel))]
    [InlineData(typeof(PlayerProfileModel), typeof(PlayerProfileDto))]
    [InlineData(typeof(Player), typeof(PlayerProfileDto))]
    [InlineData(typeof(Player), typeof(PlayerStatsDto))]
    [InlineData(typeof(PlayerStatsModel), typeof(PlayerStatsDto))]
    [InlineData(typeof(PlayerStatValueDto), typeof(PlayerStat))]
    [InlineData(typeof(PlayerStatValueDto), typeof(PlayerStatValueModel))]
    [InlineData(typeof(PaginationModel), typeof(PaginationDto))]
    [InlineData(typeof(PaginationDto), typeof(Pagination))]
    [InlineData(typeof(SortingModel), typeof(SortingDto))]
    [InlineData(typeof(PlayerDto), typeof(Player))]
    [InlineData(typeof(PlayerDto), typeof(PlayerModel))]
    [InlineData(typeof(PageLinksDto), typeof(PageLinksModel))]
    [InlineData(typeof(PageMetadataDto), typeof(PageMetadataModel))]
    public void Mapping_ComplexTypes_ShouldHaveValidConfiguration(Type source, Type destination)
    {
        // Arrange
        object instance = GetInstanceOf(source);

        // Assert
        _mapper.Map(instance, source, destination);
    }

    [Theory]
    [Trait("Mapping", "GenericTypes")]
    [InlineData(typeof(RangeLimitModel<int>), typeof(RangeLimitDto<int>))]
    [InlineData(typeof(PagedList<Player>), typeof(PageDto<PlayerByFiltersDto>))]
    [InlineData(typeof(PagedList<Player>), typeof(PageMetadataDto))]
    public void Mapping_GenericTypes_ShouldHaveValidConfiguration(Type source, Type destination)
    {
        // Arrange
        object instance = GetInstanceOf(source);

        // Assert
        _mapper.Map(instance, source, destination);
    }

    private static object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        if (type == typeof(string))
            return string.Empty;

        // Type without parameterless constructor
        return FormatterServices.GetUninitializedObject(type);
    }
}
