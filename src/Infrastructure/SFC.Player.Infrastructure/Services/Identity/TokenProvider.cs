using IdentityModel.Client;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using IdentityModel.AspNetCore.AccessTokenManagement;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Common.Exceptions;
using SFC.Player.Infrastructure.Extensions;
using SFC.Player.Infrastructure.Settings.Grpc;
using SFC.Player.Infrastructure.Settings;

namespace SFC.Player.Infrastructure.Services.Identity;
public interface ITokenProvider
{
    Task<string> GetTokenAsync(GrpcEndpoint endpoint, CancellationToken cancellationToken);
}

public class TokenProvider(
    IHttpClientFactory httpClientFactory,
    IConfiguration configuration,
    IClientAccessTokenCache clientAccessTokenCache,
    IHttpContextAccessor httpContextAccessor,
    IWebHostEnvironment environment) : ITokenProvider
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly IConfiguration _configuration = configuration;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IWebHostEnvironment _environment = environment;
    private readonly IClientAccessTokenCache _clientAccessTokenCache = clientAccessTokenCache;

    public async Task<string> GetTokenAsync(GrpcEndpoint endpoint, CancellationToken cancellationToken)
    {
        if (_environment.IsDevelopment())
        {
            GrpcEndpointAuthenticationDevelopment? devEndpointAuthentication = GetDevelopmentAuthentication(endpoint.Key);

            if (devEndpointAuthentication is not null)
            {
                return devEndpointAuthentication.AccessToken;
            }
        }

        Guid userId = _httpContextAccessor.GetUserId() ?? throw new AuthorizationException(Localization.AuthorizationError);

        if (endpoint.Authentication is null)
        {
            throw new ConfigurationException($"Missing Authentication settings for Grpc endpoint.");
        }

        string accessTokenCacheKey = $"{endpoint.Authentication.ClientId}_{userId}";

        ClientAccessToken? clientAccessToken = await _clientAccessTokenCache.GetAsync(
            accessTokenCacheKey,
            new ClientAccessTokenParameters(),
            cancellationToken).ConfigureAwait(true);

        if (!string.IsNullOrWhiteSpace(clientAccessToken?.AccessToken))
        {
            return clientAccessToken.AccessToken;
        }

        IdentitySettings identitySettings = _configuration.GetIdentitySettings();

        using HttpClient client = _httpClientFactory.CreateClient();

        DiscoveryDocumentResponse discoveryDocument = await client.GetDiscoveryDocumentAsync(identitySettings.Authority, cancellationToken)
                                                                  .ConfigureAwait(true);

        if (discoveryDocument.IsError)
        {
            throw new TokenExchangeException($"Token exchanged failed: {discoveryDocument.Error}");
        }

        string incomingAccessToken = await _httpContextAccessor.GetAccessTokenAsync().ConfigureAwait(true)
            ?? throw new AuthorizationException(Localization.AuthorizationError);

        using TokenExchangeTokenRequest request = new()
        {
            Address = discoveryDocument.TokenEndpoint,
            GrantType = OidcConstants.GrantTypes.TokenExchange,
            ClientId = endpoint.Authentication.ClientId,
            ClientSecret = endpoint.Authentication.ClientSecret,
            SubjectToken = incomingAccessToken,
            SubjectTokenType = OidcConstants.TokenTypeIdentifiers.AccessToken,
            Scope = endpoint.Authentication.Scopes
        };

        TokenResponse exchangeResponse = await client.RequestTokenExchangeTokenAsync(request, cancellationToken)
                                                     .ConfigureAwait(true);

        if (exchangeResponse.IsError)
        {
            throw new TokenExchangeException($"Token exchanged failed: {exchangeResponse.ErrorDescription}");
        }

        if (exchangeResponse.AccessToken is null)
        {
            throw new TokenExchangeException("Token exchanged failed. Access token is null");
        }

        await _clientAccessTokenCache.SetAsync(
            accessTokenCacheKey,
            exchangeResponse.AccessToken,
            exchangeResponse.ExpiresIn,
            new ClientAccessTokenParameters(),
            cancellationToken).ConfigureAwait(false);

        return exchangeResponse.AccessToken;
    }

    private GrpcEndpointAuthenticationDevelopment? GetDevelopmentAuthentication(string endpointKey)
    {
        GrpcSettingsDevelopment devGrpcSettings = _configuration.GetDevelopmentGrpcSettings();

        KeyValuePair<string, GrpcEndpointDevelopment> grpcEndpoint =
            devGrpcSettings.Endpoints.FirstOrDefault(e => e.Value.Key.Equals(endpointKey, StringComparison.OrdinalIgnoreCase));

        return grpcEndpoint.Value.Authentication;
    }
}