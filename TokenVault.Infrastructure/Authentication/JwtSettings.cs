namespace TokenVault.Infrastructure.Authentication;

public class JwtSettings
{
    public const string SectionName = "JwtSettings";
    public string Secret { get; init; } = null!;
    public int ExpiryMinutes { get; init; }
    public string Isuuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
}