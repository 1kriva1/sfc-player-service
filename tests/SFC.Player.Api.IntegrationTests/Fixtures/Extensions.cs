//using System.Net.Http.Headers;
//using System.Runtime.CompilerServices;

//using Microsoft.EntityFrameworkCore;

//using SFC.Player.Domain.Entities.Data;
//using SFC.Player.Infrastructure.Persistence;
//using SFC.Player.Infrastructure.Persistence.Contexts;

//namespace SFC.Player.Api.IntegrationTests.Fixtures;
//public static class Extensions
//{
//    public static HttpClient SetAuthenticationToken(this HttpClient client, bool forbidden = false,
//        string? accessToken = null)
//    {
//        string token = accessToken ?? (forbidden 
//            ? Constants.PLAYER_ACCESS_TOKEN_FORBIDDEN 
//            : Constants.PLAYER_ACCESS_TOKEN_VALID_0);
//        client.DefaultRequestHeaders.Authorization =
//            new AuthenticationHeaderValue("Bearer", token);

//        return client;
//    }

//    public static void RefreshData(this PlayerDbContext context, IdentityDbContext identityContext, DataDbContext dataContext)
//    {
//        context.Database.EnsureCreated();

//        context.Players.ExecuteDelete();

//        identityContext.Users.ExecuteDelete();

//        dataContext.FootballPositions.ExecuteDelete();

//        dataContext.WorkingFoots.ExecuteDelete();

//        dataContext.GameStyles.ExecuteDelete();

//        dataContext.StatCategories.ExecuteDelete();

//        dataContext.StatSkills.ExecuteDelete();

//        dataContext.StatTypes.ExecuteDelete();

//        dataContext.Set<FootballPosition>().AddRange(Constants.FOOTBALL_POSITIONS);

//        dataContext.Set<WorkingFoot>().AddRange(Constants.WORKING_FOOTS);

//        dataContext.Set<GameStyle>().AddRange(Constants.GAME_STYLES);

//        dataContext.Set<StatCategory>().AddRange(Constants.STAT_CATEGORIES);

//        dataContext.Set<StatSkill>().AddRange(Constants.STAT_SKILLS);

//        dataContext.Set<StatType>().AddRange(Constants.STAT_TYPES);

//        context.SaveChanges();
//    }
//}
