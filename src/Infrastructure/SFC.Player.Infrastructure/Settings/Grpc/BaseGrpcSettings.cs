namespace SFC.Player.Infrastructure.Settings.Grpc;
public class BaseGrpcSettings
{
    public const string SectionKey = "Grpc";

    public int MaxReceiveMessageSizeInMb { get; set; }

    public int MaxSendMessageSizeInMb { get; set; }

    public int? DeadLineInSeconds { get; set; }

    public GrpcRetrySettings? Retry { get; set; }
}

public class GrpcRetrySettings
{
    public int? MaxAttempts { get; set; }

    public int? InitialBackoffInSeconds { get; set; }

    public int? MaxBackoffInSeconds { get; set; }

    public double? BackoffMultiplier { get; set; }
}
