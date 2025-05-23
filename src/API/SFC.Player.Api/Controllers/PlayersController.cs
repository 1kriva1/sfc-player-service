using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFC.Player.Application.Features.Common.Base;
using SFC.Player.Application.Features.Player.Commands.Create;
using SFC.Player.Application.Features.Player.Commands.Update;
using SFC.Player.Application.Features.Player.Queries.Get;
using SFC.Player.Application.Features.Player.Queries.Find;
using SFC.Player.Application.Features.Player.Queries.Find.Dto.Filters;
using SFC.Player.Api.Infrastructure.Extensions;
using SFC.Player.Api.Infrastructure.Models.Base;
using SFC.Player.Infrastructure.Constants;
using SFC.Player.Application.Features.Player.Queries.GetByUser;
using SFC.Player.Api.Infrastructure.Models.Pagination;
using SFC.Player.Api.Infrastructure.Models.Player.General.Create;
using SFC.Player.Api.Infrastructure.Models.Player.General.Find;
using SFC.Player.Api.Infrastructure.Models.Player.General.Get;
using SFC.Player.Api.Infrastructure.Models.Player.General.GetByUser;
using SFC.Player.Api.Infrastructure.Models.Player.General.Update;

namespace SFC.Player.Api.Controllers;

[Tags("Players")]
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
    [Authorize(Policy.General)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreatePlayerResponse>> CreatePlayerAsync([FromBody] CreatePlayerRequest request)
    {
        CreatePlayerCommand command = Mapper.Map<CreatePlayerCommand>(request);

        CreatePlayerViewModel player = await Mediator.Send(command).ConfigureAwait(true);

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
    [Authorize(Policy.OwnPlayer)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UpdatePlayerAsync([FromRoute] long id, [FromBody] UpdatePlayerRequest request)
    {
        UpdatePlayerCommand command = Mapper.Map<UpdatePlayerCommand>(request)
                                            .SetPlayerId(id);

        await Mediator.Send(command).ConfigureAwait(true);

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
    [Authorize(Policy.General)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetPlayerResponse>> GetPlayerAsync([FromRoute] long id)
    {
        GetPlayerQuery query = new() { PlayerId = id };

        GetPlayerViewModel player = await Mediator.Send(query).ConfigureAwait(true);

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
    [Authorize(Policy.General)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<GetPlayerByUserResponse>> GetPlayerByUserAsync()
    {
        GetPlayerByUserQuery query = new();

        GetPlayerByUserViewModel? player = await Mediator.Send(query).ConfigureAwait(true);

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
    [Authorize(Policy.General)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GetPlayersResponse>> GetPlayersAsync([FromQuery] GetPlayersRequest request)
    {
        BasePaginationRequest<GetPlayersViewModel, GetPlayersFilterDto> query = Mapper.Map<GetPlayersQuery>(request);

        GetPlayersViewModel result = await Mediator.Send(query).ConfigureAwait(true);

        PageMetadataModel metadata = Mapper.Map<PageMetadataModel>(result.Metadata)
                                           .SetLinks(UriService, Request.QueryString.Value!, Request.Path.Value!);

        Response.AddPaginationHeader(metadata);

        return Ok(Mapper.Map<GetPlayersResponse>(result));
    }
}
