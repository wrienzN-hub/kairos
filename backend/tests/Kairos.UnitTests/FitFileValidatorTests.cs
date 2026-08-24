using Kairos.Application.ActivityImports;

namespace Kairos.UnitTests;

public sealed class FitFileValidatorTests
{
    private static readonly string FixtureDirectory = Path.Combine(
        AppContext.BaseDirectory,
        "Fixtures",
        "Fit"
    );

    [Theory]
    [InlineData("valid-cycling.fit")]
    [InlineData("minimal-cycling.fit")]
    [InlineData("interval-cycling.fit")]
    [InlineData("incomplete-cycling.fit")]
    public void Supported_reference_files_pass_container_validation(string fileName)
    {
        var content = File.ReadAllBytes(Path.Combine(FixtureDirectory, fileName));

        FitFileValidator.Validate(content);
    }

    [Fact]
    public void Corrupted_reference_file_reports_a_crc_error()
    {
        var content = File.ReadAllBytes(Path.Combine(FixtureDirectory, "corrupted-crc.fit"));

        var exception = Assert.Throws<FitUploadException>(() =>
            FitFileValidator.Validate(content)
        );

        Assert.Equal("invalid_fit_crc", exception.Code);
        Assert.Equal(400, exception.StatusCode);
    }

    [Fact]
    public void Non_fit_content_reports_a_signature_error()
    {
        var content = File.ReadAllBytes(Path.Combine(FixtureDirectory, "minimal-cycling.fit"));
        content[8] = (byte)'X';

        var exception = Assert.Throws<FitUploadException>(() =>
            FitFileValidator.Validate(content)
        );

        Assert.Equal("invalid_fit_signature", exception.Code);
    }
}
