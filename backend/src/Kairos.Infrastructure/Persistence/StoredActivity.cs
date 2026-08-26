namespace Kairos.Infrastructure.Persistence;

public sealed class StoredActivity
{
    public Guid Id { get; set; }

    public required string OwnerSubject { get; set; }

    public Guid SourceUploadId { get; set; }

    public required string ActivityType { get; set; }

    public DateTimeOffset StartUtc { get; set; }

    public DateTimeOffset EndUtc { get; set; }

    public required string SourceKind { get; set; }

    public required string SourceProvider { get; set; }

    public string? OriginalFileName { get; set; }

    public string? ContentHashSha256 { get; set; }

    public DateTimeOffset ImportedAtUtc { get; set; }

    public required string Document { get; set; }
}
