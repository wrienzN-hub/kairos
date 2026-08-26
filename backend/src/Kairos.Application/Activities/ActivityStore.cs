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
}
