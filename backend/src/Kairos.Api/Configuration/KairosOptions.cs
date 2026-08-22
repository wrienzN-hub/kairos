namespace Kairos.Api.Configuration;

public sealed class KairosOptions
{
    public const string SectionName = "Kairos";

    public string ProductName { get; init; } = string.Empty;
}
