using Microsoft.Extensions.Configuration;

using SFC.Player.Application.Common.Settings;
using SFC.Player.Infrastructure.Settings;

namespace SFC.Player.Infrastructure.Extensions;
public static class SettingsExtensions
{
    public static JwtSettings GetJwtSettings(this IConfiguration configuration)
       => configuration.GetSection(JwtSettings.SECTION_KEY)
              .Get<JwtSettings>()!;

    public static RabbitMqSettings GetRabbitMqSettings(this IConfiguration configuration)
        => configuration.GetSection(RabbitMqSettings.SECTION_KEY)
                        .Get<RabbitMqSettings>()!;

    public static CacheSettings GetCacheSettings(this IConfiguration configuration)
        => configuration.GetSection(CacheSettings.SECTION_KEY)
                        .Get<CacheSettings>()!;
}
