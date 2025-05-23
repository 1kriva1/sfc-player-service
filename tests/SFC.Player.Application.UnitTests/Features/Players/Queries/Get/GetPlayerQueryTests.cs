//using AutoMapper;

//using Microsoft.Extensions.Logging;

//using Moq;

//using SFC.Player.Application.Common.Constants;
//using SFC.Player.Application.Common.Enums;
//using SFC.Player.Application.Common.Exceptions;
//using SFC.Player.Application.Common.Mappings;
//using SFC.Player.Application.Features.Players.Queries.Get;
//using SFC.Player.Application.Interfaces.Persistence.Repository;
//using SFC.Player.Domain.Entities;

//using PlayerEntity = SFC.Player.Domain.Entities.Player;

//namespace SFC.Player.Application.UnitTests.Features.Player.Queries.Get;
//public class GetPlayerQueryTests
//{
//    private readonly IMapper _mapper;
//    private readonly Mock<IPlayerRepository> _mockPlayerRepository = new();

//    public GetPlayerQueryTests()
//    {
//        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>())
//                                                    .CreateMapper();
//    }

//    [Fact]
//    [Trait("Feature", "GetPlayer")]
//    public async Task Feature_GetPlayer_ShouldCallAllRelevantMethods()
//    {
//        // Arrange
//        GetPlayerQuery query = new()
//        {
//            PlayerId = 1
//        };

//        _mockPlayerRepository.Setup(r => r.GetByIdAsync(query.PlayerId)).ReturnsAsync(new PlayerEntity());

//        GetPlayerQueryHandler handler = new(_mapper, _mockPlayerRepository.Object);

//        // Act
//        GetPlayerViewModel result = await handler.Handle(query, new CancellationToken());

//        // Assert
//        _mockPlayerRepository.Verify(mock => mock.GetByIdAsync(query.PlayerId), Times.Once());
//    }

//    [Fact]
//    [Trait("Feature", "GetPlayer")]
//    public async Task Feature_GetPlayer_ShouldThrowNotFoundException()
//    {
//        // Arrange
//        GetPlayerQuery query = new()
//        {
//            PlayerId = 1
//        };

//        _mockPlayerRepository.Setup(r => r.GetByIdAsync(query.PlayerId)).ReturnsAsync((PlayerEntity)null!);

//        GetPlayerQueryHandler handler = new(_mapper, _mockPlayerRepository.Object);

//        // Act
//        NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(async () =>
//           await handler.Handle(query, new CancellationToken()));

//        // Assert
//        Assert.NotNull(exception);
//        Assert.Equal(Localization.PlayerNotFound, exception.Message);
//    }

//    [Fact]
//    [Trait("Feature", "GetPlayer")]
//    public async Task Feature_GetPlayer_ShouldReturnPlayer()
//    {
//        // Arrange
//        GetPlayerQuery query = new()
//        {
//            PlayerId = 1
//        };

//        _mockPlayerRepository.Setup(r => r.GetByIdAsync(query.PlayerId)).ReturnsAsync(new PlayerEntity());

//        GetPlayerQueryHandler handler = new(_mapper, _mockPlayerRepository.Object);

//        // Act
//        GetPlayerViewModel result = await handler.Handle(query, new CancellationToken());

//        // Assert
//        Assert.NotNull(result.Player);
//        Assert.Equal(RequestId.GetPlayer, query.RequestId);
//        Assert.Equal(new EventId(2, "GetPlayer"), query.EventId);
//    }
//}
