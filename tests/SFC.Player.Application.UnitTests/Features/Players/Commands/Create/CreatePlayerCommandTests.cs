using AutoMapper;

using Microsoft.Extensions.Logging;

using Moq;

using SFC.Player.Application.Common.Enums;
using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Players.Commands.Create;
using SFC.Player.Application.Interfaces.Persistence;
using SFC.Player.Domain.Events;

using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.UnitTests.Features.Player.Commands.Create;
public class CreatePlayerCommandTests
{
    private readonly Guid _userIdMock = Guid.Parse("db69fc8c-cd50-4c99-96b3-9ddb6c49d08b");
    private readonly Mock<IMapper> _mockMapper = new();
    private readonly IMapper _mapper;
    private readonly Mock<IPlayerRepository> _mockPlayerRepository = new();

    public CreatePlayerCommandTests()
    {
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>())
                                         .CreateMapper();
    }

    [Fact]
    [Trait("Feature", "CreatePlayer")]
    public async Task Feature_CreatePlayer_ShouldCallAllRelevantMethods()
    {
        // Arrange
        CreatePlayerCommand command = new()
        {
            Player = new CreatePlayerDto(),
            UserId = _userIdMock
        };

        Mock<PlayerEntity> mockPlayer = new();

        mockPlayer.Setup(r => r.AddDomainEvent(It.IsAny<PlayerCreatedEvent>())).Verifiable();
        _mockPlayerRepository.Setup(r => r.AddAsync(It.IsAny<PlayerEntity>())).ReturnsAsync(mockPlayer.Object);
        _mockMapper.Setup(r => r.Map<PlayerEntity>(command.Player)).Returns(mockPlayer.Object);
        _mockMapper.Setup(r => r.Map<CreatePlayerViewModel>(mockPlayer.Object)).Returns(new CreatePlayerViewModel());

        CreatePlayerCommandHandler handler = new(_mockMapper.Object, _mockPlayerRepository.Object);

        // Act
        CreatePlayerViewModel result = await handler.Handle(command, new CancellationToken());

        // Assert
        Assert.Equal(_userIdMock, mockPlayer.Object.User.Id);
        mockPlayer.Verify(mock => mock.AddDomainEvent(It.IsAny<PlayerCreatedEvent>()), Times.Once());
        _mockPlayerRepository.Verify(mock => mock.AddAsync(mockPlayer.Object), Times.Once());
    }

    [Fact]
    [Trait("Feature", "CreatePlayer")]
    public async Task Feature_CreatePlayer_ShouldCreatePlayer()
    {
        // Arrange
        CreatePlayerCommand command = new()
        {
            Player = new CreatePlayerDto(),
            UserId = _userIdMock
        };

        _mockPlayerRepository.Setup(r => r.AddAsync(It.IsAny<PlayerEntity>())).ReturnsAsync(new PlayerEntity());

        CreatePlayerCommandHandler handler = new(_mapper, _mockPlayerRepository.Object);

        // Act
        CreatePlayerViewModel result = await handler.Handle(command, new CancellationToken());

        // Assert
        Assert.NotNull(result);
        Assert.Equal(RequestId.CreatePlayer, command.RequestId);
        Assert.Equal(new EventId(0, "CreatePlayer"), command.EventId);
    }
}
