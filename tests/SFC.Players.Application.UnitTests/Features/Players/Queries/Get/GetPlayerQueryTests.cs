using AutoMapper;

using Microsoft.Extensions.Logging;

using Moq;

using SFC.Players.Application.Common.Constants;
using SFC.Players.Application.Common.Enums;
using SFC.Players.Application.Common.Exceptions;
using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Players.Queries.Get;
using SFC.Players.Application.Interfaces.Persistence;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.UnitTests.Features.Players.Queries.Get;
public class GetPlayerQueryTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IPlayerRepository> _mockPlayerRepository = new();

    public GetPlayerQueryTests()
    {
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>())
                                                    .CreateMapper();
    }

    [Fact]
    [Trait("Feature", "GetPlayer")]
    public async Task Feature_GetPlayer_ShouldCallAllRelevantMethods()
    {
        // Arrange
        GetPlayerQuery query = new()
        {
            PlayerId = 1
        };

        _mockPlayerRepository.Setup(r => r.GetByIdAsync(query.PlayerId)).ReturnsAsync(new Player());

        GetPlayerQueryHandler handler = new(_mapper, _mockPlayerRepository.Object);

        // Act
        GetPlayerViewModel result = await handler.Handle(query, new CancellationToken());

        // Assert
        _mockPlayerRepository.Verify(mock => mock.GetByIdAsync(query.PlayerId), Times.Once());
    }

    [Fact]
    [Trait("Feature", "GetPlayer")]
    public async Task Feature_GetPlayer_ShouldThrowNotFoundException()
    {
        // Arrange
        GetPlayerQuery query = new()
        {
            PlayerId = 1
        };

        _mockPlayerRepository.Setup(r => r.GetByIdAsync(query.PlayerId)).ReturnsAsync((Player)null!);

        GetPlayerQueryHandler handler = new(_mapper, _mockPlayerRepository.Object);

        // Act
        NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(async () =>
           await handler.Handle(query, new CancellationToken()));

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(Messages.PlayerNotFound, exception.Message);
    }

    [Fact]
    [Trait("Feature", "GetPlayer")]
    public async Task Feature_GetPlayer_ShouldReturnPlayer()
    {
        // Arrange
        GetPlayerQuery query = new()
        {
            PlayerId = 1
        };

        _mockPlayerRepository.Setup(r => r.GetByIdAsync(query.PlayerId)).ReturnsAsync(new Player());

        GetPlayerQueryHandler handler = new(_mapper, _mockPlayerRepository.Object);

        // Act
        GetPlayerViewModel result = await handler.Handle(query, new CancellationToken());

        // Assert
        Assert.NotNull(result.Player);
        Assert.Equal(RequestId.GetPlayer, query.RequestId);
        Assert.Equal(new EventId(2, "GetPlayer"), query.EventId);
    }
}
