using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using SFC.Player.Application.Interfaces.Persistence;

namespace SFC.Player.Infrastructure.Authorization.OwnPlayer;
public class OwnPlayerHandler(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
    : AuthorizationHandler<OwnPlayerRequirement>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnPlayerRequirement requirement)
    {
        string? playerIdValue = _httpContextAccessor.HttpContext?.GetRouteValue("id")?.ToString();

        if (!long.TryParse(playerIdValue, out long playerId))
        {
            context.Fail(new AuthorizationFailureReason(this, "Route does not have \"id\" parameter value."));
            return;
        }

        string? userIdValue = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdValue, out Guid userId))
        {
            context.Fail(new AuthorizationFailureReason(this, "User does not have NameIdentifier claim value."));
            return;
        }

        if (!await _userRepository.AnyAsync(playerId, userId))
        {
            context.Fail(new AuthorizationFailureReason(this, $"User - {userId} does not related to this resource - {playerId}."));
            return;
        }

        context.Succeed(requirement);
    }
}
