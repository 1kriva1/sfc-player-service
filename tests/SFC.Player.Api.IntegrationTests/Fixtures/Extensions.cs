﻿using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

using Microsoft.EntityFrameworkCore;

using SFC.Player.Infrastructure.Persistence;

namespace SFC.Player.Api.IntegrationTests.Fixtures;
public static class Extensions
{
    public static HttpClient SetAuthenticationToken(this HttpClient client, bool forbidden = false,
        string? accessToken = null)
    {
        string token = accessToken ?? (forbidden 
            ? Constants.PLAYER_ACCESS_TOKEN_FORBIDDEN 
            : Constants.PLAYER_ACCESS_TOKEN_VALID_0);
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        return client;
    }

    public static void RefreshData(this PlayerDbContext context)
    {
        context.Database.EnsureCreated();

        context.Players.ExecuteDelete();

        context.Users.ExecuteDelete();

        context.IdentityUsers.ExecuteDelete();

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
