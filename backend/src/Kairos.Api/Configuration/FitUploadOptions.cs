namespace Kairos.Api.Configuration;

public sealed class FitUploadOptions
{
    public const string SectionName = "FitUpload";

    public long MaximumFileSizeBytes { get; init; } =
        Kairos.Application.ActivityImports.FitUploadPolicy.DefaultMaximumFileSizeBytes;
}
