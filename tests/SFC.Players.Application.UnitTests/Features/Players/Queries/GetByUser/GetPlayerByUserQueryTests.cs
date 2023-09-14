using AutoMapper;

using Microsoft.Extensions.Logging;

using Moq;

using SFC.Players.Application.Common.Enums;
using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Players.Queries.Get;
using SFC.Players.Application.Interfaces.Persistence;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.UnitTests.Features.Players.Queries.GetByUser;
public class GetPlayerByUserQueryTests
{
    private readonly Guid MOCK_USER_ID = Guid.Parse("db69fc8c-cd50-4c99-96b3-9ddb6c49d08b");
    private readonly IMapper _mapper;
    private readonly Mock<IPlayerRepository> _mockPlayerRepository = new();

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

        _mockPlayerRepository.Setup(r => r.GetByUserIdAsync(query.UserId)).ReturnsAsync(new Player());

        GetPlayerByUserQueryHandler handler = new(_mapper, _mockPlayerRepository.Object);

        // Act
        GetPlayerByUserViewModel? result = await handler.Handle(query, new CancellationToken());

        // Assert
        _mockPlayerRepository.Verify(mock => mock.GetByUserIdAsync(query.UserId), Times.Once());
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

        _mockPlayerRepository.Setup(r => r.GetByUserIdAsync(query.UserId)).ReturnsAsync(new Player());

        GetPlayerByUserQueryHandler handler = new(_mapper, _mockPlayerRepository.Object);

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

        _mockPlayerRepository.Setup(r => r.GetByUserIdAsync(query.UserId)).ReturnsAsync((Player)null!);

        GetPlayerByUserQueryHandler handler = new(_mapper, _mockPlayerRepository.Object);

        // Act
        GetPlayerByUserViewModel? result = await handler.Handle(query, new CancellationToken());

        // Assert
        Assert.Null(result);
    }
}
