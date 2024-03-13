using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using SFC.Player.Infrastructure.Persistence;

namespace SFC.Player.Api.IntegrationTests.Fixtures;
public static class Extensions
{
    public static HttpClient SetAuthenticationToken(this HttpClient client, Guid userId)
    {
        JwtSecurityToken jwtToken = new(
           Constants.JwtSettings.Issuer,
           Constants.JwtSettings.Audience,
           new List<Claim> { new(ClaimTypes.NameIdentifier, userId.ToString()) },
           expires: DateTime.Now.AddMinutes(1),
           signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.JwtSettings.Key)), SecurityAlgorithms.HmacSha256)
       );

        string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return client;
    }

    public static void RefreshData(this PlayerDbContext context)
    {
        context.Database.EnsureCreated();

        context.Players.ExecuteDelete();

        context.FootballPositions.ExecuteDelete();

        context.WorkingFoots.ExecuteDelete();

        context.GameStyles.ExecuteDelete();

        context.StatCategories.ExecuteDelete();

        context.StatSkills.ExecuteDelete();

        context.StatTypes.ExecuteDelete();

        context.FootballPositions.AddRange(Constants.FOOTBALL_POSITIONS);

        context.WorkingFoots.AddRange(Constants.WORKING_FOOTS);

        context.GameStyles.AddRange(Constants.GAME_STYLES);

        context.StatCategories.AddRange(Constants.STAT_CATEGORIES);

        context.StatSkills.AddRange(Constants.STAT_SKILLS);

        context.StatTypes.AddRange(Constants.STAT_TYPES);

        context.SaveChanges();        
    }
}
