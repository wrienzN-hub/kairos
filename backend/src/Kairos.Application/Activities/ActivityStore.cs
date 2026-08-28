using Kairos.Domain.Activities;

namespace Kairos.Application.Activities;

public sealed record ActivityListItem(
    Guid Id,
    string Type,
    DateTimeOffset StartUtc,
    DateTimeOffset EndUtc,
    decimal? DistanceMeters,
    string AnalysisStatus,
    string? OriginalFileName,
    string SourceProvider
);

public interface IActivityStore
{
    Task AddImportedAsync(
        string ownerSubject,
        Guid sourceUploadId,
        Activity activity,
        CancellationToken cancellationToken
    );

    Task<Activity?> FindAsync(
        Guid id,
        string ownerSubject,
        CancellationToken cancellationToken
    );

    Task<Activity?> FindBySourceHashAsync(
        string ownerSubject,
        string contentHashSha256,
        CancellationToken cancellationToken
    );

    Task<IReadOnlyList<ActivityListItem>> ListAsync(
        string ownerSubject,
        CancellationToken cancellationToken
    );

    Task<Activity?> FindForExportAsync(
        Guid id,
        string ownerSubject,
        CancellationToken cancellationToken
    );

    Task<bool> DeleteAsync(
        Guid id,
        string ownerSubject,
        CancellationToken cancellationToken
    );
}
