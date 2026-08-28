using Kairos.Domain.Activities;

namespace Kairos.Application.Activities;

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
}
