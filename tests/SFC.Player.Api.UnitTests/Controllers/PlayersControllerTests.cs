using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Moq;

using SFC.Player.Api.Controllers;
using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Common.Dto.Pagination;
using SFC.Player.Application.Features.Players.Commands.Create;
using SFC.Player.Application.Features.Players.Commands.Update;
using SFC.Player.Application.Features.Players.Queries.Get;
using SFC.Player.Application.Features.Players.Queries.Find;
using SFC.Player.Application.Interfaces.Identity;
using SFC.Player.Application.Models.Base;
using SFC.Player.Application.Models.Common;
using SFC.Player.Application.Models.Common.Pagination;
using SFC.Player.Application.Models.Players.Create;
using SFC.Player.Application.Models.Players.Update;
using SFC.Player.Application.Models.Players.Get;
using SFC.Player.Application.Models.Players.Find;
using SFC.Player.Application.Models.Players.Find.Filters;
using SFC.Player.Application.Models.Players.GetByUser;

namespace SFC.Player.Api.UnitTests.Controllers;
public class PlayersControllerTests
{
    private readonly Mock<ISender> _mediatorMock = new();
    private readonly Mock<IUserService> _userServiceMock = new();
    private readonly Mock<HttpContext> httpContext = new();
    private readonly IMapper _mapper;
    private readonly PlayersController _controller;

    public PlayersControllerTests()
    {
        httpContext.Setup(x => x.RequestServices.GetService(typeof(ISender)))
           .Returns(_mediatorMock.Object);
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>())
           .CreateMapper();
        httpContext.Setup(x => x.RequestServices.GetService(typeof(IMapper)))
           .Returns(_mapper);
        httpContext.Setup(x => x.RequestServices.GetService(typeof(IUserService)))
           .Returns(_userServiceMock.Object);
        httpContext.Setup(x => x.Request.Path)
           .Returns(new PathString());
        httpContext.Setup(x => x.Response.Headers)
           .Returns(new HeaderDictionary());

        _controller = new PlayersController();
        _controller.ControllerContext.HttpContext = httpContext.Object;
    }

    [Fact]
    [Trait("API", "Controller")]
    public async Task API_Controller_Player_ShouldReturnSuccessResponseForCreate()
    {
        // Arrange
        CreatePlayerRequest request = new();
        CreatePlayerViewModel model = new() { Player = new Application.Features.Players.Common.Dto.PlayerDto() };
        _userServiceMock.Setup(m => m.UserId).Returns(Guid.NewGuid());
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreatePlayerCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(model);

        // Act
        ActionResult<CreatePlayerResponse> result = await _controller.CreatePlayerAsync(request);

        // Assert
        AssertResponse<CreatePlayerResponse, CreatedAtRouteResult>(result);
        _mediatorMock.Verify(m => m.Send(It.IsAny<CreatePlayerCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    [Trait("API", "Controller")]
    public async Task API_Controller_Player_ShouldReturnSuccessResponseForUpdate()
    {
        // Arrange
        UpdatePlayerRequest request = new();
        _userServiceMock.Setup(m => m.UserId).Returns(Guid.NewGuid());
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdatePlayerCommand>(), It.IsAny<CancellationToken>())).Verifiable();

        // Act
        ActionResult result = await _controller.UpdatePlayerAsync(1, request);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _mediatorMock.Verify(m => m.Send(It.IsAny<UpdatePlayerCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    [Trait("API", "Controller")]
    public async Task API_Controller_Player_ShouldReturnSuccessResponseForGetPlayer()
    {
        // Arrange
        GetPlayerViewModel model = new() { Player = new Application.Features.Players.Common.Dto.PlayerDto() };
        _userServiceMock.Setup(m => m.UserId).Returns(Guid.NewGuid());
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetPlayerQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(model);

        // Act
        ActionResult<GetPlayerResponse> result = await _controller.GetPlayerAsync(1);

        // Assert
        AssertResponse<GetPlayerResponse, OkObjectResult>(result);
        _mediatorMock.Verify(m => m.Send(It.IsAny<GetPlayerQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    [Trait("API", "Controller")]
    public async Task API_Controller_Player_ShouldReturnSuccessResponseForGetPlayerByUser()
    {
        // Arrange
        GetPlayerByUserViewModel model = new() { Player = new Application.Features.Players.Queries.GetByUser.Dto.PlayerDto() };
        _userServiceMock.Setup(m => m.UserId).Returns(Guid.NewGuid());
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetPlayerByUserQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(model);

        // Act
        ActionResult<GetPlayerByUserResponse> result = await _controller.GetPlayerByUserAsync();

        // Assert
        AssertResponse<GetPlayerByUserResponse, OkObjectResult>(result);
        _mediatorMock.Verify(m => m.Send(It.IsAny<GetPlayerByUserQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    [Trait("API", "Controller")]
    public async Task API_Controller_Player_ShouldReturnSuccessResponseForGetPlayers()
    {
        // Arrange
        httpContext.Setup(x => x.Request.QueryString).Returns(new QueryString("?Pagination.Page=1&Pagination.Size=10"));
        GetPlayersRequest request = new()
        {
            Filter = new GetPlayersFilterModel(),
            Pagination = new PaginationModel(),
            Sorting = new List<SortingModel>()
        };
        GetPlayersViewModel model = new() { Items = new List<Application.Features.Players.Queries.Find.Dto.Result.PlayerDto>(), Metadata = new PageMetadataDto() };
        _userServiceMock.Setup(m => m.UserId).Returns(Guid.NewGuid());
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetPlayersQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(model);

        // Act
        ActionResult<GetPlayersResponse> result = await _controller.GetPlayersAsync(request);

        // Assert
        AssertResponse<GetPlayersResponse, OkObjectResult>(result);
        _mediatorMock.Verify(m => m.Send(It.IsAny<GetPlayersQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    private static void AssertResponse<T, R>(ActionResult<T> result) where T : BaseErrorResponse where R : ObjectResult
    {
        ActionResult<T> actionResult = Assert.IsType<ActionResult<T>>(result);

        R? objectResult = Assert.IsType<R>(actionResult.Result);

        T response = Assert.IsType<T>(objectResult.Value);

        Assert.True(response?.Success);
        Assert.Equal(Messages.SuccessResult, response?.Message);
    }
}
