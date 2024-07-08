using Microsoft.AspNetCore.Authentication.JwtBearer;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Infrastructure.Extensions;
using SFC.Player.Infrastructure.Settings;

namespace SFC.Player.Api.Extensions;

public static class AuthenticationExtensions
{
    public static void AddAuthentication(this WebApplicationBuilder builder)
    {
        IdentitySettings identitySettings = builder.Configuration.GetIdentitySettings();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
             {
                 if (!builder.Environment.IsDevelopment() || builder.Configuration.UseAuthentication())
                 {
                     options.Authority = identitySettings.Authority;
                     options.Audience = identitySettings.Audience;
                     options.TokenValidationParameters = new()
                     {
                         ValidateAudience = true,
                         NameClaimType = "name",
                         ValidTypes = [AuthenticationConstants.VALID_JWT_HEADER_TYP]
                     };
                 }
             }
         );

        builder.Services.AddAuthorization(options =>
        {
            options.AddGeneralPolicy(identitySettings.RequireClaims);
            options.AddOwnPlayerPolicy(identitySettings.RequireClaims);
        });
    }
}
