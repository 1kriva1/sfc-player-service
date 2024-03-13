using AutoMapper;

using Microsoft.Extensions.Logging;

using Moq;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Common.Enums;
using SFC.Player.Application.Common.Exceptions;
using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.Commands.Update;
using SFC.Player.Application.Interfaces.Persistence;
using SFC.Player.Domain.Entities;

using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.UnitTests.Features.Player.Commands.Update;
public class UpdatePlayerCommandTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IPlayerRepository> _mockPlayerRepository = new();

    public UpdatePlayerCommandTests()
    {
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>())
                                                    .CreateMapper();
    }

    [Fact]
    [Trait("Feature", "UpdatePlayer")]
    public async Task Feature_UpdatePlayer_ShouldCallAllRelevantMethods()
    {
        // Arrange
        UpdatePlayerCommand command = new()
        {
            Player = new UpdatePlayerDto(),
            PlayerId = 1
        };

        _mockPlayerRepository.Setup(r => r.GetByIdAsync(command.PlayerId)).ReturnsAsync(new PlayerEntity());
        _mockPlayerRepository.Setup(r => r.UpdateAsync(It.IsAny<PlayerEntity>())).Verifiable();

        UpdatePlayerCommandHandler handler = new(_mapper, _mockPlayerRepository.Object);

        // Act
        await handler.Handle(command, new CancellationToken());

        // Assert
        Assert.Equal(RequestId.UpdatePlayer, command.RequestId);
        _mockPlayerRepository.Verify(mock => mock.GetByIdAsync(command.PlayerId), Times.Once());
        _mockPlayerRepository.Verify(mock => mock.UpdateAsync(It.IsAny<PlayerEntity>()), Times.Once());
    }

    [Fact]
    [Trait("Feature", "UpdatePlayer")]
    public async Task Feature_UpdatePlayer_ShouldThrowNotFoundException()
    {
        // Arrange
        UpdatePlayerCommand command = new()
        {
            Player = new UpdatePlayerDto(),
            PlayerId = 1
        };

        _mockPlayerRepository.Setup(r => r.GetByIdAsync(command.PlayerId)).ReturnsAsync((PlayerEntity)null!);

        UpdatePlayerCommandHandler handler = new(_mapper, _mockPlayerRepository.Object);

        // Act
        NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(async () =>
           await handler.Handle(command, new CancellationToken()));

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(Messages.PlayerNotFound, exception.Message);
    }

    [Fact]
    [Trait("Feature", "UpdatePlayer")]
    public async Task Feature_UpdatePlayer_ShouldUpdatePlayer()
    {
        // Arrange
        UpdatePlayerCommand command = new()
        {
            Player = new UpdatePlayerDto(),
            PlayerId = 1
        };

        _mockPlayerRepository.Setup(r => r.GetByIdAsync(command.PlayerId)).ReturnsAsync(new PlayerEntity());

        UpdatePlayerCommandHandler handler = new(_mapper, _mockPlayerRepository.Object);

        // Act
        await handler.Handle(command, new CancellationToken());

        // Assert
        Assert.Equal(RequestId.UpdatePlayer, command.RequestId);
        Assert.Equal(new EventId(1, "UpdatePlayer"), command.EventId);
    }
}
