using Kairos.Domain.Activities;

namespace Kairos.UnitTests;

public sealed class ActivityDomainModelTests
{
    private static readonly DateTimeOffset StartUtc = new(
        2026,
        1,
        15,
        6,
        0,
        0,
        TimeSpan.Zero
    );

    [Fact]
    public void Activity_keeps_source_time_zone_metrics_segments_and_quality_traceable()
    {
        var range = Range(StartUtc, StartUtc.AddMinutes(30));
        var distance = new ActivityMetric(
            "distance",
            10_000,
            MeasurementUnit.Meters,
            DataProvenance.ImportedSummary("session.total_distance", "m")
        );
        var duration = new ActivityMetric(
            "duration",
            1_800,
            MeasurementUnit.Seconds,
            DataProvenance.Derived(
                "elapsed_time_difference",
                "1.0",
                ["start_time", "end_time"]
            )
        );
        var heartRate = new ActivityMetric(
            "heart_rate",
            132,
            MeasurementUnit.BeatsPerMinute,
            DataProvenance.Measured("record.heart_rate", "bpm")
        );
        var source = new ActivitySource(
            "fit_file",
            "file_import",
            StartUtc.AddHours(1),
            originalFileName: "valid-cycling.fit",
            contentHashSha256: new string('a', 64)
        );
        var quality = new ActivityQuality(
            [
                new QualityFinding(
                    "optional_streams_missing",
                    QualitySeverity.Information,
                    "Power and cadence are not available.",
                    ["power", "cadence"]
                ),
            ]
        );

        var activity = new Activity(
            Guid.NewGuid(),
            ActivityType.Cycling,
            source,
            range,
            new ActivitySummary([distance, duration]),
            [new ActivitySample(StartUtc, [heartRate])],
            [new ActivitySegment(0, SegmentType.Lap, range, new ActivitySummary([distance]))],
            quality
        );

        Assert.Equal("cycling", activity.Type.Code);
        Assert.Equal("valid-cycling.fit", activity.Source.OriginalFileName);
        Assert.Equal("Europe/Vienna", activity.TimeRange.Start.TimeZoneId);
        Assert.Equal(TimeSpan.FromHours(1), activity.TimeRange.Start.ObservedUtcOffset);
        Assert.Equal(7, activity.TimeRange.Start.ToObservedLocalTime().Hour);
        Assert.Equal(
            TimeSpan.FromHours(1),
            activity.TimeRange.Start.ToObservedLocalTime().Offset
        );
        Assert.Equal(DataOrigin.ImportedSummary, activity.Summary.Find("distance")!.Provenance.Origin);
        Assert.True(activity.Summary.Find("duration")!.Provenance.IsDerived);
        Assert.Equal("record.heart_rate", activity.Samples[0].Metrics[0].Provenance.SourceField);
        Assert.Equal("lap", activity.Segments[0].Type.Code);
        Assert.False(activity.Quality.HasErrors);
        Assert.Contains("power", activity.Quality.Findings[0].AffectedMetricCodes);
    }

    [Fact]
    public void Sport_metric_unit_and_segment_codes_remain_extensible()
    {
        var strengthLoad = new ActivityMetric(
            "load",
            80,
            MeasurementUnit.Kilograms,
            DataProvenance.UserEntered()
        );
        var repetitions = new ActivityMetric(
            "repetitions",
            5,
            MeasurementUnit.Count,
            DataProvenance.UserEntered()
        );
        var rowingStrokeRate = new ActivityMetric(
            "stroke_rate",
            28,
            MeasurementUnit.From("strokes_per_minute", "spm"),
            DataProvenance.Measured("stroke_rate", "spm")
        );

        Assert.Equal("strength_training", ActivityType.StrengthTraining.Code);
        Assert.Equal("rowing", ActivityType.Rowing.Code);
        Assert.Equal("ski_erg", ActivityType.FromCode("ski_erg").Code);
        Assert.Equal("strength_set", SegmentType.StrengthSet.Code);
        Assert.Equal("repetitions", repetitions.Code);
        Assert.Equal("kg", strengthLoad.Unit.Symbol);
        Assert.Equal("strokes_per_minute", rowingStrokeRate.Unit.Code);
    }

    [Fact]
    public void Derived_values_require_a_versioned_method_and_inputs()
    {
        var provenance = DataProvenance.Derived(
            "average_speed",
            "2.1",
            ["distance", "duration", "duration"]
        );

        Assert.Equal(DataOrigin.Derived, provenance.Origin);
        Assert.Equal("average_speed", provenance.Derivation!.Method);
        Assert.Equal("2.1", provenance.Derivation.Version);
        Assert.Equal(["distance", "duration"], provenance.Derivation.InputMetricCodes);
        Assert.Throws<ArgumentException>(() => DataProvenance.Derived("average", "1", []));
        Assert.Throws<ArgumentException>(() => DataProvenance.Measured(" "));
    }

    [Fact]
    public void Activity_timestamps_separate_utc_instant_zone_and_observed_offset()
    {
        var timestamp = new ActivityTimestamp(
            StartUtc,
            "Europe/Vienna",
            TimeSpan.FromHours(1)
        );

        Assert.Equal(StartUtc, timestamp.InstantUtc);
        Assert.Equal("Europe/Vienna", timestamp.TimeZoneId);
        Assert.Equal(7, timestamp.ToObservedLocalTime().Hour);
        Assert.Throws<ArgumentException>(() =>
            new ActivityTimestamp(StartUtc.ToOffset(TimeSpan.FromHours(1)), "Europe/Vienna", TimeSpan.FromHours(1))
        );
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new ActivityTimestamp(StartUtc, "Europe/Vienna", TimeSpan.FromHours(15))
        );
    }

    [Fact]
    public void Time_range_rejects_an_end_before_the_start()
    {
        Assert.Throws<ArgumentException>(() => Range(StartUtc, StartUtc.AddSeconds(-1)));
    }

    [Fact]
    public void Summary_and_samples_reject_duplicate_or_empty_metrics()
    {
        var first = MeasuredDistance(100);
        var duplicate = MeasuredDistance(200);

        Assert.Throws<ArgumentException>(() => new ActivitySummary([first, duplicate]));
        Assert.Throws<ArgumentException>(() => new ActivitySample(StartUtc, []));
    }

    [Fact]
    public void Activity_rejects_samples_outside_or_not_strictly_ordered_by_utc()
    {
        var range = Range(StartUtc, StartUtc.AddMinutes(10));
        var source = ManualSource();
        var summary = new ActivitySummary([]);

        Assert.Throws<ArgumentException>(() =>
            new Activity(
                Guid.NewGuid(),
                ActivityType.Cycling,
                source,
                range,
                summary,
                [new ActivitySample(StartUtc.AddMinutes(-1), [MeasuredDistance(0)])]
            )
        );
        Assert.Throws<ArgumentException>(() =>
            new Activity(
                Guid.NewGuid(),
                ActivityType.Cycling,
                source,
                range,
                summary,
                [
                    new ActivitySample(StartUtc.AddMinutes(2), [MeasuredDistance(200)]),
                    new ActivitySample(StartUtc.AddMinutes(1), [MeasuredDistance(100)]),
                ]
            )
        );
    }

    [Fact]
    public void Activity_rejects_duplicate_or_out_of_range_segments()
    {
        var range = Range(StartUtc, StartUtc.AddMinutes(10));
        var segmentSummary = new ActivitySummary([]);
        var first = new ActivitySegment(0, SegmentType.Lap, range, segmentSummary);
        var duplicate = new ActivitySegment(0, SegmentType.Interval, range, segmentSummary);
        var outside = new ActivitySegment(
            1,
            SegmentType.Interval,
            Range(StartUtc, StartUtc.AddMinutes(11)),
            segmentSummary
        );

        Assert.Throws<ArgumentException>(() => ActivityWith(range, [first, duplicate]));
        Assert.Throws<ArgumentException>(() => ActivityWith(range, [outside]));
    }

    [Fact]
    public void Imported_sources_require_traceable_identity_and_valid_sha256()
    {
        Assert.Throws<ArgumentException>(() =>
            new ActivitySource("fit_file", "file_import", StartUtc)
        );
        Assert.Throws<ArgumentException>(() =>
            new ActivitySource(
                "fit_file",
                "file_import",
                StartUtc,
                originalFileName: "activity.fit",
                contentHashSha256: "not-a-sha"
            )
        );

        var manual = ManualSource();
        Assert.Equal("manual", manual.Kind);
        Assert.Null(manual.ContentHashSha256);
    }

    [Fact]
    public void Error_quality_findings_make_the_activity_quality_explicitly_erroneous()
    {
        var quality = new ActivityQuality(
            [
                new QualityFinding(
                    "timestamp_order_invalid",
                    QualitySeverity.Error,
                    "Samples are not ordered.",
                    ["timestamp", "timestamp"]
                ),
            ]
        );

        Assert.True(quality.HasErrors);
        Assert.Equal(["timestamp"], quality.Findings[0].AffectedMetricCodes);
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new QualityFinding("invalid", (QualitySeverity)99, "Invalid severity")
        );
    }

    private static Activity ActivityWith(
        ActivityTimeRange range,
        IEnumerable<ActivitySegment> segments
    ) =>
        new(
            Guid.NewGuid(),
            ActivityType.Cycling,
            ManualSource(),
            range,
            new ActivitySummary([]),
            segments: segments
        );

    private static ActivityMetric MeasuredDistance(decimal value) =>
        new(
            "distance",
            value,
            MeasurementUnit.Meters,
            DataProvenance.Measured("record.distance", "m")
        );

    private static ActivitySource ManualSource() =>
        new("manual", "kairos", StartUtc);

    private static ActivityTimeRange Range(DateTimeOffset start, DateTimeOffset end) =>
        new(
            new ActivityTimestamp(start, "Europe/Vienna", TimeSpan.FromHours(1)),
            new ActivityTimestamp(end, "Europe/Vienna", TimeSpan.FromHours(1))
        );
}
