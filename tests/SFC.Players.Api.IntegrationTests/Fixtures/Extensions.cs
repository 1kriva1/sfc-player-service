using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using SFC.Players.Infrastructure.Persistence;

namespace SFC.Players.Api.IntegrationTests.Fixtures;
public static class Extensions
{
    public static HttpClient SetAuthenticationToken(this HttpClient client)
    {
        JwtSecurityToken test = new(
           Constants.JWT_SETTINGS.Issuer,
           Constants.JWT_SETTINGS.Audience,
           new List<Claim> { new(ClaimTypes.NameIdentifier, Constants.USER_ID.ToString()) },
           expires: DateTime.Now.AddMinutes(1),
           signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.JWT_SETTINGS.Key)), SecurityAlgorithms.HmacSha256)
       );

        string token = new JwtSecurityTokenHandler().WriteToken(test);

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return client;
    }

    public static void RefreshData(this PlayersDbContext context)
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
