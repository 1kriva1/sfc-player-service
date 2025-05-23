namespace SFC.Player.Infrastructure.Settings.Grpc;
public class GrpcSettingsDevelopment : BaseGrpcSettings
{
    public Dictionary<string, GrpcEndpointDevelopment> Endpoints { get; init; } = [];
}

public class GrpcEndpointDevelopment
{
    public required string Key { get; set; }

    public required GrpcEndpointAuthenticationDevelopment Authentication { get; set; }
}

public class GrpcEndpointAuthenticationDevelopment
{
    public required string AccessToken { get; set; }
}