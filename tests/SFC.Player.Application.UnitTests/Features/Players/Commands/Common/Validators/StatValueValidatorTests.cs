//using System.Text.Json;

//using FluentValidation.Results;

//using Moq;

//using SFC.Player.Application.Features.Players.Commands.Common.Validators;
//using SFC.Player.Application.Features.Players.Common.Dto;
//using SFC.Player.Application.Interfaces.Common;
//using SFC.Player.Application.Interfaces.Persistence.Repository;
//using SFC.Player.Domain.Entities.Data;

//namespace SFC.Player.Application.UnitTests.Features.Player.Commands.Common.Validators;
//public class StatValueValidatorTests
//{
//    private readonly Mock<IDateTimeService> _mockDateTimeService = new();
//    private readonly Mock<IStatTypeRepository> _statTypeRepository = new();
//    private readonly Mock<IDataRepository<FootballPosition>> _footballPositionRepository = new();
//    private readonly Mock<IDataRepository<GameStyle>> _gameStyleRepository = new();
//    private readonly Mock<IDataRepository<WorkingFoot>> _workingFootRepository = new();

//    private PlayerValidator<BasePlayerDto> Validator
//    {
//        get
//        {
//            return new(
//                _mockDateTimeService.Object,
//                _statTypeRepository.Object,
//                _footballPositionRepository.Object,
//                _workingFootRepository.Object,
//                _gameStyleRepository.Object);
//        }
//    }

//    public StatValueValidatorTests()
//    {
//        _mockDateTimeService.Setup(dt => dt.Now).Returns(DateTime.UtcNow);
//        _statTypeRepository.Setup(r => r.CountAsync()).ReturnsAsync(PlayerTestConstants.STAT_TYPES_COUNT);
//        _statTypeRepository.Setup(r => r.ListAllAsync()).ReturnsAsync(PlayerTestConstants.STAT_TYPES);
//        _footballPositionRepository.Setup(r => r.AnyAsync(It.IsAny<int>())).ReturnsAsync(true);
//        _gameStyleRepository.Setup(r => r.AnyAsync(It.IsAny<int>())).ReturnsAsync(true);
//        _workingFootRepository.Setup(r => r.AnyAsync(It.IsAny<int>())).ReturnsAsync(true);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_Stat_Values_ShouldFailValidationWhenCountIsNotEqualToConstant()
//    {
//        // Arrange
//        BasePlayerDto player = PlayerTestConstants.GetValidPlayer();
//        player.Stats.Values = new List<PlayerStatValueDto>();

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Equal(2, result.Errors.Count);

//        ValidationFailure failure = result.Errors.First();

//        Assert.Equal("Stat count is invalid.", failure.ErrorMessage);
//        Assert.Equal($"Stats.Values.Type", failure.PropertyName);
//    }

//    [Fact]
//    [Trait("Feature", "Validators")]
//    public async Task Feature_Validator_Stat_Values_ShouldFailValidationWhenTypeIsInvalid()
//    {
//        // Arrange
//        BasePlayerDto player = JsonSerializer.Deserialize<BasePlayerDto>(JsonSerializer.Serialize(PlayerTestConstants.GetValidPlayer()))!;
//        player.Stats.Values.ToList()[0].Type = 29;

//        // Act
//        ValidationResult result = await Validator.ValidateAsync(player);

//        // Assert
//        Assert.False(result.IsValid);
//        Assert.Single(result.Errors);

//        ValidationFailure failure = result.Errors.First();

//        Assert.Equal("Each value from 'Stats' must have Type in Stat Type range.", failure.ErrorMessage);
//        Assert.Equal($"Stats.Values.Type", failure.PropertyName);
//    }
//}
