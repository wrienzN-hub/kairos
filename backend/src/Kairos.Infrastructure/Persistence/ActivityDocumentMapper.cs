using System.Text.Json;
using System.Text.Json.Serialization;
using Kairos.Domain.Activities;

namespace Kairos.Infrastructure.Persistence;

internal static class ActivityDocumentMapper
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
    };

    public static string Serialize(Activity activity)
    {
        ArgumentNullException.ThrowIfNull(activity);
        return JsonSerializer.Serialize(ActivityDocument.From(activity), SerializerOptions);
    }

    public static Activity Deserialize(string json)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(json);
        var document = JsonSerializer.Deserialize<ActivityDocument>(json, SerializerOptions)
            ?? throw new InvalidDataException("The stored activity document is empty.");
        return document.ToDomain();
    }

    private sealed record ActivityDocument(
        int SchemaVersion,
        Guid Id,
        string Type,
        SourceDocument Source,
        TimeRangeDocument TimeRange,
        MetricDocument[] Summary,
        SampleDocument[] Samples,
        SegmentDocument[] Segments,
        FindingDocument[] QualityFindings
    )
    {
        public static ActivityDocument From(Activity activity) =>
            new(
                1,
                activity.Id,
                activity.Type.Code,
                SourceDocument.From(activity.Source),
                TimeRangeDocument.From(activity.TimeRange),
                activity.Summary.Metrics.Select(MetricDocument.From).ToArray(),
                activity.Samples.Select(SampleDocument.From).ToArray(),
                activity.Segments.Select(SegmentDocument.From).ToArray(),
                activity.Quality.Findings.Select(FindingDocument.From).ToArray()
            );

        public Activity ToDomain()
        {
            if (SchemaVersion != 1)
            {
                throw new InvalidDataException(
                    $"Activity document schema version {SchemaVersion} is not supported."
                );
            }

            return new Activity(
                Id,
                ActivityType.FromCode(Type),
                Source.ToDomain(),
                TimeRange.ToDomain(),
                new ActivitySummary(Summary.Select(metric => metric.ToDomain())),
                Samples.Select(sample => sample.ToDomain()),
                Segments.Select(segment => segment.ToDomain()),
                new ActivityQuality(QualityFindings.Select(finding => finding.ToDomain()))
            );
        }
    }

    private sealed record SourceDocument(
        string Kind,
        string Provider,
        string? ExternalIdentifier,
        string? OriginalFileName,
        string? ContentHashSha256,
        DateTimeOffset ImportedAtUtc
    )
    {
        public static SourceDocument From(ActivitySource source) =>
            new(
                source.Kind,
                source.Provider,
                source.ExternalIdentifier,
                source.OriginalFileName,
                source.ContentHashSha256,
                source.ImportedAtUtc
            );

        public ActivitySource ToDomain() =>
            new(
                Kind,
                Provider,
                ImportedAtUtc,
                ExternalIdentifier,
                OriginalFileName,
                ContentHashSha256
            );
    }

    private sealed record TimestampDocument(
        DateTimeOffset InstantUtc,
        string TimeZoneId,
        long ObservedUtcOffsetTicks
    )
    {
        public static TimestampDocument From(ActivityTimestamp timestamp) =>
            new(
                timestamp.InstantUtc,
                timestamp.TimeZoneId,
                timestamp.ObservedUtcOffset.Ticks
            );

        public ActivityTimestamp ToDomain() =>
            new(InstantUtc, TimeZoneId, TimeSpan.FromTicks(ObservedUtcOffsetTicks));
    }

    private sealed record TimeRangeDocument(TimestampDocument Start, TimestampDocument End)
    {
        public static TimeRangeDocument From(ActivityTimeRange range) =>
            new(TimestampDocument.From(range.Start), TimestampDocument.From(range.End));

        public ActivityTimeRange ToDomain() => new(Start.ToDomain(), End.ToDomain());
    }

    private sealed record UnitDocument(string Code, string Symbol)
    {
        public static UnitDocument From(MeasurementUnit unit) => new(unit.Code, unit.Symbol);

        public MeasurementUnit ToDomain() => MeasurementUnit.From(Code, Symbol);
    }

    private sealed record DerivationDocument(
        string Method,
        string Version,
        string[] InputMetricCodes
    )
    {
        public static DerivationDocument From(Derivation derivation) =>
            new(derivation.Method, derivation.Version, derivation.InputMetricCodes.ToArray());
    }

    private sealed record ProvenanceDocument(
        DataOrigin Origin,
        string? SourceField,
        string? SourceUnit,
        DerivationDocument? Derivation
    )
    {
        public static ProvenanceDocument From(DataProvenance provenance) =>
            new(
                provenance.Origin,
                provenance.SourceField,
                provenance.SourceUnit,
                provenance.Derivation is null
                    ? null
                    : DerivationDocument.From(provenance.Derivation)
            );

        public DataProvenance ToDomain() =>
            Origin switch
            {
                DataOrigin.Measured => DataProvenance.Measured(SourceField!, SourceUnit),
                DataOrigin.ImportedSummary => DataProvenance.ImportedSummary(
                    SourceField!,
                    SourceUnit
                ),
                DataOrigin.UserEntered => DataProvenance.UserEntered(),
                DataOrigin.Derived when Derivation is not null => DataProvenance.Derived(
                    Derivation.Method,
                    Derivation.Version,
                    Derivation.InputMetricCodes
                ),
                _ => throw new InvalidDataException("The stored metric provenance is invalid."),
            };
    }

    private sealed record MetricDocument(
        string Code,
        decimal Value,
        UnitDocument Unit,
        ProvenanceDocument Provenance
    )
    {
        public static MetricDocument From(ActivityMetric metric) =>
            new(
                metric.Code,
                metric.Value,
                UnitDocument.From(metric.Unit),
                ProvenanceDocument.From(metric.Provenance)
            );

        public ActivityMetric ToDomain() =>
            new(Code, Value, Unit.ToDomain(), Provenance.ToDomain());
    }

    private sealed record SampleDocument(DateTimeOffset TimestampUtc, MetricDocument[] Metrics)
    {
        public static SampleDocument From(ActivitySample sample) =>
            new(
                sample.TimestampUtc,
                sample.Metrics.Select(MetricDocument.From).ToArray()
            );

        public ActivitySample ToDomain() =>
            new(TimestampUtc, Metrics.Select(metric => metric.ToDomain()));
    }

    private sealed record SegmentDocument(
        int Index,
        string Type,
        TimeRangeDocument TimeRange,
        MetricDocument[] Summary
    )
    {
        public static SegmentDocument From(ActivitySegment segment) =>
            new(
                segment.Index,
                segment.Type.Code,
                TimeRangeDocument.From(segment.TimeRange),
                segment.Summary.Metrics.Select(MetricDocument.From).ToArray()
            );

        public ActivitySegment ToDomain() =>
            new(
                Index,
                SegmentType.FromCode(Type),
                TimeRange.ToDomain(),
                new ActivitySummary(Summary.Select(metric => metric.ToDomain()))
            );
    }

    private sealed record FindingDocument(
        string Code,
        QualitySeverity Severity,
        string Message,
        string[] AffectedMetricCodes
    )
    {
        public static FindingDocument From(QualityFinding finding) =>
            new(
                finding.Code,
                finding.Severity,
                finding.Message,
                finding.AffectedMetricCodes.ToArray()
            );

        public QualityFinding ToDomain() =>
            new(Code, Severity, Message, AffectedMetricCodes);
    }
}
