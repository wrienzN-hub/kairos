namespace Kairos.Infrastructure.Persistence;

public sealed class StoredFitUpload
{
    public Guid Id { get; set; }

    public required string OwnerSubject { get; set; }

    public required string OriginalFileName { get; set; }

    public required string ContentType { get; set; }

    public long SizeBytes { get; set; }

    public required string Sha256 { get; set; }

    public DateTimeOffset UploadedAtUtc { get; set; }

    public required string Status { get; set; }

    public required byte[] Content { get; set; }
}
