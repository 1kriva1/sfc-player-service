using System.Runtime.Serialization;

using AutoMapper;
using AutoMapper.Internal;

using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Players.Commands.Create;
using SFC.Players.Application.Features.Players.Commands.Update;
using SFC.Players.Application.Features.Players.Queries.Get;
using SFC.Players.Application.Models.Players.Common;
using SFC.Players.Application.Models.Players.Common.Models;
using SFC.Players.Application.Models.Players.Create;
using SFC.Players.Application.Models.Players.Get;
using SFC.Players.Application.Models.Players.GetByUser;
using SFC.Players.Application.Models.Players.Update;
using SFC.Players.Domain.Entities;

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
    public void Mapping_GetPlayerByUser_ShouldHaveValidConfiguration(Type source, Type destination)
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
    [InlineData(typeof(PlayerPhotoDto), typeof(PlayerPhoto))]
    [InlineData(typeof(BasePlayerDto), typeof(Player))]
    [InlineData(typeof(Player), typeof(PlayerGeneralProfileModel))]
    [InlineData(typeof(Player), typeof(PlayerModel))]
    [InlineData(typeof(Player), typeof(PlayerProfileModel))]
    [InlineData(typeof(PlayerAvailability), typeof(PlayerAvailabilityDto))]
    [InlineData(typeof(PlayerAvailabilityDto), typeof(PlayerAvailability))]
    [InlineData(typeof(PlayerFootballProfile), typeof(PlayerFootballProfileDto))]
    [InlineData(typeof(PlayerFootballProfileDto), typeof(PlayerFootballProfile))]
    [InlineData(typeof(PlayerGeneralProfileModel), typeof(PlayerGeneralProfileDto))]
    [InlineData(typeof(PlayerStatPoints), typeof(PlayerStatPointsDto))]
    [InlineData(typeof(PlayerStatPointsDto), typeof(PlayerStatPoints))]
    [InlineData(typeof(PlayerProfileModel), typeof(PlayerProfileDto))]
    [InlineData(typeof(Player), typeof(PlayerStatsDto))]
    [InlineData(typeof(PlayerStat), typeof(PlayerStatValueDto))]
    [InlineData(typeof(PlayerStatValueDto), typeof(PlayerStat))]
    public void Mapping_ComplexTypes_ShouldHaveValidConfiguration(Type source, Type destination)
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
