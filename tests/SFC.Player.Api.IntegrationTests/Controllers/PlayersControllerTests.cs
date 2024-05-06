using System.Net.Http.Json;
using System.Net;

using SFC.Player.Api.IntegrationTests.Fixtures;
using System.Text.Json;
using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Models.Base;
using SFC.Player.Application.Models.Players.Create;
using SFC.Player.Application.Models.Players.Common;
using SFC.Player.Application.Models.Players.Update;
using SFC.Player.Application.Models.Players.Get;
using SFC.Player.Application.Models.Players.Find;
using SFC.Player.Application.Models.Players.GetByUser;

namespace SFC.Player.Api.IntegrationTests.Controllers;
public class PlayersControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public PlayersControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _factory.InitializeDbForTests();
    }

    #region Create

    [Fact]
    [Trait("API", "Integration")]
    public async Task API_Integration_Create_ShouldReturnSuccess()
    {
        // Act
        HttpResponseMessage response = await CreateNewPlayer(Guid.NewGuid());

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        string responseString = await response.Content.ReadAsStringAsync();

        CreatePlayerResponse? responseValue = JsonSerializer.Deserialize<CreatePlayerResponse>(responseString);

        Assert.IsType<CreatePlayerResponse>(responseValue);
        Assert.True(responseValue.Success);
        Assert.Equal(Messages.SuccessResult, responseValue.Message);
        Assert.Null(responseValue.Errors);
        Assert.NotNull(responseValue.Player);
    }

    [Fact]
    [Trait("API", "Integration")]
    public async Task API_Integration_Create_ShouldReturnUnauthorized()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();

        CreatePlayerRequest request = new()
        {
            Player = new CreatePlayerModel
            {
                Profile = new PlayerProfileModel
                {
                    General = new PlayerGeneralProfileModel
                    {
                        FirstName = "Andrii",
                        LastName = "Kryvoruk",
                        City = "Kyiv"
                    },
                    Football = new PlayerFootballProfileModel
                    {
                        Position = 3
                    }
                },
                Stats = new PlayerStatsModel
                {
                    Points = new PlayerStatPointsModel { Available = 2, Used = 1 },
                    Values = Constants.VALID_STATS
                }
            }
        };

        JsonContent content = JsonContent.Create(request);

        // Act
        HttpResponseMessage response = await client.PostAsync("/api/players", content);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    [Trait("API", "Integration")]
    public async Task API_Integration_Create_ShouldReturnBadRequest()
    {
        // Arrange
        HttpClient client = _factory.CreateClient()
                                    .SetAuthenticationToken(Guid.NewGuid());

        CreatePlayerRequest request = new()
        {
            Player = new CreatePlayerModel
            {
                Profile = new PlayerProfileModel
                {
                    General = new PlayerGeneralProfileModel
                    {
                        LastName = "Kryvoruk",
                        City = "Kyiv"
                    },
                    Football = new PlayerFootballProfileModel
                    {
                        Position = 3
                    }
                },
                Stats = new PlayerStatsModel
                {
                    Points = new PlayerStatPointsModel { Available = 2, Used = 1 },
                    Values = Constants.VALID_STATS
                }
            }
        };

        JsonContent content = JsonContent.Create(request);

        // Act
        HttpResponseMessage response = await client.PostAsync("/api/players", content);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        string responseString = await response.Content.ReadAsStringAsync();

        BaseErrorResponse? responseValue = JsonSerializer.Deserialize<BaseErrorResponse>(responseString);

        Assert.IsType<BaseErrorResponse>(responseValue);
        Assert.False(responseValue.Success);
        Assert.Equal(Messages.ValidationError, responseValue.Message);
        Assert.NotNull(responseValue.Errors);
        Assert.True(responseValue.Errors.ContainsKey("Player.Profile.General.FirstName"));
        Assert.Single(responseValue.Errors["Player.Profile.General.FirstName"]);
        Assert.Equal("'FirstName' must not be empty.", responseValue.Errors["Player.Profile.General.FirstName"].FirstOrDefault());
    }

    [Fact]
    [Trait("API", "Integration")]
    public async Task API_Integration_Create_ShouldReturnLocalizedBadRequest()
    {
        // Arrange
        HttpClient client = _factory.CreateClient()
                                    .SetAuthenticationToken(Guid.NewGuid());

        CreatePlayerRequest request = new()
        {
            Player = new CreatePlayerModel
            {
                Profile = new PlayerProfileModel
                {
                    General = new PlayerGeneralProfileModel
                    {
                        LastName = "Kryvoruk",
                        City = "Kyiv"
                    },
                    Football = new PlayerFootballProfileModel
                    {
                        Position = 3
                    }
                },
                Stats = new PlayerStatsModel
                {
                    Points = new PlayerStatPointsModel { Available = 2, Used = 1 },
                    Values = Constants.VALID_STATS
                }
            }
        };

        JsonContent content = JsonContent.Create(request);

        client.DefaultRequestHeaders.Add("Accept-Language", CommonConstants.SUPPORTED_CULTURES[1]);

        // Act
        HttpResponseMessage response = await client.PostAsync("/api/players", content);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        string responseString = await response.Content.ReadAsStringAsync();
        BaseErrorResponse? responseValue = JsonSerializer.Deserialize<BaseErrorResponse>(responseString);

        Assert.Equal("Валідаційна помилка.", responseValue!.Message);
        Assert.Equal("'FirstName' не може бути порожнім.", responseValue!.Errors!["Player.Profile.General.FirstName"].FirstOrDefault());
    }

    #endregion Create

    #region Update

    [Fact]
    [Trait("API", "Integration")]
    public async Task API_Integration_Update_ShouldReturnSuccess()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        long newPlayerId = await GetNewPlayerId(userId);

        HttpClient client = _factory.CreateClient()
                                    .SetAuthenticationToken(userId);

        UpdatePlayerRequest request = new()
        {
            Player = new UpdatePlayerModel
            {
                Profile = new PlayerProfileModel
                {
                    General = new PlayerGeneralProfileModel
                    {
                        FirstName = "Ira",
                        LastName = "Kryvoruk",
                        City = "Kyiv"
                    },
                    Football = new PlayerFootballProfileModel
                    {
                        Position = 3
                    }
                },
                Stats = new PlayerStatsModel
                {
                    Points = new PlayerStatPointsModel { Available = 2, Used = 1 },
                    Values = Constants.VALID_STATS
                }
            }
        };

        JsonContent content = JsonContent.Create(request);

        // Act
        HttpResponseMessage response = await client.PutAsync($"/api/players/{newPlayerId}", content);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    [Trait("API", "Integration")]
    public async Task API_Integration_Update_ShouldReturnUnauthorized()
    {
        // Arrange
        long newPlayerId = await GetNewPlayerId(Guid.NewGuid());

        HttpClient client = _factory.CreateClient();

        UpdatePlayerRequest request = new()
        {
            Player = new UpdatePlayerModel
            {
                Profile = new PlayerProfileModel
                {
                    General = new PlayerGeneralProfileModel
                    {
                        FirstName = "Ira",
                        LastName = "Kryvoruk",
                        City = "Kyiv"
                    },
                    Football = new PlayerFootballProfileModel
                    {
                        Position = 3
                    }
                },
                Stats = new PlayerStatsModel
                {
                    Points = new PlayerStatPointsModel { Available = 2, Used = 1 },
                    Values = Constants.VALID_STATS
                }
            }
        };

        JsonContent content = JsonContent.Create(request);

        // Act
        HttpResponseMessage response = await client.PutAsync($"/api/players/{newPlayerId}", content);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    [Trait("API", "Integration")]
    public async Task API_Integration_Update_ShouldReturnBadRequest()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        long newPlayerId = await GetNewPlayerId(userId);

        HttpClient client = _factory.CreateClient()
                                    .SetAuthenticationToken(userId);

        UpdatePlayerRequest request = new()
        {
            Player = new UpdatePlayerModel
            {
                Profile = new PlayerProfileModel
                {
                    General = new PlayerGeneralProfileModel
                    {
                        LastName = "Kryvoruk",
                        City = "Kyiv"
                    },
                    Football = new PlayerFootballProfileModel
                    {
                        Position = 3
                    }
                },
                Stats = new PlayerStatsModel
                {
                    Points = new PlayerStatPointsModel { Available = 2, Used = 1 },
                    Values = Constants.VALID_STATS
                }
            }
        };

        JsonContent content = JsonContent.Create(request);

        // Act
        HttpResponseMessage response = await client.PutAsync($"/api/players/{newPlayerId}", content);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        string responseString = await response.Content.ReadAsStringAsync();

        BaseErrorResponse? responseValue = JsonSerializer.Deserialize<BaseErrorResponse>(responseString);

        Assert.IsType<BaseErrorResponse>(responseValue);
        Assert.False(responseValue.Success);
        Assert.Equal(Messages.ValidationError, responseValue.Message);
        Assert.NotNull(responseValue.Errors);
        Assert.True(responseValue.Errors.ContainsKey("Player.Profile.General.FirstName"));
        Assert.Single(responseValue.Errors["Player.Profile.General.FirstName"]);
        Assert.Equal("'FirstName' must not be empty.", responseValue.Errors["Player.Profile.General.FirstName"].FirstOrDefault());
    }

    [Fact]
    [Trait("API", "Integration")]
    public async Task API_Integration_Update_ShouldReturnLocalizedBadRequest()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        long newPlayerId = await GetNewPlayerId(userId);

        HttpClient client = _factory.CreateClient()
                                    .SetAuthenticationToken(userId);

        UpdatePlayerRequest request = new()
        {
            Player = new UpdatePlayerModel
            {
                Profile = new PlayerProfileModel
                {
                    General = new PlayerGeneralProfileModel
                    {
                        LastName = "Kryvoruk",
                        City = "Kyiv"
                    },
                    Football = new PlayerFootballProfileModel
                    {
                        Position = 3
                    }
                },
                Stats = new PlayerStatsModel
                {
                    Points = new PlayerStatPointsModel { Available = 2, Used = 1 },
                    Values = Constants.VALID_STATS
                }
            }
        };

        JsonContent content = JsonContent.Create(request);

        client.DefaultRequestHeaders.Add("Accept-Language", CommonConstants.SUPPORTED_CULTURES[1]);

        // Act
        HttpResponseMessage response = await client.PutAsync($"/api/players/{newPlayerId}", content);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        string responseString = await response.Content.ReadAsStringAsync();

        BaseErrorResponse? responseValue = JsonSerializer.Deserialize<BaseErrorResponse>(responseString);

        Assert.Equal("Валідаційна помилка.", responseValue!.Message);
        Assert.Equal("'FirstName' не може бути порожнім.", responseValue!.Errors!["Player.Profile.General.FirstName"].FirstOrDefault());
    }

    #endregion Update

    #region Get

    [Fact]
    [Trait("API", "Integration")]
    public async Task API_Integration_Get_ShouldReturnSuccess()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        long newPlayerId = await GetNewPlayerId(userId);

        HttpClient client = _factory.CreateClient()
                                    .SetAuthenticationToken(userId);

        // Act
        HttpResponseMessage response = await client.GetAsync($"/api/players/{newPlayerId}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        string responseString = await response.Content.ReadAsStringAsync();

        GetPlayerResponse? responseValue = JsonSerializer.Deserialize<GetPlayerResponse>(responseString);

        Assert.IsType<GetPlayerResponse>(responseValue);
        Assert.True(responseValue.Success);
        Assert.Equal(Messages.SuccessResult, responseValue.Message);
        Assert.Null(responseValue.Errors);
        Assert.NotNull(responseValue.Player);
    }

    [Fact]
    [Trait("API", "Integration")]
    public async Task API_Integration_Get_ShouldReturnUnauthorize()
    {
        // Arrange
        long newPlayerId = await GetNewPlayerId(Guid.NewGuid());

        HttpClient client = _factory.CreateClient();

        // Act
        HttpResponseMessage response = await client.GetAsync($"/api/players/{newPlayerId}");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    [Trait("API", "Integration")]
    public async Task API_Integration_Get_ShouldReturnNotFound()
    {
        // Arrange
        HttpClient client = _factory.CreateClient()
                                    .SetAuthenticationToken(Guid.NewGuid());

        // Act
        HttpResponseMessage response = await client.GetAsync($"/api/players/{1}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        string responseString = await response.Content.ReadAsStringAsync();

        BaseResponse? responseValue = JsonSerializer.Deserialize<BaseResponse>(responseString);

        Assert.IsType<BaseResponse>(responseValue);
        Assert.False(responseValue.Success);
        Assert.Equal(Messages.PlayerNotFound, responseValue.Message);
    }

    [Fact]
    [Trait("API", "Integration")]
    public async Task API_Integration_Get_ShouldReturnLocalizedNotFound()
    {
        // Arrange
        HttpClient client = _factory.CreateClient()
                                    .SetAuthenticationToken(Guid.NewGuid());
        client.DefaultRequestHeaders.Add("Accept-Language", CommonConstants.SUPPORTED_CULTURES[1]);

        // Act
        HttpResponseMessage response = await client.GetAsync($"/api/players/{1}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        string responseString = await response.Content.ReadAsStringAsync();

        BaseResponse? responseValue = JsonSerializer.Deserialize<BaseResponse>(responseString);

        Assert.Equal("Гравець не знайдений.", responseValue!.Message);
    }

    #endregion Get

    #region GetByUser

    [Fact]
    [Trait("API", "Integration")]
    public async Task API_Integration_GetByUser_ShouldReturnSuccess()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        await CreateNewPlayer(userId);

        HttpClient client = _factory.CreateClient()
                                    .SetAuthenticationToken(userId);

        // Act
        HttpResponseMessage response = await client.GetAsync("/api/players/byuser");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        string responseString = await response.Content.ReadAsStringAsync();

        GetPlayerByUserResponse? responseValue = JsonSerializer.Deserialize<GetPlayerByUserResponse>(responseString);

        Assert.IsType<GetPlayerByUserResponse>(responseValue);
        Assert.True(responseValue.Success);
        Assert.Equal(Messages.SuccessResult, responseValue.Message);
        Assert.Null(responseValue.Errors);
        Assert.NotNull(responseValue.Player);
    }

    [Fact]
    [Trait("API", "Integration")]
    public async Task API_Integration_GetByUser_ShouldReturnLocalizedSuccess()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        await CreateNewPlayer(userId);

        HttpClient client = _factory.CreateClient()
                                    .SetAuthenticationToken(userId);
        client.DefaultRequestHeaders.Add("Accept-Language", CommonConstants.SUPPORTED_CULTURES[1]);

        // Act
        HttpResponseMessage response = await client.GetAsync("/api/players/byuser");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        string responseString = await response.Content.ReadAsStringAsync();

        GetPlayerByUserResponse? responseValue = JsonSerializer.Deserialize<GetPlayerByUserResponse>(responseString);

        Assert.Equal(Messages.SuccessResult, responseValue!.Message);
    }

    [Fact]
    [Trait("API", "Integration")]
    public async Task API_Integration_GetByUser_ShouldReturnUnauthorize()
    {
        // Arrange
        await CreateNewPlayer(Guid.NewGuid());

        HttpClient client = _factory.CreateClient();

        // Act
        HttpResponseMessage response = await client.GetAsync("/api/players/byuser");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    [Trait("API", "Integration")]
    public async Task API_Integration_GetByUser_ShouldNotReturnPlayerWithSuccessResult()
    {
        // Arrange
        HttpClient client = _factory.CreateClient()
                                    .SetAuthenticationToken(Guid.NewGuid());

        // Act
        HttpResponseMessage response = await client.GetAsync("/api/players/byuser");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        string responseString = await response.Content.ReadAsStringAsync();

        GetPlayerByUserResponse? responseValue = JsonSerializer.Deserialize<GetPlayerByUserResponse>(responseString);

        Assert.IsType<GetPlayerByUserResponse>(responseValue);
        Assert.True(responseValue.Success);
        Assert.Equal(Messages.SuccessResult, responseValue.Message);
        Assert.Null(responseValue.Errors);
        Assert.Null(responseValue.Player);
    }

    #endregion GetByUser

    #region GetPlayers

    [Fact]
    [Trait("API", "Integration")]
    public async Task API_Integration_GetPlayers_ShouldReturnSuccess()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        await CreateNewPlayer(userId);

        HttpClient client = _factory.CreateClient()
                                    .SetAuthenticationToken(userId);

        // Act
        HttpResponseMessage response = await client
            .GetAsync("/api/players/find?Pagination.Page=1&Pagination.Size=10&Sorting[0].Name=Height&Sorting[0].Direction=Descending&Filter.Profile.General.Name=Andrii");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        string responseString = await response.Content.ReadAsStringAsync();

        GetPlayersResponse? responseValue = JsonSerializer.Deserialize<GetPlayersResponse>(responseString);

        Assert.IsType<GetPlayersResponse>(responseValue);
        Assert.True(responseValue.Success);
        Assert.Equal(Messages.SuccessResult, responseValue.Message);
        Assert.Null(responseValue.Errors);
        Assert.Single(responseValue.Items);
    }

    [Fact]
    [Trait("API", "Integration")]
    public async Task API_Integration_GetPlayers_ShouldReturnLocalizedSuccess()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        await CreateNewPlayer(userId);

        HttpClient client = _factory.CreateClient()
                                    .SetAuthenticationToken(userId);
        client.DefaultRequestHeaders.Add("Accept-Language", CommonConstants.SUPPORTED_CULTURES[1]);

        // Act
        HttpResponseMessage response = await client
            .GetAsync("/api/players/find?Pagination.Page=1&Pagination.Size=10&Sorting[0].Name=Height&Sorting[0].Direction=Descending&Filter.Profile.General.Name=Andrii");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        string responseString = await response.Content.ReadAsStringAsync();

        GetPlayersResponse? responseValue = JsonSerializer.Deserialize<GetPlayersResponse>(responseString);

        Assert.Equal(Messages.SuccessResult, responseValue!.Message);
    }

    [Fact]
    [Trait("API", "Integration")]
    public async Task API_Integration_GetPlayers_ShouldReturnUnauthorize()
    {
        // Arrange
        await CreateNewPlayer(Guid.NewGuid());

        HttpClient client = _factory.CreateClient();

        // Act
        HttpResponseMessage response = await client
            .GetAsync("/api/players/find?Pagination.Page=1&Pagination.Size=10&Sorting[0].Name=Height&Sorting[0].Direction=Descending&Filter.Profile.General.Name=Andrii");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    [Trait("API", "Integration")]
    public async Task API_Integration_GetPlayers_ShouldNotReturnPlayersWithSuccessResult()
    {
        // Arrange
        HttpClient client = _factory.CreateClient()
                                    .SetAuthenticationToken(Guid.NewGuid());

        // Act
        HttpResponseMessage response = await client
            .GetAsync("/api/players/find?Pagination.Page=1&Pagination.Size=10&Sorting[0].Name=Height&Sorting[0].Direction=Descending&Filter.Profile.General.Name=Kelly");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        string responseString = await response.Content.ReadAsStringAsync();

        GetPlayersResponse? responseValue = JsonSerializer.Deserialize<GetPlayersResponse>(responseString);

        Assert.IsType<GetPlayersResponse>(responseValue);
        Assert.True(responseValue.Success);
        Assert.Equal(Messages.SuccessResult, responseValue.Message);
        Assert.Null(responseValue.Errors);
        Assert.False(responseValue.Items.Any());
    }

    [Fact]
    [Trait("API", "Integration")]
    public async Task API_Integration_GetPlayers_ShouldReturnBadRequest()
    {
        // Arrange
        HttpClient client = _factory.CreateClient()
                                    .SetAuthenticationToken(Guid.NewGuid());

        // Act
        HttpResponseMessage response = await client
            .GetAsync("/api/players/find?Pagination.Page=1&Pagination.Size=10" +
            "&Sorting[0].Name=Height&Sorting[0].Direction=Descending" +
            "&Filter.Profile.General.Tags[0]=zik&Filter.Profile.General.Tags[1]=zik");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        string responseString = await response.Content.ReadAsStringAsync();

        BaseErrorResponse? responseValue = JsonSerializer.Deserialize<BaseErrorResponse>(responseString);

        Assert.IsType<BaseErrorResponse>(responseValue);
        Assert.False(responseValue.Success);
        Assert.Equal(Messages.ValidationError, responseValue.Message);
        Assert.NotNull(responseValue.Errors);
        Assert.True(responseValue.Errors.ContainsKey("Filter.Profile.General.Tags"));
        Assert.Single(responseValue.Errors["Filter.Profile.General.Tags"]);
        Assert.Equal("Each value from 'Tags' must be unique.", responseValue.Errors["Filter.Profile.General.Tags"].FirstOrDefault());
    }

    [Fact]
    [Trait("API", "Integration")]
    public async Task API_Integration_GetPlayers_ShouldReturnLocalizedBadRequest()
    {
        // Arrange
        HttpClient client = _factory.CreateClient()
                                    .SetAuthenticationToken(Guid.NewGuid());

        client.DefaultRequestHeaders.Add("Accept-Language", CommonConstants.SUPPORTED_CULTURES[1]);

        // Act
        HttpResponseMessage response = await client
            .GetAsync("/api/players/find?Pagination.Page=1&Pagination.Size=10" +
            "&Sorting[0].Name=Height&Sorting[0].Direction=Descending" +
            "&Filter.Profile.General.Tags[0]=zik&Filter.Profile.General.Tags[1]=zik");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        string responseString = await response.Content.ReadAsStringAsync();
        BaseErrorResponse? responseValue = JsonSerializer.Deserialize<BaseErrorResponse>(responseString);

        Assert.Equal("Валідаційна помилка.", responseValue!.Message);
        Assert.Equal("Кожне значення з 'Tags' має бути унікальним.", responseValue!.Errors!["Filter.Profile.General.Tags"].FirstOrDefault());
    }

    [Fact]
    [Trait("API", "Integration")]
    public async Task API_Integration_GetPlayers_ShouldReturnPaginationHeader()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        await CreateNewPlayer(userId);

        HttpClient client = _factory.CreateClient()
                                    .SetAuthenticationToken(userId);

        // Act
        HttpResponseMessage response = await client
            .GetAsync("/api/players/find?Pagination.Page=1&Pagination.Size=10&Sorting[0].Name=Height&Sorting[0].Direction=Descending&Filter.Profile.General.Name=Andrii");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        string responseString = await response.Content.ReadAsStringAsync();

        GetPlayersResponse? responseValue = JsonSerializer.Deserialize<GetPlayersResponse>(responseString);

        Assert.Single(response.Headers);
        Assert.True(response.Headers.Contains(CommonConstants.PAGINATION_HEADER_KEY));
        Assert.NotEmpty(response.Headers.FirstOrDefault(h => h.Key == CommonConstants.PAGINATION_HEADER_KEY).Value);
    }

    #endregion GetPlayers

    private async Task<HttpResponseMessage> CreateNewPlayer(Guid userId)
    {
        HttpClient client = _factory.CreateClient()
                                    .SetAuthenticationToken(userId);

        CreatePlayerRequest request = new()
        {
            Player = new CreatePlayerModel
            {
                Profile = new PlayerProfileModel
                {
                    General = new PlayerGeneralProfileModel
                    {
                        FirstName = "Andrii",
                        LastName = "Kryvoruk",
                        City = "Kyiv"
                    },
                    Football = new PlayerFootballProfileModel
                    {
                        Position = 3
                    }
                },
                Stats = new PlayerStatsModel
                {
                    Points = new PlayerStatPointsModel { Available = 2, Used = 1 },
                    Values = Constants.VALID_STATS
                }
            }
        };

        JsonContent content = JsonContent.Create(request);

        return await client.PostAsync("/api/players", content);
    }

    private async Task<long> GetNewPlayerId(Guid userId)
    {
        HttpResponseMessage createPlayerResponse = await CreateNewPlayer(userId);

        string createPlayerResponseString = await createPlayerResponse.Content.ReadAsStringAsync();

        CreatePlayerResponse? responseValue = JsonSerializer.Deserialize<CreatePlayerResponse>(createPlayerResponseString);

        return responseValue!.Player.Id;
    }
}
