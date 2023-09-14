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
