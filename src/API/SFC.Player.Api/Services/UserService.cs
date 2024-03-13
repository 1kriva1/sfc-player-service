using System.Security.Claims;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Common.Exceptions;
using SFC.Player.Application.Interfaces.Identity;

namespace SFC.Player.Api.Services;
public record UserService(IHttpContextAccessor Context) : IUserService
{
    public Guid UserId => Guid.Parse(Context.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? throw new AuthorizationException(Messages.AuthorizationError));
}

public record UserServiceDevelopment(IHttpContextAccessor Context) : IUserService
{
    public Guid UserId => string.IsNullOrEmpty(Context.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier))
        ? Guid.Parse("{38D9EF25-E935-489F-859A-3E66D226E5B2}")
        : Guid.Parse(Context.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
}
