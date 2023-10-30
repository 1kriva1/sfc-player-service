namespace SFC.Players.Infrastructure.Settings;
public class JwtSettings
{
    public const string SECTION_KEY = "JwtSettings";

    public string Key { get; set; } = null!;

    public string Issuer { get; set; } = null!;

    public string Audience { get; set; } = null!;
}
