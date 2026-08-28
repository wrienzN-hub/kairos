using Kairos.Application.ActivityImports;
using Kairos.Infrastructure.ActivityImports;
using Kairos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Kairos.IntegrationTests;

public sealed class ActivityPersistenceTests
{
    [Fact]
    public async Task Imported_activity_round_trips_as_complete_owner_scoped_aggregate()
    {
        var options = Options("activity-round-trip");
        var upload = Upload("interval-cycling.fit", "athlete-123");

        await using (var writeContext = new KairosDbContext(options))
        {
            var uploadStore = new EfFitUploadStore(writeContext);
            await uploadStore.AddAsync(upload, CancellationToken.None);

            var parser = new GarminFitActivityParser();
            var activity = parser.Parse(
                new FitActivityFile(
                    upload.Id,
                    upload.OriginalFileName,
                    upload.Sha256,
                    upload.UploadedAtUtc,
                    upload.Content
                )
            );
            var activityStore = new EfActivityStore(writeContext);
            await activityStore.AddImportedAsync(
                upload.OwnerSubject,
                upload.Id,
                activity,
                CancellationToken.None
            );
        }

        await using var readContext = new KairosDbContext(options);
        var readStore = new EfActivityStore(readContext);
        var restored = await readStore.FindAsync(
            upload.Id,
            upload.OwnerSubject,
            CancellationToken.None
        );

        Assert.NotNull(restored);
        Assert.Equal("cycling", restored.Type.Code);
        Assert.Equal(5, restored.Samples.Count);
        Assert.Equal(2, restored.Segments.Count);
        Assert.Equal(8000m, restored.Summary.Find("distance")?.Value);
        Assert.Equal("record.speed", restored.Samples[0].Metrics.Single(
            metric => metric.Code == "speed"
        ).Provenance.SourceField);
        Assert.Equal("interval-cycling.fit", restored.Source.OriginalFileName);
        Assert.Equal("imported", (await readContext.FitUploads.SingleAsync()).Status);

        var otherOwner = await readStore.FindAsync(
            upload.Id,
            "other-athlete",
            CancellationToken.None
        );
        Assert.Null(otherOwner);
    }

    [Fact]
    public async Task Import_service_parses_persists_and_returns_the_activity()
    {
        var options = Options("activity-service");
        var upload = Upload("incomplete-cycling.fit", "athlete-123");
        await using var context = new KairosDbContext(options);
        var uploadStore = new EfFitUploadStore(context);
        var activityStore = new EfActivityStore(context);
        await uploadStore.AddAsync(upload, CancellationToken.None);
        var service = new FitActivityImportService(
            uploadStore,
            new GarminFitActivityParser(),
            activityStore,
            new Kairos.Application.Activities.ActivityQualityEvaluator()
        );

        var receipt = await service.ImportAsync(
            upload.Id,
            upload.OwnerSubject,
            CancellationToken.None
        );
        var activity = await service.FindAsync(
            receipt.Id,
            upload.OwnerSubject,
            CancellationToken.None
        );

        Assert.Equal("imported", receipt.Status);
        Assert.Equal(3, receipt.SampleCount);
        Assert.NotNull(activity);
        Assert.DoesNotContain(
            activity.Samples.SelectMany(sample => sample.Metrics),
            metric => metric.Code is "cadence" or "power"
        );
    }

    [Fact]
    public async Task Missing_upload_leaves_no_activity_or_accepted_upload()
    {
        var options = Options("activity-failed-import");
        await using var context = new KairosDbContext(options);
        var store = new EfActivityStore(context);
        var upload = Upload("minimal-cycling.fit", "athlete-123");
        var activity = new GarminFitActivityParser().Parse(
            new FitActivityFile(
                upload.Id,
                upload.OriginalFileName,
                upload.Sha256,
                upload.UploadedAtUtc,
                upload.Content
            )
        );

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            store.AddImportedAsync(
                upload.OwnerSubject,
                upload.Id,
                activity,
                CancellationToken.None
            )
        );

        Assert.Empty(context.Activities);
        Assert.Empty(context.FitUploads.Where(value => value.Status == "imported"));
    }

    [Fact]
    public async Task Reimporting_same_content_returns_existing_activity_without_duplicate_load()
    {
        var options = Options("activity-duplicate");
        await using var context = new KairosDbContext(options);
        var uploadStore = new EfFitUploadStore(context);
        var activityStore = new EfActivityStore(context);
        var service = new FitActivityImportService(
            uploadStore,
            new GarminFitActivityParser(),
            activityStore,
            new Kairos.Application.Activities.ActivityQualityEvaluator()
        );
        var firstUpload = Upload("minimal-cycling.fit", "athlete-123");
        var secondUpload = firstUpload with { Id = Guid.NewGuid() };
        await uploadStore.AddAsync(firstUpload, CancellationToken.None);
        var firstReceipt = await service.ImportAsync(
            firstUpload.Id,
            firstUpload.OwnerSubject,
            CancellationToken.None
        );
        await uploadStore.AddAsync(secondUpload, CancellationToken.None);

        var duplicateReceipt = await service.ImportAsync(
            secondUpload.Id,
            secondUpload.OwnerSubject,
            CancellationToken.None
        );

        Assert.Equal("imported", firstReceipt.Status);
        Assert.Equal("duplicate", duplicateReceipt.Status);
        Assert.Equal(firstReceipt.Id, duplicateReceipt.Id);
        Assert.Single(context.Activities);
        Assert.Equal(
            "duplicate",
            (await context.FitUploads.SingleAsync(value => value.Id == secondUpload.Id)).Status
        );
    }

    private static DbContextOptions<KairosDbContext> Options(string prefix) =>
        new DbContextOptionsBuilder<KairosDbContext>()
            .UseInMemoryDatabase($"{prefix}-{Guid.NewGuid():N}")
            .Options;

    private static FitUploadSubmission Upload(string fileName, string ownerSubject)
    {
        var content = System.IO.File.ReadAllBytes(
            Path.Combine(AppContext.BaseDirectory, "Fixtures", "Fit", fileName)
        );
        return new FitUploadSubmission(
            Guid.NewGuid(),
            ownerSubject,
            fileName,
            "application/octet-stream",
            content.LongLength,
            Convert.ToHexStringLower(System.Security.Cryptography.SHA256.HashData(content)),
            new DateTimeOffset(2026, 8, 26, 10, 0, 0, TimeSpan.Zero),
            content
        );
    }
}
