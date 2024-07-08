using AutoMapper;

using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Common.Dto;
using SFC.Player.Application.Features.Common.Dto.Pagination;
using SFC.Player.Application.Features.Common.Models.Paging;
using SFC.Player.Application.Features.Players.Commands.Create;
using SFC.Player.Application.Features.Players.Commands.Update;
using SFC.Player.Application.Features.Players.Common.Dto;
using SFC.Player.Application.Features.Players.Queries.Get;
using SFC.Player.Application.Features.Players.Queries.Find;
using SFC.Player.Application.Features.Players.Queries.Find.Dto.Filters;
using SFC.Player.Application.Models.Common;
using SFC.Player.Application.Models.Common.Pagination;
using SFC.Player.Domain.Entities;
using SFC.Player.Domain.Entities.Data;
using SFC.Player.Application.Models.Players.Update;
using SFC.Player.Application.Models.Players.Create;
using SFC.Player.Application.Models.Players.Get;
using SFC.Player.Application.Models.Players.Find.Filters;
using SFC.Player.Application.Models.Players.Find;
using SFC.Player.Application.Models.Players.Common;
using SFC.Player.Application.Models.Players.GetByUser;
using Newtonsoft.Json;

#region Usings

using PlayerByUserModel = SFC.Player.Application.Models.Players.GetByUser.Result.PlayerModel;
using PlayerFootballProfileByUserModel = SFC.Player.Application.Models.Players.GetByUser.Result.PlayerFootballProfileModel;
using PlayerGeneralProfileByUserModel = SFC.Player.Application.Models.Players.GetByUser.Result.PlayerGeneralProfileModel;
using PlayerProfileByUserModel = SFC.Player.Application.Models.Players.GetByUser.Result.PlayerProfileModel;
using PlayerEntity = SFC.Player.Domain.Entities.Player;
using GetPlayersDto = SFC.Player.Application.Features.Players.Queries.Find.Dto.Result.PlayerDto;
using GetPlayersFootballProfileDto = SFC.Player.Application.Features.Players.Queries.Find.Dto.Result.PlayerFootballProfileDto;
using GetPlayersGeneralProfileDto = SFC.Player.Application.Features.Players.Queries.Find.Dto.Result.PlayerGeneralProfileDto;
using GetPlayersProfileDto = SFC.Player.Application.Features.Players.Queries.Find.Dto.Result.PlayerProfileDto;
using GetPlayersStatsDto = SFC.Player.Application.Features.Players.Queries.Find.Dto.Result.PlayerStatsDto;
using GetPlayersFootballProfileModel = SFC.Player.Application.Models.Players.Find.Result.PlayerFootballProfileModel;
using GetPlayersGeneralProfileModel = SFC.Player.Application.Models.Players.Find.Result.PlayerGeneralProfileModel;
using GetPlayersModel = SFC.Player.Application.Models.Players.Find.Result.PlayerModel;
using GetPlayersProfileModel = SFC.Player.Application.Models.Players.Find.Result.PlayerProfileModel;
using GetPlayersStatsModel = SFC.Player.Application.Models.Players.Find.Result.PlayerStatsModel;
using PlayerGeneralProfileDto = SFC.Player.Application.Features.Players.Common.Dto.PlayerGeneralProfileDto;
using PlayerDto = SFC.Player.Application.Features.Players.Common.Dto.PlayerDto;
using PlayerModel = SFC.Player.Application.Models.Players.Common.PlayerModel;
using PlayerByUserDto = SFC.Player.Application.Features.Players.Queries.GetByUser.Dto.PlayerDto;
using PlayerFootballProfileByUserDto = SFC.Player.Application.Features.Players.Queries.GetByUser.Dto.PlayerFootballProfileDto;
using PlayerGeneralProfileByUserDto = SFC.Player.Application.Features.Players.Queries.GetByUser.Dto.PlayerGeneralProfileDto;
using PlayerProfileByUserDto = SFC.Player.Application.Features.Players.Queries.GetByUser.Dto.PlayerProfileDto;

#endregion Usings

namespace SFC.Player.Application.UnitTests.Common.Mappings;
public class MappingTests
{
    private readonly MapperConfiguration _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config => config.AddProfile<MappingProfile>());
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
        object? instance = GetInstanceOf(source);

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
        object? instance = GetInstanceOf(source);

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
        object? instance = GetInstanceOf(source);

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
        object? instance = GetInstanceOf(source);

        // Assert
        _mapper.Map(instance, source, destination);
    }

    [Theory]
    [Trait("Mapping", "GetPlayers")]
    [InlineData(typeof(GetPlayersAvailabilityLimitModel), typeof(GetPlayersAvailabilityLimitDto))]
    [InlineData(typeof(GetPlayersFilterModel), typeof(GetPlayersFilterDto))]
    [InlineData(typeof(GetPlayersFootballProfileFilterModel), typeof(GetPlayersFootballProfileFilterDto))]
    [InlineData(typeof(GetPlayersGeneralProfileFilterModel), typeof(GetPlayersGeneralProfileFilterDto))]
    [InlineData(typeof(GetPlayersProfileFilterModel), typeof(GetPlayersProfileFilterDto))]
    [InlineData(typeof(GetPlayersStatsBySkillRangeLimitModel), typeof(GetPlayersStatsBySkillRangeLimitDto))]
    [InlineData(typeof(GetPlayersStatsFilterModel), typeof(GetPlayersStatsFilterDto))]
    [InlineData(typeof(PlayerEntity), typeof(GetPlayersDto))]
    [InlineData(typeof(PlayerFootballProfile), typeof(GetPlayersFootballProfileDto))]
    [InlineData(typeof(PlayerEntity), typeof(GetPlayersGeneralProfileDto))]
    [InlineData(typeof(PlayerEntity), typeof(GetPlayersProfileDto))]
    [InlineData(typeof(PlayerEntity), typeof(GetPlayersStatsDto))]
    [InlineData(typeof(GetPlayersRequest), typeof(GetPlayersQuery))]
    [InlineData(typeof(GetPlayersFootballProfileDto), typeof(GetPlayersFootballProfileModel))]
    [InlineData(typeof(GetPlayersGeneralProfileDto), typeof(GetPlayersGeneralProfileModel))]
    [InlineData(typeof(GetPlayersDto), typeof(GetPlayersModel))]
    [InlineData(typeof(GetPlayersProfileDto), typeof(GetPlayersProfileModel))]
    [InlineData(typeof(GetPlayersStatsDto), typeof(GetPlayersStatsModel))]
    [InlineData(typeof(GetPlayersViewModel), typeof(GetPlayersResponse))]
    public void Mapping_GetPlayers_ShouldHaveValidConfiguration(Type source, Type destination)
    {
        // Arrange
        object? instance = GetInstanceOf(source);

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
        object? instance = GetInstanceOf(source);

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
    [InlineData(typeof(PlayerFootballProfile), typeof(GetPlayersFootballProfileDto))]
    [InlineData(typeof(GetPlayersFootballProfileDto), typeof(GetPlayersFootballProfileModel))]
    [InlineData(typeof(PlayerFootballProfileDto), typeof(PlayerFootballProfile))]
    [InlineData(typeof(PlayerStatPoints), typeof(PlayerStatPointsDto))]
    [InlineData(typeof(PlayerStatPointsDto), typeof(PlayerStatPoints))]
    [InlineData(typeof(PlayerStatPointsDto), typeof(PlayerStatPointsModel))]
    [InlineData(typeof(PlayerEntity), typeof(GetPlayersProfileDto))]
    [InlineData(typeof(PlayerEntity), typeof(GetPlayersStatsDto))]
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
        object? instance = GetInstanceOf(source);

        // Assert
        _mapper.Map(instance, source, destination);
    }

    [Theory]
    [Trait("Mapping", "GenericTypes")]
    [InlineData(typeof(RangeLimitModel<int>), typeof(RangeLimitDto<int>))]
    [InlineData(typeof(PagedList<PlayerEntity>), typeof(PageDto<GetPlayersDto>))]
    [InlineData(typeof(PagedList<PlayerEntity>), typeof(PageMetadataDto))]
    public void Mapping_GenericTypes_ShouldHaveValidConfiguration(Type source, Type destination)
    {
        // Arrange
        object? instance = GetInstanceOf(source);

        // Assert
        _mapper.Map(instance, source, destination);
    }

    private static object? GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        if (type == typeof(string))
            return string.Empty;

        string json = JsonConvert.SerializeObject(new object(), type, null);

        return JsonConvert.DeserializeObject<object>(json);
    }
}
