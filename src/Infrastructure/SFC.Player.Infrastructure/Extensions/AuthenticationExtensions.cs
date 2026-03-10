using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace SFC.Player.Infrastructure.Extensions;

public static class AuthenticationExtensions
{
    public static bool UseAuthentication(this IWebHostEnvironment environment, IConfiguration configuration)
    {
        return environment.IsProduction() || configuration.UseAuthentication();
    }
}