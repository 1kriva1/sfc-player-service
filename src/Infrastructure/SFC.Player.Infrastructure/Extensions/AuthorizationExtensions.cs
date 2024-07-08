using Microsoft.AspNetCore.Authorization;

using SFC.Player.Infrastructure.Authorization;

namespace SFC.Player.Infrastructure.Extensions;
public static class AuthorizationExtensions
{
    public static void AddGeneralPolicy(this AuthorizationOptions options, IDictionary<string, IEnumerable<string>> claims)
    {
        PolicyModel general = AuthorizationPolicies.General(claims);
        options.AddPolicy(general.Name, general.Policy);
    }

    public static void AddOwnPlayerPolicy(this AuthorizationOptions options, IDictionary<string, IEnumerable<string>> claims)
    {
        PolicyModel ownPlayer = AuthorizationPolicies.OwnPlayer(claims);
        options.AddPolicy(ownPlayer.Name, ownPlayer.Policy);
    }
}
