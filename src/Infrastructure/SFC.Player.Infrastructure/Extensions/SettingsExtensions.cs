using Microsoft.Extensions.Configuration;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Common.Settings;
using SFC.Player.Infrastructure.Settings;

namespace SFC.Player.Infrastructure.Extensions;
public static class SettingsExtensions
{
    public static bool UseAuthentication(this ConfigurationManager configuration)
        => configuration.GetValue<bool>(SettingConstants.AUTHENTICATION);

    public static IdentitySettings GetIdentitySettings(this IConfiguration configuration)
        => configuration.GetSection(IdentitySettings.SECTION_KEY)
                        .Get<IdentitySettings>()!;

    public static RabbitMqSettings GetRabbitMqSettings(this IConfiguration configuration)
        => configuration.GetSection(RabbitMqSettings.SECTION_KEY)
                        .Get<RabbitMqSettings>()!;

    public static CacheSettings GetCacheSettings(this IConfiguration configuration)
        => configuration.GetSection(CacheSettings.SECTION_KEY)
                        .Get<CacheSettings>()!;
}
