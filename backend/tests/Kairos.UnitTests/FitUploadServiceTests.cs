using Kairos.Application.ActivityImports;

namespace Kairos.UnitTests;

public sealed class FitUploadServiceTests
{
    private static readonly string FixtureDirectory = Path.Combine(
        AppContext.BaseDirectory,
        "Fixtures",
        "Fit"
    );

    [Fact]
    public async Task Valid_upload_is_owned_hashed_and_persisted_once()
    {
        var store = new RecordingStore();
        var service = new FitUploadService(store, new FitUploadPolicy());
        var bytes = ReadFixture("minimal-cycling.fit");

        var receipt = await service.UploadAsync(
            "athlete-123",
            @"C:\fakepath\morning.fit",
            "application/octet-stream",
            bytes.Length,
            new MemoryStream(bytes),
            CancellationToken.None
        );

        var stored = Assert.Single(store.Uploads);
        Assert.Equal("athlete-123", stored.OwnerSubject);
        Assert.Equal("morning.fit", stored.OriginalFileName);
        Assert.Equal(bytes, stored.Content);
        Assert.Equal(stored.Id, receipt.Id);
        Assert.Equal(stored.Sha256, receipt.Sha256);
        Assert.Equal("pending", receipt.Status);
    }

    [Fact]
    public async Task Invalid_upload_does_not_persist_anything()
    {
        var store = new RecordingStore();
        var service = new FitUploadService(store, new FitUploadPolicy());
        var bytes = ReadFixture("corrupted-crc.fit");

        var exception = await Assert.ThrowsAsync<FitUploadException>(() =>
            service.UploadAsync(
                "athlete-123",
                "broken.fit",
                "application/vnd.ant.fit",
                bytes.Length,
                new MemoryStream(bytes),
                CancellationToken.None
            )
        );

        Assert.Equal("invalid_fit_crc", exception.Code);
        Assert.Empty(store.Uploads);
    }

    [Fact]
    public async Task Oversized_upload_is_rejected_before_it_is_read_or_persisted()
    {
        var store = new RecordingStore();
        var service = new FitUploadService(store, new FitUploadPolicy(32));
        var source = new TrackingStream(ReadFixture("minimal-cycling.fit"));

        var exception = await Assert.ThrowsAsync<FitUploadException>(() =>
            service.UploadAsync(
                "athlete-123",
                "activity.fit",
                "application/octet-stream",
                source.Length,
                source,
                CancellationToken.None
            )
        );

        Assert.Equal("file_too_large", exception.Code);
        Assert.False(source.WasRead);
        Assert.Empty(store.Uploads);
    }

    [Fact]
    public async Task Underreported_length_cannot_bypass_the_stream_size_limit()
    {
        var store = new RecordingStore();
        var bytes = ReadFixture("minimal-cycling.fit");
        var service = new FitUploadService(store, new FitUploadPolicy(bytes.Length - 1));

        var exception = await Assert.ThrowsAsync<FitUploadException>(() =>
            service.UploadAsync(
                "athlete-123",
                "activity.fit",
                "application/octet-stream",
                bytes.Length - 1,
                new MemoryStream(bytes),
                CancellationToken.None
            )
        );

        Assert.Equal("file_too_large", exception.Code);
        Assert.Empty(store.Uploads);
    }

    [Fact]
    public async Task Disguised_file_type_is_rejected()
    {
        var store = new RecordingStore();
        var service = new FitUploadService(store, new FitUploadPolicy());
        var bytes = ReadFixture("minimal-cycling.fit");

        var exception = await Assert.ThrowsAsync<FitUploadException>(() =>
            service.UploadAsync(
                "athlete-123",
                "activity.txt",
                "text/plain",
                bytes.Length,
                new MemoryStream(bytes),
                CancellationToken.None
            )
        );

        Assert.Equal("unsupported_file_type", exception.Code);
        Assert.Empty(store.Uploads);
    }

    private static byte[] ReadFixture(string fileName)
    {
        return File.ReadAllBytes(Path.Combine(FixtureDirectory, fileName));
    }

    private sealed class RecordingStore : IFitUploadStore
    {
        public List<FitUploadSubmission> Uploads { get; } = [];

        public Task AddAsync(FitUploadSubmission upload, CancellationToken cancellationToken)
        {
            Uploads.Add(upload);
            return Task.CompletedTask;
        }

        public Task<FitUploadReceipt?> FindAsync(
            Guid id,
            string ownerSubject,
            CancellationToken cancellationToken
        )
        {
            var upload = Uploads.SingleOrDefault(value =>
                value.Id == id && value.OwnerSubject == ownerSubject
            );
            return Task.FromResult(
                upload is null
                    ? null
                    : new FitUploadReceipt(
                        upload.Id,
                        upload.OriginalFileName,
                        upload.SizeBytes,
                        upload.Sha256,
                        upload.UploadedAtUtc,
                        "pending"
                    )
            );
        }

        public Task<FitUploadContent?> LoadAsync(
            Guid id,
            string ownerSubject,
            CancellationToken cancellationToken
        )
        {
            var upload = Uploads.SingleOrDefault(value =>
                value.Id == id && value.OwnerSubject == ownerSubject
            );
            return Task.FromResult(
                upload is null
                    ? null
                    : new FitUploadContent(
                        upload.Id,
                        upload.OwnerSubject,
                        upload.OriginalFileName,
                        upload.Sha256,
                        upload.UploadedAtUtc,
                        "pending",
                        upload.Content
                    )
            );
        }

        public Task SetStatusAsync(
            Guid id,
            string ownerSubject,
            string status,
            CancellationToken cancellationToken
        ) => Task.CompletedTask;
    }

    private sealed class TrackingStream(byte[] content) : MemoryStream(content)
    {
        public bool WasRead { get; private set; }

        public override ValueTask<int> ReadAsync(
            Memory<byte> buffer,
            CancellationToken cancellationToken = default
        )
        {
            WasRead = true;
            return base.ReadAsync(buffer, cancellationToken);
        }
    }
}
