using System.Security.Cryptography;
using Kairos.Application.Activities;
using Kairos.Application.ActivityImports;
using Kairos.Domain.Activities;
using Kairos.Infrastructure.ActivityImports;

namespace Kairos.UnitTests;

public sealed class ActivityQualityEvaluatorTests
{
    private readonly ActivityQualityEvaluator evaluator = new();
    private readonly GarminFitActivityParser parser = new();

    [Fact]
    public void Complete_reference_activity_is_eligible_for_analysis()
    {
        var activity = evaluator.Evaluate(Parse("valid-cycling.fit"));

        Assert.Equal("eligible", activity.Quality.AnalysisStatus);
        Assert.False(activity.Quality.IsAnalysisRestricted);
        Assert.Empty(activity.Quality.Findings);
    }

    [Fact]
    public void Missing_power_and_cadence_are_explained_without_rejecting_activity()
    {
        var activity = evaluator.Evaluate(Parse("incomplete-cycling.fit"));

        Assert.Equal("limited", activity.Quality.AnalysisStatus);
        Assert.Contains(
            activity.Quality.Findings,
            finding =>
                finding.Code == "missing_power_stream"
                && finding.Severity == QualitySeverity.Warning
                && finding.Message.Contains("Leistungsbasierte Analysen", StringComparison.Ordinal)
        );
        Assert.Contains(
            activity.Quality.Findings,
            finding => finding.Code == "missing_cadence_stream"
        );
    }

    [Fact]
    public void Implausible_measurement_blocks_analysis_with_affected_metric()
    {
        var parsed = Parse("valid-cycling.fit");
        var samples = parsed.Samples
            .Select((sample, index) =>
                index == 0
                    ? new ActivitySample(
                        sample.TimestampUtc,
                        sample.Metrics.Select(metric =>
                            metric.Code == "power"
                                ? new ActivityMetric(
                                    metric.Code,
                                    3000,
                                    metric.Unit,
                                    metric.Provenance
                                )
                                : metric
                        )
                    )
                    : sample
            )
            .ToArray();
        var activity = new Activity(
            parsed.Id,
            parsed.Type,
            parsed.Source,
            parsed.TimeRange,
            parsed.Summary,
            samples,
            parsed.Segments
        );

        var assessed = evaluator.Evaluate(activity);

        Assert.Equal("blocked", assessed.Quality.AnalysisStatus);
        var finding = Assert.Single(
            assessed.Quality.Findings,
            value => value.Code == "implausible_power"
        );
        Assert.Equal(QualitySeverity.Error, finding.Severity);
        Assert.Equal(["power"], finding.AffectedMetricCodes);
    }

    private Activity Parse(string fileName)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Fixtures", "Fit", fileName);
        var content = System.IO.File.ReadAllBytes(path);
        return parser.Parse(
            new FitActivityFile(
                Guid.NewGuid(),
                fileName,
                Convert.ToHexStringLower(SHA256.HashData(content)),
                new DateTimeOffset(2026, 8, 26, 10, 0, 0, TimeSpan.Zero),
                content
            )
        );
    }
}
