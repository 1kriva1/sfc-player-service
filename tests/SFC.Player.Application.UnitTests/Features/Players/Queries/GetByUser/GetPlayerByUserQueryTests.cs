using AutoMapper;

using Microsoft.Extensions.Logging;

using Moq;

using SFC.Player.Application.Common.Enums;
using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.Queries.Get;
using SFC.Player.Application.Interfaces.Persistence;
using SFC.Player.Domain.Entities;

using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.UnitTests.Features.Player.Queries.GetByUser;
public class GetPlayerByUserQueryTests
{
    private readonly Guid MOCK_USER_ID = Guid.Parse("db69fc8c-cd50-4c99-96b3-9ddb6c49d08b");
    private readonly IMapper _mapper;
    private readonly Mock<IPlayerRepository> _playerRepositoryMock = new();

    public GetPlayerByUserQueryTests()
    {
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>())
                                                    .CreateMapper();
    }

    [Fact]
    [Trait("Feature", "GetPlayerByUser")]
    public async Task Feature_GetPlayerByUser_ShouldCallAllRelevantMethods()
    {
        // Arrange
        GetPlayerByUserQuery query = new()
        {
            UserId = MOCK_USER_ID
        };

        _playerRepositoryMock.Setup(r => r.GetByUserIdAsync(query.UserId)).ReturnsAsync(new PlayerEntity());

        GetPlayerByUserQueryHandler handler = new(_mapper, _playerRepositoryMock.Object);

        // Act
        GetPlayerByUserViewModel? result = await handler.Handle(query, new CancellationToken());

        // Assert
        _playerRepositoryMock.Verify(mock => mock.GetByUserIdAsync(query.UserId), Times.Once());
    }

    [Fact]
    [Trait("Feature", "GetPlayerByUser")]
    public async Task Feature_GetPlayerByUser_ShouldFoundAndReturnPlayer()
    {
        // Arrange
        GetPlayerByUserQuery query = new()
        {
            UserId = MOCK_USER_ID
        };

        _playerRepositoryMock.Setup(r => r.GetByUserIdAsync(query.UserId)).ReturnsAsync(new PlayerEntity());

        GetPlayerByUserQueryHandler handler = new(_mapper, _playerRepositoryMock.Object);

        // Act
        GetPlayerByUserViewModel? result = await handler.Handle(query, new CancellationToken());

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Player);
        Assert.Equal(RequestId.GetPlayerByUser, query.RequestId);
        Assert.Equal(new EventId(3, "GetPlayerByUser"), query.EventId);
    }

    [Fact]
    [Trait("Feature", "GetPlayerByUser")]
    public async Task Feature_GetPlayerByUser_ShouldNotFoundPlayer()
    {
        // Arrange
        GetPlayerByUserQuery query = new()
        {
            UserId = MOCK_USER_ID
        };

        _playerRepositoryMock.Setup(r => r.GetByUserIdAsync(query.UserId)).ReturnsAsync((PlayerEntity)null!);

        GetPlayerByUserQueryHandler handler = new(_mapper, _playerRepositoryMock.Object);

        // Act
        GetPlayerByUserViewModel? result = await handler.Handle(query, new CancellationToken());

        // Assert
        Assert.Null(result);
    }
}
