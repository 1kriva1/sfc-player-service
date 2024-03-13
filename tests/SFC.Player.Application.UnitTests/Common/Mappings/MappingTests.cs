using System.Runtime.Serialization;

using AutoMapper;
using AutoMapper.Internal;

using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Common.Dto;
using SFC.Player.Application.Features.Common.Dto.Pagination;
using SFC.Player.Application.Features.Common.Models.Paging;
using SFC.Player.Application.Features.Player.Commands.Create;
using SFC.Player.Application.Features.Player.Commands.Update;
using SFC.Player.Application.Features.Player.Common.Dto;
using SFC.Player.Application.Features.Player.Queries.Get;
using SFC.Player.Application.Features.Player.Queries.GetByFilters;
using SFC.Player.Application.Features.Player.Queries.GetByFilters.Dto.Filters;
using SFC.Player.Application.Features.Player.Queries.GetByFilters.Dto.Result;
using SFC.Player.Application.Features.Player.Queries.GetByUser.Dto;
using SFC.Player.Application.Models.Common;
using SFC.Player.Application.Models.Common.Pagination;
using SFC.Player.Application.Features.Player.Common;
using SFC.Player.Application.Features.Player.Create;
using SFC.Player.Application.Features.Player.Get;
using SFC.Player.Application.Features.Player.GetByFilters;
using SFC.Player.Application.Features.Player.GetByFilters.Filters;
using SFC.Player.Application.Features.Player.GetByFilters.Result;
using SFC.Player.Application.Features.Player.GetByUser.Result;
using SFC.Player.Application.Features.Player.Update;
using SFC.Player.Domain.Entities;
using SFC.Player.Domain.Entities.Data;

using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.UnitTests.Common.Mappings;
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
    [InlineData(typeof(CreatePlayerDto), typeof(PlayerEntity))]
    [InlineData(typeof(PlayerEntity), typeof(CreatePlayerViewModel))]
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
    [InlineData(typeof(UpdatePlayerDto), typeof(PlayerEntity))]
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
    [InlineData(typeof(PlayerEntity), typeof(GetPlayerViewModel))]
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
    [InlineData(typeof(PlayerEntity), typeof(GetPlayerByUserViewModel))]
    [InlineData(typeof(GetPlayerByUserViewModel), typeof(GetPlayerByUserResponse))]
    [InlineData(typeof(PlayerEntity), typeof(PlayerByUserDto))]
    [InlineData(typeof(PlayerFootballProfile), typeof(PlayerFootballProfileByUserDto))]
    [InlineData(typeof(PlayerEntity), typeof(PlayerGeneralProfileByUserDto))]
    [InlineData(typeof(PlayerEntity), typeof(PlayerProfileByUserDto))]
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
    [InlineData(typeof(PlayerEntity), typeof(PlayerByFiltersDto))]
    [InlineData(typeof(PlayerFootballProfile), typeof(PlayerByFiltersFootballProfileDto))]
    [InlineData(typeof(PlayerEntity), typeof(PlayerByFiltersGeneralProfileDto))]
    [InlineData(typeof(PlayerEntity), typeof(PlayerByFiltersProfileDto))]
    [InlineData(typeof(PlayerEntity), typeof(PlayerByFiltersStatsDto))]
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
    [InlineData(typeof(PlayerEntity), typeof(PlayerGeneralProfileDto))]
    [InlineData(typeof(PlayerPhotoDto), typeof(PlayerPhoto))]
    [InlineData(typeof(BasePlayerDto), typeof(PlayerEntity))]
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
    [InlineData(typeof(PlayerEntity), typeof(PlayerProfileDto))]
    [InlineData(typeof(PlayerEntity), typeof(PlayerStatsDto))]
    [InlineData(typeof(PlayerStatsModel), typeof(PlayerStatsDto))]
    [InlineData(typeof(PlayerStatValueDto), typeof(PlayerStat))]
    [InlineData(typeof(PlayerStatValueDto), typeof(PlayerStatValueModel))]
    [InlineData(typeof(PaginationModel), typeof(PaginationDto))]
    [InlineData(typeof(PaginationDto), typeof(Pagination))]
    [InlineData(typeof(SortingModel), typeof(SortingDto))]
    [InlineData(typeof(PlayerDto), typeof(PlayerEntity))]
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
    [InlineData(typeof(PagedList<PlayerEntity>), typeof(PageDto<PlayerByFiltersDto>))]
    [InlineData(typeof(PagedList<PlayerEntity>), typeof(PageMetadataDto))]
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
