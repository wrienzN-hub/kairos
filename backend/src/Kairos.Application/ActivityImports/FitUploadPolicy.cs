namespace Kairos.Application.ActivityImports;

public sealed record FitUploadPolicy
{
    public const long DefaultMaximumFileSizeBytes = 10 * 1024 * 1024;

    public FitUploadPolicy(long maximumFileSizeBytes = DefaultMaximumFileSizeBytes)
    {
        if (maximumFileSizeBytes is < 1 or > int.MaxValue)
        {
            throw new ArgumentOutOfRangeException(nameof(maximumFileSizeBytes));
        }

        MaximumFileSizeBytes = maximumFileSizeBytes;
    }

    public long MaximumFileSizeBytes { get; }
}
