using IdentityModel;
using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Common.Exceptions;
using SFC.Player.Infrastructure.Extensions;
using SFC.Player.Infrastructure.Settings;
using SFC.Player.Infrastructure.Settings.Grpc;

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
        if (_environment.IsDevelopment() && !_configuration.UseAuthentication())
        {
            GrpcEndpointAuthenticationDevelopment? devEndpointAuthentication = GetDevelopmentAuthentication(endpoint.Key);

            if (devEndpointAuthentication is not null)
            {
                return devEndpointAuthentication.AccessToken;
            }
        }

        if (endpoint.Authentication is null)
        {
            throw new ConfigurationException("Missing Authentication settings for Grpc endpoint.");
        }

        Guid? userId = _httpContextAccessor.GetUserId();

        return userId.HasValue
            ? await GetTokenExchangeTokenAsync(userId, endpoint, cancellationToken).ConfigureAwait(false)
            : await GetClientCredentialsTokenAsync(endpoint, cancellationToken).ConfigureAwait(false);
    }

    private async Task<string> GetTokenExchangeTokenAsync(Guid? userId, GrpcEndpoint endpoint, CancellationToken cancellationToken)
    {
        string cacheKey = $"{endpoint.Authentication!.ExchangeToken.ClientId}_{userId}";

        string? cached = await GetCachedTokenAsync(cacheKey, cancellationToken).ConfigureAwait(false);

        if (!string.IsNullOrWhiteSpace(cached))
        {
            return cached;
        }

        string incomingAccessToken = await _httpContextAccessor.GetAccessTokenAsync().ConfigureAwait(false)
            ?? throw new AuthorizationException(Localization.AuthorizationError);

        DiscoveryDocumentResponse discovery = await GetDiscoveryDocumentAsync(cancellationToken).ConfigureAwait(false);

        using TokenExchangeTokenRequest request = new()
        {
            Address = discovery.TokenEndpoint,
            GrantType = OidcConstants.GrantTypes.TokenExchange,
            ClientId = endpoint.Authentication.ExchangeToken.ClientId,
            ClientSecret = endpoint.Authentication.ExchangeToken.ClientSecret,
            SubjectToken = incomingAccessToken,
            SubjectTokenType = OidcConstants.TokenTypeIdentifiers.AccessToken,
            Scope = endpoint.Authentication.ExchangeToken.Scopes
        };

        using HttpClient client = _httpClientFactory.CreateClient();

        TokenResponse response = await client
            .RequestTokenExchangeTokenAsync(request, cancellationToken)
            .ConfigureAwait(false);

        return await HandleTokenResponseAsync(cacheKey, response, "Token exchange failed", cancellationToken).ConfigureAwait(false);
    }

    private async Task<string> GetClientCredentialsTokenAsync(GrpcEndpoint endpoint, CancellationToken cancellationToken)
    {
        string cacheKey = $"{endpoint.Authentication!.ClientCredentials.ClientId}_background";

        string? cached = await GetCachedTokenAsync(cacheKey, cancellationToken).ConfigureAwait(false);

        if (!string.IsNullOrWhiteSpace(cached))
        {
            return cached;
        }

        DiscoveryDocumentResponse discovery = await GetDiscoveryDocumentAsync(cancellationToken).ConfigureAwait(false);

        using ClientCredentialsTokenRequest request = new()
        {
            Address = discovery.TokenEndpoint,
            ClientId = endpoint.Authentication.ClientCredentials.ClientId,
            ClientSecret = endpoint.Authentication.ClientCredentials.ClientSecret,
            Scope = endpoint.Authentication.ClientCredentials.Scopes
        };

        using HttpClient client = _httpClientFactory.CreateClient();

        TokenResponse response = await client
            .RequestClientCredentialsTokenAsync(request, cancellationToken)
            .ConfigureAwait(false);

        return await HandleTokenResponseAsync(cacheKey, response, "Client credentials failed", cancellationToken).ConfigureAwait(false);
    }

    private async Task<DiscoveryDocumentResponse> GetDiscoveryDocumentAsync(CancellationToken cancellationToken)
    {
        IdentitySettings identitySettings = _configuration.GetIdentitySettings();

        using HttpClient client = _httpClientFactory.CreateClient();

        DiscoveryDocumentResponse discovery = await client
            .GetDiscoveryDocumentAsync(identitySettings.Authority, cancellationToken)
            .ConfigureAwait(false);

        return discovery.IsError
            ? throw new TokenExchangeException(discovery.Error ?? "Discovery document request failed.")
            : discovery;
    }

    private async Task<string?> GetCachedTokenAsync(string cacheKey, CancellationToken cancellationToken)
    {
        ClientAccessToken? token = await _clientAccessTokenCache.GetAsync(
            cacheKey,
            new ClientAccessTokenParameters(),
            cancellationToken).ConfigureAwait(false);

        return token?.AccessToken;
    }

    private async Task<string> HandleTokenResponseAsync(
        string cacheKey,
        TokenResponse response,
        string errorPrefix,
        CancellationToken cancellationToken)
    {
        if (response.IsError)
        {
            throw new TokenExchangeException($"{errorPrefix}: {response.ErrorDescription}");
        }

        if (string.IsNullOrWhiteSpace(response.AccessToken))
        {
            throw new TokenExchangeException($"{errorPrefix}: access token is null");
        }

        await _clientAccessTokenCache.SetAsync(
            cacheKey,
            response.AccessToken,
            response.ExpiresIn,
            new ClientAccessTokenParameters(),
            cancellationToken).ConfigureAwait(false);

        return response.AccessToken;
    }

    private GrpcEndpointAuthenticationDevelopment? GetDevelopmentAuthentication(string endpointKey)
    {
        GrpcSettingsDevelopment devGrpcSettings = _configuration.GetDevelopmentGrpcSettings();

        KeyValuePair<string, GrpcEndpointDevelopment> grpcEndpoint =
            devGrpcSettings.Endpoints.FirstOrDefault(e => e.Value.Key.Equals(endpointKey, StringComparison.OrdinalIgnoreCase));

        return grpcEndpoint.Value.Authentication;
    }
}