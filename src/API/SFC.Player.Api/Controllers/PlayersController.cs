using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SFC.Player.Api.Extensions;
using SFC.Player.Application.Features.Common.Base;
using SFC.Player.Application.Features.Players.Commands.Create;
using SFC.Player.Application.Features.Players.Commands.Update;
using SFC.Player.Application.Features.Players.Queries.Get;
using SFC.Player.Application.Features.Players.Queries.Find;
using SFC.Player.Application.Features.Players.Queries.Find.Dto.Filters;
using SFC.Player.Application.Models.Common.Pagination;
using SFC.Player.Application.Models.Players.Update;
using SFC.Player.Application.Models.Players.Create;
using SFC.Player.Application.Models.Players.Get;
using SFC.Player.Application.Models.Players.Find;
using SFC.Player.Application.Models.Players.GetByUser;
using SFC.Player.Application.Models.Base;
using SFC.Player.Application.Common.Constants;

namespace SFC.Player.Api.Controllers;

[ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
public class PlayersController : ApiControllerBase
{
    /// <summary>
    /// Create new player.
    /// </summary>
    /// <param name="request">Create player request.</param>
    /// <returns>An ActionResult of type CreatePlayerResponse</returns>
    /// <response code="201">Returns **new** created player.</response>
    /// <response code="400">Returns **validation** errors.</response>
    /// <response code="401">Returns when **failed** authentication.</response>
    /// <response code="403">Returns when **failed** authorization.</response>
    [HttpPost]
    [Authorize(Policy.GENERAL)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreatePlayerResponse>> CreatePlayerAsync([FromBody] CreatePlayerRequest request)
    {
        CreatePlayerCommand command = Mapper.Map<CreatePlayerCommand>(request)
                                            .SetUserId<CreatePlayerCommand>(UserService.UserId);

        CreatePlayerViewModel player = await Mediator.Send(command);

        return CreatedAtRoute("GetPlayer", new { id = player.Player.Id }, Mapper.Map<CreatePlayerResponse>(player));
    }

    /// <summary>
    /// Update existing player.
    /// </summary>
    /// <param name="id">Player unique identifier.</param>
    /// <param name="request">Update player request.</param>
    /// <returns>No content</returns>
    /// <response code="204">Returns no content if player updated **successfully**.</response>
    /// <response code="400">Returns **validation** errors.</response>
    /// <response code="401">Returns when **failed** authentication.</response>
    /// <response code="403">Returns when **failed** authorization.</response>
    [HttpPut("{id}")]
    [Authorize(Policy.OWN_PLAYER)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UpdatePlayerAsync([FromRoute] long id, [FromBody] UpdatePlayerRequest request)
    {
        UpdatePlayerCommand command = Mapper.Map<UpdatePlayerCommand>(request)
                                            .SetUserId<UpdatePlayerCommand>(UserService.UserId)
                                            .SetPlayerId(id);

        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    /// Return player model by unique identifier.
    /// </summary>
    /// <param name="id">Player unique identifier.</param>
    /// <returns>An ActionResult of type GetPlayerResponse</returns>
    /// <response code="200">Returns player model.</response>
    /// <response code="401">Returns when **failed** authentication.</response>
    /// <response code="403">Returns when **failed** authorization.</response>
    /// <response code="404">Returns when player **not found** by unique identifier.</response>
    [HttpGet("{id}", Name = "GetPlayer")]
    [Authorize(Policy.GENERAL)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetPlayerResponse>> GetPlayerAsync([FromRoute] long id)
    {
        GetPlayerQuery query = new() { PlayerId = id, UserId = UserService.UserId };

        GetPlayerViewModel player = await Mediator.Send(query);

        return Ok(Mapper.Map<GetPlayerResponse>(player));
    }

    /// <summary>
    /// Return player model by authentication JWT token (player's unique identifier saved as claim in token payload).
    /// </summary>
    /// <returns>An ActionResult of type GetPlayerByUserResponse</returns>
    /// <response code="200">Returns thin player model.</response>
    /// <response code="401">Returns when **failed** authentication.</response>
    /// <response code="403">Returns when **failed** authorization.</response>
    [HttpGet("byuser")]
    [Authorize(Policy.GENERAL)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<GetPlayerByUserResponse>> GetPlayerByUserAsync()
    {
        GetPlayerByUserQuery query = new() { UserId = UserService.UserId };

        GetPlayerByUserViewModel? player = await Mediator.Send(query);

        return Ok(Mapper.Map<GetPlayerByUserResponse>(player) ?? GetPlayerByUserResponse.SuccessResult);
    }

    /// <summary>
    /// Return list of players
    /// </summary>
    /// <param name="request">Get players request.</param>
    /// <returns>An ActionResult of type GetPlayersResponse</returns>
    /// <response code="200">Returns list of players with pagination header.</response>
    /// <response code="400">Returns **validation** errors.</response>
    /// <response code="401">Returns when **failed** authentication.</response>
    /// <response code="403">Returns when **failed** authorization.</response>
    [HttpGet("find")]
    [Authorize(Policy.GENERAL)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GetPlayersResponse>> GetPlayersAsync([FromQuery] GetPlayersRequest request)
    {
        BasePaginationRequest<GetPlayersViewModel, GetPlayersFilterDto> query = Mapper.Map<GetPlayersQuery>(request)
                                               .SetUserId<GetPlayersQuery>(UserService.UserId)
                                               .SetRoute(Request.Path.Value!)
                                               .SetQueryString(Request.QueryString.Value!);

        GetPlayersViewModel result = await Mediator.Send(query);

        Response.AddPaginationHeader(Mapper.Map<PageMetadataModel>(result.Metadata));

        return Ok(Mapper.Map<GetPlayersResponse>(result));
    }
}
