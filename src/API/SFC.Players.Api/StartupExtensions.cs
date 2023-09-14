using Microsoft.AspNetCore.Mvc;
using SFC.Players.Api.Filters;
using SFC.Players.Api.Middlewares;
using SFC.Players.Application;
using SFC.Players.Infrastructure.Persistence;
using SFC.Players.Application.Common.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SFC.Players.Application.Interfaces.Identity;
using SFC.Players.Api.Services;
using SFC.Players.Infrastructure;
using SFC.Players.Application.Models.Configurations;

namespace SFC.Players.Api;

public static class StartupExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddApplicationServices();

        builder.Services.AddPersistenceServices(builder.Configuration);

        builder.Services.AddInfrastructureServices();

        builder.AddApiServices();

        builder.Services.AddHttpContextAccessor();

        builder.Services.Configure<MvcOptions>(options => options.AllowEmptyInputInBodyModelBinding = true);

        builder.Services.AddCors();

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

        builder.AddLocalization();

        builder.AddAuthentication();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        // global cors policy
        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true) // allow any origin
            .AllowCredentials()); // allow credentials

        app.UseHttpsRedirection();

        app.UseLocalization();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseCustomExceptionHandler();

        app.MapControllers();

        return app;
    }

    private static void AddApiServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserService, UserService>();
    }

    private static void AddAuthentication(this WebApplicationBuilder builder)
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

    private static void AddLocalization(this WebApplicationBuilder builder)
    {
        builder.Services.AddLocalization(options => options.ResourcesPath = CommonConstants.RESOURCE_PATH);

        builder.Services.Configure<RequestLocalizationOptions>(options => options.SetDefaultCulture(CommonConstants.SUPPORTED_CULTURES[0])
                .AddSupportedCultures(CommonConstants.SUPPORTED_CULTURES)
                .AddSupportedUICultures(CommonConstants.SUPPORTED_CULTURES));
    }
}
