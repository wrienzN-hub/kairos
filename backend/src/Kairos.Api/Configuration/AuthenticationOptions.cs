namespace Kairos.Api.Configuration;

public sealed class AuthenticationOptions
{
    public const string SectionName = "Authentication";

    public string Authority { get; init; } = string.Empty;

    public string Audience { get; init; } = string.Empty;

    public string? ValidIssuer { get; init; }

    public string? BackchannelAuthority { get; init; }

    public bool RequireHttpsMetadata { get; init; } = true;
}
