using Kairos.Application.ActivityImports;
using Kairos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Kairos.IntegrationTests;

public sealed class DatabaseIsolationTests
{
    [Fact]
    public async Task Test_database_is_isolated_and_can_be_created()
    {
        var databaseName = $"kairos-test-{Guid.NewGuid():N}";
        var options = new DbContextOptionsBuilder<KairosDbContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;

        await using var context = new KairosDbContext(options);

        Assert.True(await context.Database.EnsureCreatedAsync());
        Assert.Equal("Microsoft.EntityFrameworkCore.InMemory", context.Database.ProviderName);
    }

    [Fact]
    public async Task Fit_upload_store_round_trips_metadata_and_content()
    {
        var options = new DbContextOptionsBuilder<KairosDbContext>()
            .UseInMemoryDatabase($"kairos-fit-store-{Guid.NewGuid():N}")
            .Options;
        await using var context = new KairosDbContext(options);
        var store = new EfFitUploadStore(context);
        var id = Guid.NewGuid();
        var uploadedAt = DateTimeOffset.UtcNow;
        var submission = new FitUploadSubmission(
            id,
            "athlete-123",
            "activity.fit",
            "application/octet-stream",
            3,
            new string('a', 64),
            uploadedAt,
            [1, 2, 3]
        );

        await store.AddAsync(submission, CancellationToken.None);
        var receipt = await store.FindAsync(id, "athlete-123", CancellationToken.None);
        var otherOwnerReceipt = await store.FindAsync(id, "other-athlete", CancellationToken.None);

        Assert.NotNull(receipt);
        Assert.Equal("pending", receipt.Status);
        Assert.Null(otherOwnerReceipt);
        var stored = await context.FitUploads.SingleAsync();
        Assert.Equal([1, 2, 3], stored.Content);
        Assert.Equal("athlete-123", stored.OwnerSubject);
    }
}
