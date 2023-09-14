using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

using Microsoft.IdentityModel.Tokens;

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
}
