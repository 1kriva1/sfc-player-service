//using Microsoft.AspNetCore.Authorization;

//using SFC.Player.Application.Common.Constants;
//using SFC.Player.Infrastructure.Constants;
//using SFC.Player.Infrastructure.Extensions;

//namespace SFC.Player.Infrastructure.UnitTests.Extensions;
//public class AuthorizationExtensionsTests
//{
//    #region General

//    [Fact]
//    [Trait("Extension", "Authorization")]
//    public void Extension_Authorization_ShouldAddGeneralPolicy()
//    {
//        // Arrange
//        AuthorizationOptions options = new();
//        IDictionary<string, IEnumerable<string>> claims = new Dictionary<string, IEnumerable<string>>();

//        // Act
//        options.AddGeneralPolicy(claims);

//        // Assert
//        AuthorizationPolicy? generalPolicy = options.GetPolicy(Policy.GENERAL);

//        Assert.NotNull(generalPolicy);
//    }

//    [Fact]
//    [Trait("Extension", "Authorization")]
//    public void Extension_Authorization_ShouldGeneralPolicyHasDefinedRequirements()
//    {
//        // Arrange
//        AuthorizationOptions options = new();
//        string claimType = "scope", claimValue = "test.full";
//        IDictionary<string, IEnumerable<string>> claims = new Dictionary<string, IEnumerable<string>>
//        {
//            { claimType, [claimValue]}
//        };

//        // Act
//        options.AddGeneralPolicy(claims);

//        // Assert
//        AuthorizationPolicy? generalPolicy = options.GetPolicy(Policy.GENERAL);

//        Assert.NotNull(generalPolicy);
//        Assert.Equal(2, generalPolicy.Requirements.Count);
//        Assert.Equal("DenyAnonymousAuthorizationRequirement: Requires an authenticated user.",
//            generalPolicy.Requirements[0].ToString());
//        Assert.Equal($"ClaimsAuthorizationRequirement:Claim.Type={claimType} and Claim.Value is one of the following values: ({claimValue})",
//            generalPolicy.Requirements[1].ToString());
//    }

//    #endregion General

//    #region Own player

//    [Fact]
//    [Trait("Extension", "Authorization")]
//    public void Extension_Authorization_ShouldAddOwnPlayerPolicy()
//    {
//        // Arrange
//        AuthorizationOptions options = new();
//        IDictionary<string, IEnumerable<string>> claims = new Dictionary<string, IEnumerable<string>>();

//        // Act
//        options.AddOwnPlayerPolicy(claims);

//        // Assert
//        AuthorizationPolicy? ownPlayerPolicy = options.GetPolicy(Policy.OWN_PLAYER);

//        Assert.NotNull(ownPlayerPolicy);
//    }

//    [Fact]
//    [Trait("Extension", "Authorization")]
//    public void Extension_Authorization_ShouldOwnPlayerPolicyHasDefinedRequirements()
//    {
//        // Arrange
//        AuthorizationOptions options = new();
//        string claimType = "scope", claimValue = "test.full";
//        IDictionary<string, IEnumerable<string>> claims = new Dictionary<string, IEnumerable<string>>
//        {
//            { claimType, [claimValue]}
//        };

//        // Act
//        options.AddOwnPlayerPolicy(claims);

//        // Assert
//        AuthorizationPolicy? ownPlayerPolicy = options.GetPolicy(Policy.OWN_PLAYER);

//        Assert.NotNull(ownPlayerPolicy);
//        Assert.Equal(3, ownPlayerPolicy.Requirements.Count);
//        Assert.Equal("DenyAnonymousAuthorizationRequirement: Requires an authenticated user.",
//            ownPlayerPolicy.Requirements[0].ToString());
//        Assert.Equal($"ClaimsAuthorizationRequirement:Claim.Type={claimType} and Claim.Value is one of the following values: ({claimValue})",
//            ownPlayerPolicy.Requirements[1].ToString());
//        Assert.Equal("OwnPlayerRequirement: Requires user has be owner of resource.",
//           ownPlayerPolicy.Requirements[2].ToString());
//    }

//    #endregion Own player
//}
