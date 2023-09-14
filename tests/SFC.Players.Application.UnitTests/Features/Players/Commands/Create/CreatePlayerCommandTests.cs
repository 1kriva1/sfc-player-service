using AutoMapper;

using Microsoft.Extensions.Logging;

using Moq;

using SFC.Players.Application.Common.Enums;
using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Players.Commands.Create;
using SFC.Players.Application.Interfaces.Persistence;
using SFC.Players.Application.Models.Players.Create;
using SFC.Players.Domain.Entities;
using SFC.Players.Domain.Events;

namespace SFC.Players.Application.UnitTests.Features.Players.Commands.Create;
public class CreatePlayerCommandTests
{
    private readonly Guid MOCK_USER_ID = Guid.Parse("db69fc8c-cd50-4c99-96b3-9ddb6c49d08b");
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
            UserId = MOCK_USER_ID
        };

        Mock<Player> _mockPlayer = new();

        _mockPlayer.Setup(r => r.AddDomainEvent(It.IsAny<PlayerCreatedEvent>())).Verifiable();
        _mockPlayerRepository.Setup(r => r.AddAsync(It.IsAny<Player>())).ReturnsAsync(_mockPlayer.Object);
        _mockMapper.Setup(r => r.Map<Player>(command.Player)).Returns(_mockPlayer.Object);
        _mockMapper.Setup(r => r.Map<CreatePlayerViewModel>(_mockPlayer.Object)).Returns(new CreatePlayerViewModel());

        CreatePlayerCommandHandler handler = new(_mockMapper.Object, _mockPlayerRepository.Object);

        // Act
        CreatePlayerViewModel result = await handler.Handle(command, new CancellationToken());

        // Assert
        Assert.Equal(MOCK_USER_ID, _mockPlayer.Object.User.UserId);
        _mockPlayer.Verify(mock => mock.AddDomainEvent(It.IsAny<PlayerCreatedEvent>()), Times.Once());
        _mockPlayerRepository.Verify(mock => mock.AddAsync(_mockPlayer.Object), Times.Once());
    }

    [Fact]
    [Trait("Feature", "CreatePlayer")]
    public async Task Feature_CreatePlayer_ShouldCreatePlayer()
    {
        // Arrange
        CreatePlayerCommand command = new()
        {
            Player = new CreatePlayerDto(),
            UserId = MOCK_USER_ID
        };

        _mockPlayerRepository.Setup(r => r.AddAsync(It.IsAny<Player>())).ReturnsAsync(new Player());

        CreatePlayerCommandHandler handler = new(_mapper, _mockPlayerRepository.Object);

        // Act
        CreatePlayerViewModel result = await handler.Handle(command, new CancellationToken());

        // Assert
        Assert.NotNull(result);
        Assert.Equal(RequestId.CreatePlayer, command.RequestId);
        Assert.Equal(new EventId(0, "CreatePlayer"), command.EventId);
    }
}
