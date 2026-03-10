using IdentityModel;
using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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
    ILogger<TokenProvider> logger,
    IHttpClientFactory httpClientFactory,
    IConfiguration configuration,
    IClientAccessTokenCache clientAccessTokenCache,
    IHttpContextAccessor httpContextAccessor,
    IWebHostEnvironment environment) : ITokenProvider
{
    private readonly ILogger<TokenProvider> _logger = logger;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly IConfiguration _configuration = configuration;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IWebHostEnvironment _environment = environment;
    private readonly IClientAccessTokenCache _clientAccessTokenCache = clientAccessTokenCache;

    public async Task<string> GetTokenAsync(GrpcEndpoint endpoint, CancellationToken cancellationToken)
    {
        try
        {
#pragma warning disable CA1848 // Use the LoggerMessage delegates
            _logger.LogInformation("Starting token acquisition for gRPC endpoint.");

            if (!_environment.UseAuthentication(_configuration))
            {
                _logger.LogInformation("Authentication disabled by environment. Attempting development token for endpoint '{EndpointKey}'.", endpoint.Key);

                GrpcEndpointAuthenticationDevelopment? devEndpointAuthentication = GetDevelopmentAuthentication(endpoint.Key);

                if (devEndpointAuthentication is not null)
                {
                    _logger.LogInformation("Development token provided for endpoint '{EndpointKey}'.", endpoint.Key);
                    return devEndpointAuthentication.AccessToken;
                }

                _logger.LogWarning("Development auth configured but no valid token found for endpoint '{EndpointKey}'. Falling back to normal flow.", endpoint.Key);
            }

            Guid userId = _httpContextAccessor.GetUserId() ?? throw new AuthorizationException(Localization.AuthorizationError);

            _logger.LogDebug("Resolved user id for token exchange. UserId: {UserId}", userId);

            if (endpoint.Authentication is null)
            {
                _logger.LogError("Missing Authentication settings for gRPC endpoint '{EndpointKey}'.", endpoint.Key);
                throw new ConfigurationException($"Missing Authentication settings for Grpc endpoint.");
            }

            string accessTokenCacheKey = $"{endpoint.Authentication.ClientId}_{userId}";

            _logger.LogDebug("Attempting access token cache lookup. CacheKey: {CacheKey}", accessTokenCacheKey);

            ClientAccessToken? clientAccessToken = await _clientAccessTokenCache.GetAsync(
                accessTokenCacheKey,
                new ClientAccessTokenParameters(),
                cancellationToken).ConfigureAwait(true);

            if (!string.IsNullOrWhiteSpace(clientAccessToken?.AccessToken))
            {
                _logger.LogInformation("Access token cache hit for client '{ClientId}'. ExpiresAt: {ExpiresAtUtc}",
                                endpoint.Authentication.ClientId,
                                clientAccessToken.Expiration);

                _logger.LogDebug("Access token cache lookup:{AccessToken}", clientAccessToken.AccessToken);

                return clientAccessToken.AccessToken;
            }


            _logger.LogInformation("Access token cache miss for client '{ClientId}'. Proceeding with discovery and token exchange.",
                        endpoint.Authentication.ClientId);

            IdentitySettings identitySettings = _configuration.GetIdentitySettings();

            using HttpClient client = _httpClientFactory.CreateClient();

            _logger.LogDebug("Requesting discovery document from authority: {Authority}", identitySettings.Authority);

            DiscoveryDocumentResponse discoveryDocument = await client.GetDiscoveryDocumentAsync(identitySettings.Authority, cancellationToken)
                                                                      .ConfigureAwait(true);

            if (discoveryDocument.IsError)
            {
                _logger.LogError("Discovery document request failed. Error: {Error}",
                                discoveryDocument.Error);

                throw new TokenExchangeException($"Token exchanged failed: {discoveryDocument.Error}");
            }


            _logger.LogInformation("Discovery document retrieved. TokenEndpoint: {TokenEndpoint}",
                        discoveryDocument.TokenEndpoint);

            string incomingAccessToken = await _httpContextAccessor.GetAccessTokenAsync().ConfigureAwait(true)
                ?? throw new AuthorizationException(Localization.AuthorizationError);

            _logger.LogDebug("Incoming Access token:{AccessToken}", incomingAccessToken);

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

            _logger.LogDebug("Initiating token exchange. ClientId: {ClientId}; Scopes: {ScopesCount}",
                        endpoint.Authentication.ClientId,
                        endpoint.Authentication.Scopes?.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Length);

            TokenResponse exchangeResponse = await client.RequestTokenExchangeTokenAsync(request, cancellationToken)
                                                         .ConfigureAwait(true);

            if (exchangeResponse.IsError)
            {
                _logger.LogError("Token exchange failed. Error: {Error}; ErrorDescription: {ErrorDescription}",
                                exchangeResponse.Error,
                                exchangeResponse.ErrorDescription);

                throw new TokenExchangeException($"Token exchanged failed: {exchangeResponse.ErrorDescription}");
            }

            if (exchangeResponse.AccessToken is null)
            {
                _logger.LogError("Token exchange succeeded but access token was null.");

                throw new TokenExchangeException("Token exchanged failed. Access token is null");
            }

            _logger.LogInformation("Token exchange succeeded. ExpiresIn (s): {ExpiresIn}",
                        exchangeResponse.ExpiresIn);

            _logger.LogDebug("Exchange Access token:{AccessToken}", exchangeResponse.AccessToken);

            await _clientAccessTokenCache.SetAsync(
                accessTokenCacheKey,
                exchangeResponse.AccessToken,
                exchangeResponse.ExpiresIn,
                new ClientAccessTokenParameters(),
                cancellationToken).ConfigureAwait(false);


            _logger.LogInformation("Access token cached for ClientId: {ClientId} with TTL(s): {TtlSeconds}",
                        endpoint.Authentication.ClientId,
                        exchangeResponse.ExpiresIn);

            _logger.LogInformation("Token acquisition completed successfully.");

            return exchangeResponse.AccessToken;
        }
        catch (AuthorizationException ex)
        {
            _logger.LogWarning(ex, "Authorization error during token acquisition.");
            throw;
        }
        catch (ConfigurationException ex)
        {
            _logger.LogError(ex, "Configuration error during token acquisition.");
            throw;
        }
        catch (TokenExchangeException ex)
        {
            _logger.LogError(ex, "Token exchange error during token acquisition.");
            throw;
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            _logger.LogWarning("Token acquisition cancelled by caller.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during token acquisition.");
            throw;
        }
#pragma warning restore CA1848 // Use the LoggerMessage delegates
    }

    private GrpcEndpointAuthenticationDevelopment? GetDevelopmentAuthentication(string endpointKey)
    {
        GrpcSettingsDevelopment devGrpcSettings = _configuration.GetDevelopmentGrpcSettings();

        KeyValuePair<string, GrpcEndpointDevelopment> grpcEndpoint =
            devGrpcSettings.Endpoints.FirstOrDefault(e => e.Value.Key.Equals(endpointKey, StringComparison.OrdinalIgnoreCase));

        return grpcEndpoint.Value.Authentication;
    }
}