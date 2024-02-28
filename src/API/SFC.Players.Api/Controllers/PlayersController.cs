using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SFC.Players.Api.Extensions;
using SFC.Players.Application.Features.Common.Base;
using SFC.Players.Application.Features.Players.Commands.Create;
using SFC.Players.Application.Features.Players.Commands.Update;
using SFC.Players.Application.Features.Players.Queries.Get;
using SFC.Players.Application.Features.Players.Queries.GetByFilters;
using SFC.Players.Application.Features.Players.Queries.GetByFilters.Dto.Filters;
using SFC.Players.Application.Models.Common.Pagination;
using SFC.Players.Application.Models.Players.Create;
using SFC.Players.Application.Models.Players.Get;
using SFC.Players.Application.Models.Players.GetByFilters;
using SFC.Players.Application.Models.Players.Update;

namespace SFC.Players.Api.Controllers;

[Authorize]
public class PlayersController : ApiControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreatePlayerResponse>> CreatePlayerAsync([FromBody] CreatePlayerRequest request)
    {
        CreatePlayerCommand command = Mapper.Map<CreatePlayerCommand>(request)
                                            .SetUserId<CreatePlayerCommand>(UserService.UserId);

        CreatePlayerViewModel player = await Mediator.Send(command);

        return CreatedAtRoute("GetPlayer", new { id = player.Player.Id }, Mapper.Map<CreatePlayerResponse>(player));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UpdatePlayerAsync([FromRoute] long id, [FromBody] UpdatePlayerRequest request)
    {
        UpdatePlayerCommand command = Mapper.Map<UpdatePlayerCommand>(request)
                                            .SetUserId<UpdatePlayerCommand>(UserService.UserId)
                                            .SetPlayerId(id);

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpGet("{id}", Name = "GetPlayer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetPlayerResponse>> GetPlayerAsync([FromRoute] long id)
    {
        GetPlayerQuery query = new() { PlayerId = id, UserId = UserService.UserId };

        GetPlayerViewModel player = await Mediator.Send(query);

        return Ok(Mapper.Map<GetPlayerResponse>(player));
    }

    [HttpGet("byuser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<GetPlayerByUserResponse>> GetPlayerByUserAsync()
    {
        GetPlayerByUserQuery query = new() { UserId = UserService.UserId };

        GetPlayerByUserViewModel? player = await Mediator.Send(query);

        return Ok(Mapper.Map<GetPlayerByUserResponse>(player) ?? GetPlayerByUserResponse.SuccessResult);
    }

    [HttpGet("byfilters")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<GetPlayersByFiltersResponse>> GetPlayersByFiltersAsync([FromQuery] GetPlayersByFiltersRequest request)
    {
        BasePaginationRequest<GetPlayersByFiltersViewModel, GetPlayersByFiltersFilterDto> query = Mapper.Map<GetPlayersByFiltersQuery>(request)
                                               .SetUserId<GetPlayersByFiltersQuery>(UserService.UserId)
                                               .SetRoute(Request.Path.Value!)
                                               .SetQueryString(Request.QueryString.Value!);

        GetPlayersByFiltersViewModel result = await Mediator.Send(query);

        Response.AddPaginationHeader(Mapper.Map<PageMetadataModel>(result.Metadata));

        return Ok(Mapper.Map<GetPlayersByFiltersResponse>(result));
    }
}
