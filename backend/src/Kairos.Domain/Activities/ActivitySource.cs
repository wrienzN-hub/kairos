namespace Kairos.Domain.Activities;

public sealed record ActivitySource
{
    public string Kind { get; }
    public string Provider { get; }
    public string? ExternalIdentifier { get; }
    public string? OriginalFileName { get; }
    public string? ContentHashSha256 { get; }
    public DateTimeOffset ImportedAtUtc { get; }

    public ActivitySource(
        string kind,
        string provider,
        DateTimeOffset importedAtUtc,
        string? externalIdentifier = null,
        string? originalFileName = null,
        string? contentHashSha256 = null
    )
    {
        Kind = DomainValue.Code(kind, nameof(kind));
        Provider = DomainValue.Code(provider, nameof(provider));
        ImportedAtUtc = DomainValue.Utc(importedAtUtc, nameof(importedAtUtc));
        ExternalIdentifier = Optional(externalIdentifier);
        OriginalFileName = Optional(originalFileName);
        ContentHashSha256 = NormalizeSha256(contentHashSha256);

        if (
            ExternalIdentifier is null
            && OriginalFileName is null
            && ContentHashSha256 is null
            && Kind != "manual"
        )
        {
            throw new ArgumentException(
                "A non-manual source requires an external identifier, file name or content hash."
            );
        }
    }

    private static string? Optional(string? value) =>
        string.IsNullOrWhiteSpace(value) ? null : value.Trim();

    private static string? NormalizeSha256(string? value)
    {
        var normalized = Optional(value)?.ToLowerInvariant();
        if (
            normalized is not null
            && (normalized.Length != 64 || normalized.Any(character => !char.IsAsciiHexDigit(character)))
        )
        {
            throw new ArgumentException(
                "Content hash must be a 64-character hexadecimal SHA-256 value.",
                nameof(value)
            );
        }

        return normalized;
    }
}
