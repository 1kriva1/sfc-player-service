using System.Security.Claims;

using IdentityModel;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace SFC.Player.Infrastructure.Extensions;
public static class HttpContextExtensions
{
    public static Guid? GetUserId(this IHttpContextAccessor httpContextAccessor)
    {
        Claim? nameClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
        return nameClaim != null ? Guid.Parse(nameClaim.Value) : null;
    }

    public static Task<string?> GetAccessTokenAsync(this IHttpContextAccessor httpContextAccessor) =>
        httpContextAccessor.HttpContext?.GetTokenAsync(OidcConstants.TokenTypes.AccessToken)!;
}
