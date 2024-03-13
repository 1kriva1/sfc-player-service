namespace SFC.Player.Application.Common.Settings;
public class CacheSettings
{
    public const string SECTION_KEY = "Cache";

    public bool Enabled { get; set; }

    public string InstanceName { get; set; } = default!;

    public int AbsoluteExpirationInMinutes { get; set; }

    public int SlidingExpirationInMinutes { get; set; }
}
