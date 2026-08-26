namespace Kairos.Application.ActivityImports;

public sealed record FitUploadSubmission(
    Guid Id,
    string OwnerSubject,
    string OriginalFileName,
    string ContentType,
    long SizeBytes,
    string Sha256,
    DateTimeOffset UploadedAtUtc,
    byte[] Content
);

public sealed record FitUploadReceipt(
    Guid Id,
    string OriginalFileName,
    long SizeBytes,
    string Sha256,
    DateTimeOffset UploadedAtUtc,
    string Status
);

public sealed record FitUploadContent(
    Guid Id,
    string OwnerSubject,
    string OriginalFileName,
    string Sha256,
    DateTimeOffset UploadedAtUtc,
    string Status,
    byte[] Content
);

public interface IFitUploadStore
{
    Task AddAsync(FitUploadSubmission upload, CancellationToken cancellationToken);

    Task<FitUploadReceipt?> FindAsync(
        Guid id,
        string ownerSubject,
        CancellationToken cancellationToken
    );

    Task<FitUploadContent?> LoadAsync(
        Guid id,
        string ownerSubject,
        CancellationToken cancellationToken
    );
}
