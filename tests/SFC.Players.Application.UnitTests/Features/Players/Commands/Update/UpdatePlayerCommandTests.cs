using AutoMapper;

using Microsoft.Extensions.Logging;

using Moq;

using SFC.Players.Application.Common.Constants;
using SFC.Players.Application.Common.Enums;
using SFC.Players.Application.Common.Exceptions;
using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Players.Commands.Update;
using SFC.Players.Application.Interfaces.Persistence;
using SFC.Players.Application.Models.Players.Update;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.UnitTests.Features.Players.Commands.Update;
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

        _mockPlayerRepository.Setup(r => r.GetByIdAsync(command.PlayerId)).ReturnsAsync(new Player());
        _mockPlayerRepository.Setup(r => r.UpdateAsync(It.IsAny<Player>())).Verifiable();

        UpdatePlayerCommandHandler handler = new(_mapper, _mockPlayerRepository.Object);

        // Act
        await handler.Handle(command, new CancellationToken());

        // Assert
        Assert.Equal(RequestId.UpdatePlayer, command.RequestId);
        _mockPlayerRepository.Verify(mock => mock.GetByIdAsync(command.PlayerId), Times.Once());
        _mockPlayerRepository.Verify(mock => mock.UpdateAsync(It.IsAny<Player>()), Times.Once());
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

        _mockPlayerRepository.Setup(r => r.GetByIdAsync(command.PlayerId)).ReturnsAsync((Player)null!);

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

        _mockPlayerRepository.Setup(r => r.GetByIdAsync(command.PlayerId)).ReturnsAsync(new Player());

        UpdatePlayerCommandHandler handler = new(_mapper, _mockPlayerRepository.Object);

        // Act
        await handler.Handle(command, new CancellationToken());

        // Assert
        Assert.Equal(RequestId.UpdatePlayer, command.RequestId);
        Assert.Equal(new EventId(1, "UpdatePlayer"), command.EventId);
    }
}
