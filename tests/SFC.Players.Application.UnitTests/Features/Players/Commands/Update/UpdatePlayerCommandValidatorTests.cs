using FluentValidation.Results;

using Moq;

using SFC.Players.Application.Common.Constants;
using SFC.Players.Application.Features.Players.Commands.Update;
using SFC.Players.Application.Features.Players.Common.Models;
using SFC.Players.Application.Interfaces.Common;
using SFC.Players.Application.Interfaces.Persistence;
using SFC.Players.Application.Models.Players.Common;
using SFC.Players.Application.Models.Players.Update;
using SFC.Players.Domain.Entities.Data;

namespace SFC.Players.Application.UnitTests.Features.Players.Commands.Update;
public class UpdatePlayerCommandValidatorTests
{
    private readonly Guid MOCK_USER_ID = Guid.Parse("db69fc8c-cd50-4c99-96b3-9ddb6c49d08b");
    private readonly UpdatePlayerDto VALID_PLAYER = new()
    {
        Profile = new PlayerProfileDto
        {
            General = new PlayerGeneralProfileDto
            {
                FirstName = "First Name",
                LastName = "Last Name",
                City = "City Value"
            },
            Football = new PlayerFootballProfileDto()
        },
        Stats = new PlayerStatsDto
        {
            Points = new PlayerStatPointsDto(),
            Values = PlayerTestConstants.VALID_STATS
        }
    };
    private readonly Mock<IDateTimeService> _mockDateTimeService = new();
    private readonly Mock<IUserRepository> _mockUserRepository = new();
    private readonly Mock<IStatCategoryRepository> _statCategoryRepository = new();
    private readonly Mock<IStatTypeRepository> _statTypeRepository = new();
    private readonly Mock<IDataRepository<FootballPosition>> _footballPositionRepository = new();
    private readonly Mock<IDataRepository<GameStyle>> _gameStyleRepository = new();
    private readonly Mock<IDataRepository<WorkingFoot>> _workingFootRepository = new();

    private UpdatePlayerCommandValidator Validator
    {
        get
        {
            return new(
                _mockDateTimeService.Object,
                _mockUserRepository.Object,
                _statCategoryRepository.Object,
                _statTypeRepository.Object,
                _footballPositionRepository.Object,
                _workingFootRepository.Object,
                _gameStyleRepository.Object);
        }
    }

    public UpdatePlayerCommandValidatorTests()
    {
        _statCategoryRepository.Setup(r => r.CountAsync(It.IsAny<IEnumerable<int>>())).ReturnsAsync(PlayerTestConstants.STAT_CATEGORIES_COUNT);
        _statTypeRepository.Setup(r => r.CountAsync(It.IsAny<IEnumerable<int>>())).ReturnsAsync(PlayerTestConstants.STAT_TYPES_COUNT);
        _statTypeRepository.Setup(r => r.CountAsync()).ReturnsAsync(PlayerTestConstants.STAT_TYPES_COUNT);
        _statTypeRepository.Setup(r => r.ListAllAsync()).ReturnsAsync(PlayerTestConstants.STAT_TYPES);
        _footballPositionRepository.Setup(r => r.AnyAsync(It.IsAny<int>())).ReturnsAsync(true);
        _gameStyleRepository.Setup(r => r.AnyAsync(It.IsAny<int>())).ReturnsAsync(true);
        _workingFootRepository.Setup(r => r.AnyAsync(It.IsAny<int>())).ReturnsAsync(true);
    }

    [Fact]
    [Trait("Feature", "UpdatePlayer")]
    public async Task Feature_UpdatePlayer_ShouldFailValidationWhenPlayerNotRelatedForUser()
    {
        // Arrange
        UpdatePlayerCommand command = new()
        {
            PlayerId = 1,
            Player = VALID_PLAYER,
            UserId = MOCK_USER_ID
        };

        _mockUserRepository.Setup(r => r.AnyAsync(command.PlayerId, command.UserId)).ReturnsAsync(false);

        // Act
        ValidationResult result = await Validator.ValidateAsync(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);

        ValidationFailure failure = result.Errors.First();

        Assert.Equal(Messages.PlayerNotRelatedToThisUser, failure.ErrorMessage);
        Assert.Equal(nameof(IPlayerRelatedRequest.PlayerId), failure.PropertyName);
    }

    [Fact]
    [Trait("Feature", "UpdatePlayer")]
    public async Task Feature_UpdatePlayer_ShouldFailValidationWhenNotFitCommonPlayerValidation()
    {
        // Arrange
        UpdatePlayerCommand command = new()
        {
            Player = VALID_PLAYER,
            UserId = MOCK_USER_ID
        };

        command.Player.Profile.General.FirstName = null!;
        command.Player.Profile.General.LastName = null!;
        command.Player.Profile.General.City = null!;

        _mockUserRepository.Setup(r => r.AnyAsync(command.PlayerId, command.UserId)).ReturnsAsync(true);

        // Act
        ValidationResult result = await Validator.ValidateAsync(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal(3, result.Errors.Count);

        ValidationFailure firstNameFailure = result.Errors.First();
        ValidationFailure lastNameFailure = result.Errors[1];
        ValidationFailure cityFailure = result.Errors[2];

        Assert.Equal("'FirstName' must not be empty.", firstNameFailure.ErrorMessage);
        Assert.Equal("'LastName' must not be empty.", lastNameFailure.ErrorMessage);
        Assert.Equal("'City' must not be empty.", cityFailure.ErrorMessage);
    }

    [Fact]
    [Trait("Feature", "UpdatePlayer")]
    public async Task Feature_UpdatePlayer_ShouldPassValidationWhenPlayerExistForUser()
    {
        // Arrange
        UpdatePlayerCommand command = new()
        {
            PlayerId = 1,
            Player = VALID_PLAYER,
            UserId = MOCK_USER_ID
        };

        _mockUserRepository.Setup(r => r.AnyAsync(command.PlayerId, command.UserId)).ReturnsAsync(true);

        // Act
        ValidationResult result = await Validator.ValidateAsync(command);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
}
