using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Moq;

using SFC.Players.Api.Controllers;
using SFC.Players.Application.Common.Constants;
using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Common.Dto.Pagination;
using SFC.Players.Application.Features.Players.Commands.Create;
using SFC.Players.Application.Features.Players.Commands.Update;
using SFC.Players.Application.Features.Players.Common.Dto;
using SFC.Players.Application.Features.Players.Queries.Get;
using SFC.Players.Application.Features.Players.Queries.GetByFilters;
using SFC.Players.Application.Features.Players.Queries.GetByFilters.Dto.Result;
using SFC.Players.Application.Features.Players.Queries.GetByUser.Dto;
using SFC.Players.Application.Interfaces.Identity;
using SFC.Players.Application.Models.Base;
using SFC.Players.Application.Models.Common;
using SFC.Players.Application.Models.Common.Pagination;
using SFC.Players.Application.Models.Players.Create;
using SFC.Players.Application.Models.Players.Get;
using SFC.Players.Application.Models.Players.GetByFilters;
using SFC.Players.Application.Models.Players.GetByFilters.Filters;
using SFC.Players.Application.Models.Players.Update;

namespace SFC.Players.Api.UnitTests.Controllers;
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
        CreatePlayerViewModel model = new() { Player = new PlayerDto() };
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
        GetPlayerViewModel model = new() { Player = new PlayerDto() };
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
        GetPlayerByUserViewModel model = new() { Player = new PlayerByUserDto() };
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
    public async Task API_Controller_Player_ShouldReturnSuccessResponseForGetPlayersByFilters()
    {
        // Arrange
        httpContext.Setup(x => x.Request.QueryString).Returns(new QueryString("?Pagination.Page=1&Pagination.Size=10"));
        GetPlayersByFiltersRequest request = new()
        {
            Filter = new GetPlayersByFiltersFilterModel(),
            Pagination = new PaginationModel(),
            Sorting = new List<SortingModel>()
        };
        GetPlayersByFiltersViewModel model = new() { Items = new List<PlayerByFiltersDto>(), Metadata = new PageMetadataDto() };
        _userServiceMock.Setup(m => m.UserId).Returns(Guid.NewGuid());
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetPlayersByFiltersQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(model);

        // Act
        ActionResult<GetPlayersByFiltersResponse> result = await _controller.GetPlayersByFiltersAsync(request);

        // Assert
        AssertResponse<GetPlayersByFiltersResponse, OkObjectResult>(result);
        _mediatorMock.Verify(m => m.Send(It.IsAny<GetPlayersByFiltersQuery>(), It.IsAny<CancellationToken>()), Times.Once);
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
