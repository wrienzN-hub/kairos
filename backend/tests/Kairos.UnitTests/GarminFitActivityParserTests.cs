using System.Security.Cryptography;
using Kairos.Application.ActivityImports;
using Kairos.Domain.Activities;
using Kairos.Infrastructure.ActivityImports;

namespace Kairos.UnitTests;

public sealed class GarminFitActivityParserTests
{
    private readonly GarminFitActivityParser parser = new();

    [Theory]
    [InlineData("valid-cycling.fit", "2026-01-15T06:00:00Z", "2026-01-15T06:30:00Z", 1800, 10000, 4, 1)]
    [InlineData("minimal-cycling.fit", "2026-01-16T12:00:00Z", "2026-01-16T12:05:00Z", 300, 1000, 2, 1)]
    [InlineData("interval-cycling.fit", "2026-01-17T09:00:00Z", "2026-01-17T09:20:00Z", 1200, 8000, 5, 2)]
    [InlineData("incomplete-cycling.fit", "2026-01-18T07:30:00Z", "2026-01-18T07:45:00Z", 900, 4500, 3, 1)]
    public void Parse_NormalizesReferenceFixtures(
        string fileName,
        string expectedStart,
        string expectedEnd,
        decimal expectedDuration,
        decimal expectedDistance,
        int expectedSamples,
        int expectedLaps
    )
    {
        var activity = parser.Parse(Input(fileName));

        Assert.Equal(ActivityType.Cycling, activity.Type);
        Assert.Equal(DateTimeOffset.Parse(expectedStart), activity.TimeRange.Start.InstantUtc);
        Assert.Equal(DateTimeOffset.Parse(expectedEnd), activity.TimeRange.End.InstantUtc);
        Assert.Equal(expectedDuration, activity.Summary.Find("duration")?.Value);
        Assert.Equal(expectedDistance, activity.Summary.Find("distance")?.Value);
        Assert.Equal(expectedSamples, activity.Samples.Count);
        Assert.Equal(expectedLaps, activity.Segments.Count);
    }

    [Fact]
    public void Parse_NormalizesUnitsAndRetainsProvenance()
    {
        var input = Input("valid-cycling.fit");
        var activity = parser.Parse(input);

        Assert.Equal("fit_file", activity.Source.Kind);
        Assert.Equal("file_import", activity.Source.Provider);
        Assert.Equal(input.UploadId.ToString(), activity.Source.ExternalIdentifier);
        Assert.Equal(input.OriginalFileName, activity.Source.OriginalFileName);
        Assert.Equal(input.Sha256, activity.Source.ContentHashSha256);

        var first = activity.Samples[0];
        var latitude = first.Metrics.Single(metric => metric.Code == "latitude");
        Assert.InRange(latitude.Value, 48.20819m, 48.20821m);
        Assert.Equal("degrees", latitude.Unit.Code);
        Assert.Equal("record.position_lat", latitude.Provenance.SourceField);
        Assert.Equal("semicircles", latitude.Provenance.SourceUnit);

        var speed = first.Metrics.Single(metric => metric.Code == "speed");
        Assert.Equal(5.5m, speed.Value);
        Assert.Equal(MeasurementUnit.MetersPerSecond, speed.Unit);
        Assert.Equal(DataOrigin.Measured, speed.Provenance.Origin);

        var duration = activity.Summary.Find("duration")!;
        Assert.Equal(MeasurementUnit.Seconds, duration.Unit);
        Assert.Equal(DataOrigin.ImportedSummary, duration.Provenance.Origin);
    }

    [Fact]
    public void Parse_PreservesLapOrderAndNormalizedSummaries()
    {
        var activity = parser.Parse(Input("interval-cycling.fit"));

        Assert.Collection(
            activity.Segments,
            first =>
            {
                Assert.Equal(0, first.Index);
                Assert.Equal(600m, first.Summary.Find("duration")?.Value);
                Assert.Equal(3500m, first.Summary.Find("distance")?.Value);
            },
            second =>
            {
                Assert.Equal(1, second.Index);
                Assert.Equal(600m, second.Summary.Find("duration")?.Value);
                Assert.Equal(4500m, second.Summary.Find("distance")?.Value);
            }
        );
    }

    [Fact]
    public void Parse_AcceptsMissingCadenceAndPower()
    {
        var activity = parser.Parse(Input("incomplete-cycling.fit"));

        Assert.DoesNotContain(activity.Summary.Metrics, metric =>
            metric.Code is "average_cadence" or "maximum_cadence" or "average_power" or "maximum_power");
        Assert.All(activity.Samples, sample =>
            Assert.DoesNotContain(sample.Metrics, metric => metric.Code is "cadence" or "power"));
    }

    [Fact]
    public void Parse_IgnoresSupportedButUnmappedMessages()
    {
        // This fixture also contains file-id, event and activity messages that
        // Kairos does not map. They must not block a valid import.
        var activity = parser.Parse(Input("minimal-cycling.fit"));

        Assert.Equal(2, activity.Samples.Count);
    }

    [Fact]
    public void Parse_RejectsCorruptedFitFile()
    {
        var exception = Assert.Throws<FitParseException>(() =>
            parser.Parse(Input("corrupted-crc.fit"))
        );

        Assert.Equal("invalid_fit_structure", exception.Code);
    }

    private static FitActivityFile Input(string fileName)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Fixtures", "Fit", fileName);
        var content = System.IO.File.ReadAllBytes(path);
        var sha256 = Convert.ToHexString(SHA256.HashData(content)).ToLowerInvariant();

        return new FitActivityFile(
            Guid.Parse("d68236fd-cb67-4f15-a8e0-e2f9447779c0"),
            fileName,
            sha256,
            new DateTimeOffset(2026, 8, 24, 12, 0, 0, TimeSpan.Zero),
            content
        );
    }
}
