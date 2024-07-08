namespace SFC.Player.Infrastructure.Settings;
public class IdentitySettings
{
    public const string SECTION_KEY = "Identity";

    public string Authority { get; set; } = default!;

    public string Audience { get; set; } = default!;

    public IDictionary<string, IEnumerable<string>> RequireClaims { get; set; } = new Dictionary<string, IEnumerable<string>>();
}
