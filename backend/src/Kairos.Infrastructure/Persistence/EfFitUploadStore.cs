using Kairos.Application.ActivityImports;
using Microsoft.EntityFrameworkCore;

namespace Kairos.Infrastructure.Persistence;

public sealed class EfFitUploadStore(KairosDbContext dbContext) : IFitUploadStore
{
    public async Task AddAsync(
        FitUploadSubmission upload,
        CancellationToken cancellationToken
    )
    {
        dbContext.FitUploads.Add(
            new StoredFitUpload
            {
                Id = upload.Id,
                OwnerSubject = upload.OwnerSubject,
                OriginalFileName = upload.OriginalFileName,
                ContentType = upload.ContentType,
                SizeBytes = upload.SizeBytes,
                Sha256 = upload.Sha256,
                UploadedAtUtc = upload.UploadedAtUtc,
                Status = "pending",
                Content = upload.Content,
            }
        );
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<FitUploadReceipt?> FindAsync(
        Guid id,
        string ownerSubject,
        CancellationToken cancellationToken
    )
    {
        return dbContext
            .FitUploads.AsNoTracking()
            .Where(upload => upload.Id == id && upload.OwnerSubject == ownerSubject)
            .Select(upload => new FitUploadReceipt(
                upload.Id,
                upload.OriginalFileName,
                upload.SizeBytes,
                upload.Sha256,
                upload.UploadedAtUtc,
                upload.Status
            ))
            .SingleOrDefaultAsync(cancellationToken);
    }

    public Task<FitUploadContent?> LoadAsync(
        Guid id,
        string ownerSubject,
        CancellationToken cancellationToken
    )
    {
        return dbContext
            .FitUploads.AsNoTracking()
            .Where(upload => upload.Id == id && upload.OwnerSubject == ownerSubject)
            .Select(upload => new FitUploadContent(
                upload.Id,
                upload.OwnerSubject,
                upload.OriginalFileName,
                upload.Sha256,
                upload.UploadedAtUtc,
                upload.Status,
                upload.Content
            ))
            .SingleOrDefaultAsync(cancellationToken);
    }
}
