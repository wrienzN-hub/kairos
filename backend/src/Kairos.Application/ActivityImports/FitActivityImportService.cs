using Kairos.Application.Activities;
using Kairos.Domain.Activities;

namespace Kairos.Application.ActivityImports;

public sealed record ActivityImportReceipt(
    Guid Id,
    string Type,
    DateTimeOffset StartUtc,
    DateTimeOffset EndUtc,
    int SampleCount,
    int SegmentCount,
    string AnalysisStatus,
    int QualityFindingCount,
    string Status
);

public sealed class ActivityImportException(string code, string message) : Exception(message)
{
    public string Code { get; } = code;
}

public sealed class FitActivityImportService(
    IFitUploadStore uploadStore,
    IFitActivityParser parser,
    IActivityStore activityStore,
    ActivityQualityEvaluator qualityEvaluator
)
{
    public async Task<ActivityImportReceipt> ImportAsync(
        Guid uploadId,
        string ownerSubject,
        CancellationToken cancellationToken
    )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(ownerSubject);
        var upload = await uploadStore.LoadAsync(uploadId, ownerSubject, cancellationToken)
            ?? throw new ActivityImportException(
                "fit_upload_not_found",
                "Der FIT-Upload wurde nicht gefunden."
            );

        if (!string.Equals(upload.Status, "pending", StringComparison.Ordinal))
        {
            throw new ActivityImportException(
                "fit_upload_not_pending",
                "Der FIT-Upload kann in seinem aktuellen Zustand nicht importiert werden."
            );
        }

        var existing = await activityStore.FindBySourceHashAsync(
            ownerSubject,
            upload.Sha256,
            cancellationToken
        );
        if (existing is not null)
        {
            await uploadStore.SetStatusAsync(
                upload.Id,
                ownerSubject,
                "duplicate",
                cancellationToken
            );
            return ToReceipt(existing, "duplicate");
        }

        Activity activity;
        try
        {
            activity = qualityEvaluator.Evaluate(
                parser.Parse(
                    new FitActivityFile(
                        upload.Id,
                        upload.OriginalFileName,
                        upload.Sha256,
                        upload.UploadedAtUtc,
                        upload.Content
                    )
                )
            );
        }
        catch (FitParseException exception)
        {
            throw new ActivityImportException(exception.Code, exception.Message);
        }

        await activityStore.AddImportedAsync(
            ownerSubject,
            upload.Id,
            activity,
            cancellationToken
        );

        return ToReceipt(activity, "imported");
    }

    private static ActivityImportReceipt ToReceipt(Activity activity, string status) =>
        new(
            activity.Id,
            activity.Type.Code,
            activity.TimeRange.Start.InstantUtc,
            activity.TimeRange.End.InstantUtc,
            activity.Samples.Count,
            activity.Segments.Count,
            activity.Quality.AnalysisStatus,
            activity.Quality.Findings.Count,
            status
        );

    public Task<Activity?> FindAsync(
        Guid id,
        string ownerSubject,
        CancellationToken cancellationToken
    )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(ownerSubject);
        return activityStore.FindAsync(id, ownerSubject, cancellationToken);
    }

    public Task<IReadOnlyList<ActivityListItem>> ListAsync(
        string ownerSubject,
        CancellationToken cancellationToken
    )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(ownerSubject);
        return activityStore.ListAsync(ownerSubject, cancellationToken);
    }

    public Task<Activity?> FindForExportAsync(
        Guid id,
        string ownerSubject,
        CancellationToken cancellationToken
    )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(ownerSubject);
        return activityStore.FindForExportAsync(id, ownerSubject, cancellationToken);
    }

    public Task<bool> DeleteAsync(
        Guid id,
        string ownerSubject,
        CancellationToken cancellationToken
    )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(ownerSubject);
        return activityStore.DeleteAsync(id, ownerSubject, cancellationToken);
    }
}
