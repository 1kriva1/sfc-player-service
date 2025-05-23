using Microsoft.AspNetCore.Authorization;

using SFC.Player.Infrastructure.Authorization.OwnPlayer;
using SFC.Player.Infrastructure.Constants;

namespace SFC.Player.Infrastructure.Authorization;
public static class AuthorizationPolicies
{
    public static PolicyModel General(IDictionary<string, IEnumerable<string>> claims)
    {
        AuthorizationPolicyBuilder builder = GetGeneralPolicyBuilder(claims);
        return BuildPolicyModel(Policy.General, builder);
    }

    public static PolicyModel OwnPlayer(IDictionary<string, IEnumerable<string>> claims)
    {
        AuthorizationPolicyBuilder builder = GetGeneralPolicyBuilder(claims)
            .AddRequirements(new OwnPlayerRequirement());

        return BuildPolicyModel(Policy.OwnPlayer, builder);
    }

    #region Private

    private static AuthorizationPolicyBuilder GetGeneralPolicyBuilder(IDictionary<string, IEnumerable<string>> claims)
    {
        AuthorizationPolicyBuilder builder = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser();

        foreach (KeyValuePair<string, IEnumerable<string>> claim in claims)
        {
            builder.RequireClaim(claim.Key, claim.Value);
        }

        return builder;
    }

    private static PolicyModel BuildPolicyModel(string name, AuthorizationPolicyBuilder builder)
    {
        return new PolicyModel { Name = name, Policy = builder.Build() };
    }

    #endregion Private
}
