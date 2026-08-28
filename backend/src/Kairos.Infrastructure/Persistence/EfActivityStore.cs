using Kairos.Application.Activities;
using Kairos.Domain.Activities;
using Microsoft.EntityFrameworkCore;

namespace Kairos.Infrastructure.Persistence;

public sealed class EfActivityStore(KairosDbContext dbContext) : IActivityStore
{
    public async Task AddImportedAsync(
        string ownerSubject,
        Guid sourceUploadId,
        Activity activity,
        CancellationToken cancellationToken
    )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(ownerSubject);
        ArgumentNullException.ThrowIfNull(activity);

        var upload = await dbContext.FitUploads.SingleOrDefaultAsync(
            value => value.Id == sourceUploadId && value.OwnerSubject == ownerSubject,
            cancellationToken
        ) ?? throw new InvalidOperationException("The source FIT upload no longer exists.");

        if (!string.Equals(upload.Status, "pending", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("The source FIT upload is not pending.");
        }

        dbContext.Activities.Add(
            new StoredActivity
            {
                Id = activity.Id,
                OwnerSubject = ownerSubject,
                SourceUploadId = sourceUploadId,
                ActivityType = activity.Type.Code,
                StartUtc = activity.TimeRange.Start.InstantUtc,
                EndUtc = activity.TimeRange.End.InstantUtc,
                SourceKind = activity.Source.Kind,
                SourceProvider = activity.Source.Provider,
                OriginalFileName = activity.Source.OriginalFileName,
                ContentHashSha256 = activity.Source.ContentHashSha256,
                ImportedAtUtc = activity.Source.ImportedAtUtc,
                Document = ActivityDocumentMapper.Serialize(activity),
            }
        );
        upload.Status = "imported";

        // EF Core wraps both the activity insert and upload state transition in
        // one transaction because they are part of the same SaveChanges call.
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Activity?> FindAsync(
        Guid id,
        string ownerSubject,
        CancellationToken cancellationToken
    )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(ownerSubject);
        var document = await dbContext
            .Activities.AsNoTracking()
            .Where(activity => activity.Id == id && activity.OwnerSubject == ownerSubject)
            .Select(activity => activity.Document)
            .SingleOrDefaultAsync(cancellationToken);

        return document is null ? null : ActivityDocumentMapper.Deserialize(document);
    }

    public async Task<Activity?> FindBySourceHashAsync(
        string ownerSubject,
        string contentHashSha256,
        CancellationToken cancellationToken
    )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(ownerSubject);
        ArgumentException.ThrowIfNullOrWhiteSpace(contentHashSha256);
        var document = await dbContext
            .Activities.AsNoTracking()
            .Where(activity =>
                activity.OwnerSubject == ownerSubject
                && activity.ContentHashSha256 == contentHashSha256
            )
            .Select(activity => activity.Document)
            .SingleOrDefaultAsync(cancellationToken);
        return document is null ? null : ActivityDocumentMapper.Deserialize(document);
    }
}
