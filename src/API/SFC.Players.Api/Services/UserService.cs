using System.Security.Claims;

using SFC.Players.Application.Common.Constants;
using SFC.Players.Application.Common.Exceptions;
using SFC.Players.Application.Interfaces.Identity;

namespace SFC.Players.Api.Services;
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
