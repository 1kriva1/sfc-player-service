using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using SFC.Player.Application.Interfaces.Identity;
using SFC.Player.Application.Interfaces.Persistence.Repository.Player;

namespace SFC.Player.Infrastructure.Authorization.OwnPlayer;
public class OwnPlayerHandler(IPlayerRepository playerRepository, IHttpContextAccessor httpContextAccessor, IUserService userService)
    : AuthorizationHandler<OwnPlayerRequirement>
{
    private readonly IPlayerRepository _playerRepository = playerRepository;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnPlayerRequirement requirement)
    {
        string? playerIdValue = _httpContextAccessor.HttpContext?.GetRouteValue("id")?.ToString();

        if (!long.TryParse(playerIdValue, out long playerId))
        {
            context.Fail(new AuthorizationFailureReason(this, "Route does not have \"id\" parameter value."));
            return;
        }

        Guid? userId = userService.GetUserId();

        if (!userId.HasValue)
        {
            context.Fail(new AuthorizationFailureReason(this, "User does not have NameIdentifier claim value."));
            return;
        }

        if (!await _playerRepository.AnyAsync(playerId, userId.Value).ConfigureAwait(true))
        {
            context.Fail(new AuthorizationFailureReason(this, $"User - {userId} does not related to this resource - {playerId}."));
            return;
        }

        context.Succeed(requirement);
    }
}
