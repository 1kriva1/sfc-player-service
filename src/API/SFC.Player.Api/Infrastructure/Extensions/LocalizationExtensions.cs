using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

using SFC.Player.Api.Infrastructure.Extensions;
using SFC.Player.Application;
using SFC.Player.Application.Common.Constants;
using SFC.Player.Infrastructure.Constants;

using Localization = SFC.Player.Application.Common.Constants.Localization;

namespace SFC.Player.Api.Infrastructure.Extensions;

public static class LocalizationExtensions
{
    public static void AddLocalization(this IServiceCollection services)
    {
        services.AddLocalization(options => options.ResourcesPath = CommonConstants.ResourcePath);

        services.Configure<RequestLocalizationOptions>(options => options.SetDefaultCulture(CommonConstants.SupportedCultures[0])
                .AddSupportedCultures(CommonConstants.SupportedCultures)
                .AddSupportedUICultures(CommonConstants.SupportedCultures));
    }

    public static void UseLocalization(this WebApplication app)
    {
        IOptions<RequestLocalizationOptions> localizationOptions =
            app.Services.GetService<IOptions<RequestLocalizationOptions>>()!;

        app.UseRequestLocalization(localizationOptions.Value);

        // inject localizer explicity for error messages
        IStringLocalizer<Resources> localizer =
            app.Services.GetService<IStringLocalizer<Resources>>()!;

        Localization.Configure(localizer);
    }
}
