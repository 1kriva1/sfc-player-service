//using AutoMapper;

//using SFC.Player.Application.Common.Mappings;
//using SFC.Player.Application.Features.Common.Dto;
//using SFC.Player.Application.Features.Common.Dto.Pagination;
//using SFC.Player.Application.Features.Common.Models.Paging;
//using SFC.Player.Application.Features.Players.Commands.Create;
//using SFC.Player.Application.Features.Players.Commands.Update;
//using SFC.Player.Application.Features.Players.Common.Dto;
//using SFC.Player.Application.Features.Players.Queries.Get;
//using SFC.Player.Application.Features.Players.Queries.Find;
//using SFC.Player.Application.Features.Players.Queries.Find.Dto.Filters;
//using SFC.Player.Domain.Entities;
//using SFC.Player.Domain.Entities.Data;
//using Newtonsoft.Json;

//#region Usings

//using PlayerEntity = SFC.Player.Domain.Entities.Player;
//using GetPlayersDto = SFC.Player.Application.Features.Players.Queries.Find.Dto.Result.PlayerDto;
//using GetPlayersFootballProfileDto = SFC.Player.Application.Features.Players.Queries.Find.Dto.Result.PlayerFootballProfileDto;
//using GetPlayersGeneralProfileDto = SFC.Player.Application.Features.Players.Queries.Find.Dto.Result.PlayerGeneralProfileDto;
//using GetPlayersProfileDto = SFC.Player.Application.Features.Players.Queries.Find.Dto.Result.PlayerProfileDto;
//using GetPlayersStatsDto = SFC.Player.Application.Features.Players.Queries.Find.Dto.Result.PlayerStatsDto;
//using PlayerGeneralProfileDto = SFC.Player.Application.Features.Players.Common.Dto.PlayerGeneralProfileDto;
//using PlayerDto = SFC.Player.Application.Features.Players.Common.Dto.PlayerDto;
//using PlayerByUserDto = SFC.Player.Application.Features.Players.Queries.GetByUser.Dto.PlayerDto;
//using PlayerFootballProfileByUserDto = SFC.Player.Application.Features.Players.Queries.GetByUser.Dto.PlayerFootballProfileDto;
//using PlayerGeneralProfileByUserDto = SFC.Player.Application.Features.Players.Queries.GetByUser.Dto.PlayerGeneralProfileDto;
//using PlayerProfileByUserDto = SFC.Player.Application.Features.Players.Queries.GetByUser.Dto.PlayerProfileDto;

//#endregion Usings

//namespace SFC.Player.Application.UnitTests.Common.Mappings;
//public class MappingTests
//{
//    private readonly MapperConfiguration _configuration;
//    private readonly IMapper _mapper;

//    public MappingTests()
//    {
//        _configuration = new MapperConfiguration(config => config.AddProfile<MappingProfile>());
//        _mapper = _configuration.CreateMapper();
//    }

//    [Fact]
//    [Trait("Mapping", "Profile")]
//    public void Mapping_Profile_ShouldHaveValidConfiguration()
//    {
//        // Assert
//        _configuration.AssertConfigurationIsValid();
//    }

//    [Theory]
//    [Trait("Mapping", "CreatePlayer")]
//    [InlineData(typeof(CreatePlayerDto), typeof(PlayerEntity))]
//    [InlineData(typeof(PlayerEntity), typeof(CreatePlayerViewModel))]
//    public void Mapping_CreatePlayer_ShouldHaveValidConfiguration(Type source, Type destination)
//    {
//        // Arrange
//        object? instance = GetInstanceOf(source);

//        // Assert
//        _mapper.Map(instance, source, destination);
//    }

//    [Theory]
//    [Trait("Mapping", "UpdatePlayer")]
//    [InlineData(typeof(UpdatePlayerDto), typeof(PlayerEntity))]
//    public void Mapping_UpdatePlayer_ShouldHaveValidConfiguration(Type source, Type destination)
//    {
//        // Arrange
//        object? instance = GetInstanceOf(source);

//        // Assert
//        _mapper.Map(instance, source, destination);
//    }

//    [Theory]
//    [Trait("Mapping", "GetPlayer")]
//    [InlineData(typeof(PlayerEntity), typeof(GetPlayerViewModel))]
//    public void Mapping_GetPlayer_ShouldHaveValidConfiguration(Type source, Type destination)
//    {
//        // Arrange
//        object? instance = GetInstanceOf(source);

//        // Assert
//        _mapper.Map(instance, source, destination);
//    }

//    [Theory]
//    [Trait("Mapping", "GetPlayerByUser")]
//    [InlineData(typeof(PlayerEntity), typeof(GetPlayerByUserViewModel))]
//    [InlineData(typeof(PlayerEntity), typeof(PlayerByUserDto))]
//    [InlineData(typeof(PlayerFootballProfile), typeof(PlayerFootballProfileByUserDto))]
//    [InlineData(typeof(PlayerEntity), typeof(PlayerGeneralProfileByUserDto))]
//    [InlineData(typeof(PlayerEntity), typeof(PlayerProfileByUserDto))]
//    public void Mapping_GetPlayerByUser_ShouldHaveValidConfiguration(Type source, Type destination)
//    {
//        // Arrange
//        object? instance = GetInstanceOf(source);

//        // Assert
//        _mapper.Map(instance, source, destination);
//    }

//    [Theory]
//    [Trait("Mapping", "GetPlayers")]
//    [InlineData(typeof(PlayerEntity), typeof(GetPlayersDto))]
//    [InlineData(typeof(PlayerFootballProfile), typeof(GetPlayersFootballProfileDto))]
//    [InlineData(typeof(PlayerEntity), typeof(GetPlayersGeneralProfileDto))]
//    [InlineData(typeof(PlayerEntity), typeof(GetPlayersProfileDto))]
//    [InlineData(typeof(PlayerEntity), typeof(GetPlayersStatsDto))]
//    public void Mapping_GetPlayers_ShouldHaveValidConfiguration(Type source, Type destination)
//    {
//        // Arrange
//        object? instance = GetInstanceOf(source);

//        // Assert
//        _mapper.Map(instance, source, destination);
//    }

//    [Theory]
//    [Trait("Mapping", "SimpleTypes")]
//    [InlineData(typeof(DayOfWeek), typeof(PlayerAvailableDay))]
//    [InlineData(typeof(PlayerAvailableDay), typeof(DayOfWeek))]
//    [InlineData(typeof(string), typeof(PlayerTag))]
//    [InlineData(typeof(PlayerTag), typeof(string))]
//    [InlineData(typeof(string), typeof(PlayerPhotoDto))]
//    [InlineData(typeof(PlayerPhotoDto), typeof(string))]
//    [InlineData(typeof(int), typeof(StatType))]
//    [InlineData(typeof(StatType), typeof(int))]
//    public void Mapping_SimpleTypes_ShouldHaveValidConfiguration(Type source, Type destination)
//    {
//        // Arrange
//        object? instance = GetInstanceOf(source);

//        // Assert
//        _mapper.Map(instance, source, destination);
//    }

//    [Theory]
//    [Trait("Mapping", "ComplexTypes")]
//    [InlineData(typeof(PlayerGeneralProfileDto), typeof(PlayerGeneralProfile))]
//    [InlineData(typeof(PlayerEntity), typeof(PlayerGeneralProfileDto))]
//    [InlineData(typeof(PlayerPhotoDto), typeof(PlayerPhoto))]
//    [InlineData(typeof(BasePlayerDto), typeof(PlayerEntity))]
//    [InlineData(typeof(PlayerAvailability), typeof(PlayerAvailabilityDto))]
//    [InlineData(typeof(PlayerAvailabilityDto), typeof(PlayerAvailability))]
//    [InlineData(typeof(PlayerFootballProfile), typeof(GetPlayersFootballProfileDto))]
//    [InlineData(typeof(PlayerFootballProfileDto), typeof(PlayerFootballProfile))]
//    [InlineData(typeof(PlayerStatPoints), typeof(PlayerStatPointsDto))]
//    [InlineData(typeof(PlayerStatPointsDto), typeof(PlayerStatPoints))]
//    [InlineData(typeof(PlayerEntity), typeof(GetPlayersProfileDto))]
//    [InlineData(typeof(PlayerEntity), typeof(GetPlayersStatsDto))]
//    [InlineData(typeof(PlayerStatValueDto), typeof(PlayerStat))]
//    [InlineData(typeof(PaginationDto), typeof(Pagination))]
//    [InlineData(typeof(PlayerDto), typeof(PlayerEntity))]
//    public void Mapping_ComplexTypes_ShouldHaveValidConfiguration(Type source, Type destination)
//    {
//        // Arrange
//        object? instance = GetInstanceOf(source);

//        // Assert
//        _mapper.Map(instance, source, destination);
//    }

//    [Theory]
//    [Trait("Mapping", "GenericTypes")]
//    [InlineData(typeof(PagedList<PlayerEntity>), typeof(PageDto<GetPlayersDto>))]
//    [InlineData(typeof(PagedList<PlayerEntity>), typeof(PageMetadataDto))]
//    public void Mapping_GenericTypes_ShouldHaveValidConfiguration(Type source, Type destination)
//    {
//        // Arrange
//        object? instance = GetInstanceOf(source);

//        // Assert
//        _mapper.Map(instance, source, destination);
//    }

//    private static object? GetInstanceOf(Type type)
//    {
//        if (type.GetConstructor(Type.EmptyTypes) != null)
//            return Activator.CreateInstance(type)!;

//        if (type == typeof(string))
//            return string.Empty;

//        string json = JsonConvert.SerializeObject(new object(), type, null);

//        return JsonConvert.DeserializeObject<object>(json);
//    }
//}
