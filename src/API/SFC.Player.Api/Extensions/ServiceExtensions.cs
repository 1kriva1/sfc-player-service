using Microsoft.AspNetCore.Authentication.JwtBearer;
using SFC.Player.Api.Filters;
using SFC.Player.Api.Services;
using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Interfaces.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SFC.Player.Api.Extensions;
using SFC.Player.Application;
using SFC.Player.Infrastructure.Settings;

namespace SFC.Player.Api.Extensions;

public static class ServiceExtensions
{
    public static void AddControllers(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers(options =>
        {
            options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            options.Filters.Add(new ValidationFilterAttribute());
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            options.JsonSerializerOptions.DictionaryKeyPolicy = null;
            options.JsonSerializerOptions.WriteIndented = true;
        })
        .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true)
        .AddDataAnnotationsLocalization(options => options.DataAnnotationLocalizerProvider = (type, factory) =>
                factory.Create(typeof(Resources)));
    }

    public static void AddApiServices(this WebApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddScoped<IUserService, UserServiceDevelopment>();
        }
        else
        {
            builder.Services.AddScoped<IUserService, UserService>();
        }
    }

    public static void AddAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;

            JwtSettings jwtSettings = builder.Configuration
                .GetSection(JwtSettings.SECTION_KEY)
                .Get<JwtSettings>()!;

            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                ValidAudience = jwtSettings.Audience,
                ValidIssuer = jwtSettings.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
            };
        });
    }

    public static void AddLocalization(this WebApplicationBuilder builder)
    {
        builder.Services.AddLocalization(options => options.ResourcesPath = CommonConstants.RESOURCE_PATH);

        builder.Services.Configure<RequestLocalizationOptions>(options => options.SetDefaultCulture(CommonConstants.SUPPORTED_CULTURES[0])
                .AddSupportedCultures(CommonConstants.SUPPORTED_CULTURES)
                .AddSupportedUICultures(CommonConstants.SUPPORTED_CULTURES));
    }
}
