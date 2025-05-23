using SFC.Player.Application.Common.Settings;

namespace SFC.Player.Infrastructure.Settings.Grpc;

public class GrpcSettings : BaseGrpcSettings
{
    public Dictionary<string, GrpcEndpoint> Endpoints { get; init; } = [];
}

public class GrpcEndpoint
{
    public required string Key { get; set; }

    public required Uri Uri { get; set; }

    public GrpcEndpointAuthentication? Authentication { get; set; }
}

public class GrpcEndpointAuthentication
{
    public required string ClientId { get; set; }

    public required string ClientSecret { get; set; }

    public string? Scopes { get; set; }
}