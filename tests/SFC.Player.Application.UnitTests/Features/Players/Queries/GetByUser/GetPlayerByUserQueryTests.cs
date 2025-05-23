//using AutoMapper;

//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;

//using Moq;

//using SFC.Player.Application.Common.Enums;
//using SFC.Player.Application.Common.Mappings;
//using SFC.Player.Application.Features.Players.Queries.Get;
//using SFC.Player.Application.Interfaces.Identity;
//using SFC.Player.Application.Interfaces.Persistence.Repository;
//using SFC.Player.Domain.Entities;

//using PlayerEntity = SFC.Player.Domain.Entities.Player;

//namespace SFC.Player.Application.UnitTests.Features.Player.Queries.GetByUser;
//public class GetPlayerByUserQueryTests
//{
//    private readonly Guid MOCK_USER_ID = Guid.Parse("db69fc8c-cd50-4c99-96b3-9ddb6c49d08b");
//    private readonly IMapper _mapper;
//    private readonly Mock<IPlayerRepository> _playerRepositoryMock = new();
//    private readonly Mock<IUserService> _userServiceMock = new();

//    public GetPlayerByUserQueryTests()
//    {
//        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>())
//                                                    .CreateMapper();
//    }

//    [Fact]
//    [Trait("Feature", "GetPlayerByUser")]
//    public async Task Feature_GetPlayerByUser_ShouldCallAllRelevantMethods()
//    {
//        // Arrange
//        GetPlayerByUserQuery query = new();

//        _playerRepositoryMock.Setup(r => r.GetByUserIdAsync(MOCK_USER_ID)).ReturnsAsync(new PlayerEntity());

//        GetPlayerByUserQueryHandler handler = new(_mapper, _playerRepositoryMock.Object, _userServiceMock.Object);

//        // Act
//        GetPlayerByUserViewModel? result = await handler.Handle(query, new CancellationToken());

//        // Assert
//        _playerRepositoryMock.Verify(mock => mock.GetByUserIdAsync(MOCK_USER_ID), Times.Once());
//    }

//    [Fact]
//    [Trait("Feature", "GetPlayerByUser")]
//    public async Task Feature_GetPlayerByUser_ShouldFoundAndReturnPlayer()
//    {
//        // Arrange
//        GetPlayerByUserQuery query = new();

//        _playerRepositoryMock.Setup(r => r.GetByUserIdAsync(MOCK_USER_ID)).ReturnsAsync(new PlayerEntity());

//        GetPlayerByUserQueryHandler handler = new(_mapper, _playerRepositoryMock.Object, _userServiceMock.Object);

//        // Act
//        GetPlayerByUserViewModel? result = await handler.Handle(query, new CancellationToken());

//        // Assert
//        Assert.NotNull(result);
//        Assert.NotNull(result.Player);
//        Assert.Equal(RequestId.GetPlayerByUser, query.RequestId);
//        Assert.Equal(new EventId(3, "GetPlayerByUser"), query.EventId);
//    }

//    [Fact]
//    [Trait("Feature", "GetPlayerByUser")]
//    public async Task Feature_GetPlayerByUser_ShouldNotFoundPlayer()
//    {
//        // Arrange
//        GetPlayerByUserQuery query = new();

//        _playerRepositoryMock.Setup(r => r.GetByUserIdAsync(MOCK_USER_ID)).ReturnsAsync((PlayerEntity)null!);

//        GetPlayerByUserQueryHandler handler = new(_mapper, _playerRepositoryMock.Object, _userServiceMock.Object);

//        // Act
//        GetPlayerByUserViewModel? result = await handler.Handle(query, new CancellationToken());

//        // Assert
//        Assert.Null(result);
//    }
//}
