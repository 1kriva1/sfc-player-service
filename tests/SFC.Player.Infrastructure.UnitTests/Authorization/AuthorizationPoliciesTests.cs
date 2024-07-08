using SFC.Player.Application.Common.Constants;
using SFC.Player.Infrastructure.Authorization;

namespace SFC.Player.Infrastructure.UnitTests.Authorization;
public class AuthorizationPoliciesTests
{
    #region General

    [Fact]
    [Trait("Authorization", "Policies")]
    public void Authorization_Policies_General_ShouldBuildPolicy()
    {
        // Arrange
        IDictionary<string, IEnumerable<string>> claims = new Dictionary<string, IEnumerable<string>>();

        // Act
        PolicyModel general = AuthorizationPolicies.General(claims);

        // Assert
        Assert.Equal(Policy.GENERAL, general.Name);
    }

    [Fact]
    [Trait("Authorization", "Policies")]
    public void Authorization_Policies_General_ShouldPolicyHasDefinedRequirements()
    {
        // Arrange
        string claimType = "scope", claimValue = "test.full";
        IDictionary<string, IEnumerable<string>> claims = new Dictionary<string, IEnumerable<string>>
        {
            { claimType, [claimValue]}
        };

        // Act
        PolicyModel general = AuthorizationPolicies.General(claims);

        // Assert
        Assert.Equal(2, general.Policy.Requirements.Count);
        Assert.Equal("DenyAnonymousAuthorizationRequirement: Requires an authenticated user.",
            general.Policy.Requirements[0].ToString());
        Assert.Equal($"ClaimsAuthorizationRequirement:Claim.Type={claimType} and Claim.Value is one of the following values: ({claimValue})",
            general.Policy.Requirements[1].ToString());
    }

    #endregion General

    #region Own player

    [Fact]
    [Trait("Authorization", "Policies")]
    public void Authorization_Policies_OwnPlater_ShouldBuildPolicy()
    {
        // Arrange
        IDictionary<string, IEnumerable<string>> claims = new Dictionary<string, IEnumerable<string>>();

        // Act
        PolicyModel ownPlayer = AuthorizationPolicies.OwnPlayer(claims);

        // Assert
        Assert.Equal(Policy.OWN_PLAYER, ownPlayer.Name);
    }

    [Fact]
    [Trait("Authorization", "Policies")]
    public void Authorization_Policies_OwnPlayer_ShouldPolicyHasDefinedRequirements()
    {
        // Arrange
        string claimType = "scope", claimValue = "test.full";
        IDictionary<string, IEnumerable<string>> claims = new Dictionary<string, IEnumerable<string>>
        {
            { claimType, [claimValue]}
        };

        // Act
        PolicyModel ownPlayer = AuthorizationPolicies.OwnPlayer(claims);

        // Assert
        Assert.Equal(3, ownPlayer.Policy.Requirements.Count);
        Assert.Equal("DenyAnonymousAuthorizationRequirement: Requires an authenticated user.",
            ownPlayer.Policy.Requirements[0].ToString());
        Assert.Equal($"ClaimsAuthorizationRequirement:Claim.Type={claimType} and Claim.Value is one of the following values: ({claimValue})",
            ownPlayer.Policy.Requirements[1].ToString());
        Assert.Equal("OwnPlayerRequirement: Requires user has be owner of resource.",
            ownPlayer.Policy.Requirements[2].ToString());        
    }

    #endregion Own player
}
