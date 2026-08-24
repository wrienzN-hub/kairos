namespace Kairos.Domain.Activities;

public sealed class ActivitySummary
{
    public IReadOnlyList<ActivityMetric> Metrics { get; }

    public ActivitySummary(IEnumerable<ActivityMetric> metrics)
    {
        Metrics = ActivityDataValidation.Metrics(metrics, nameof(metrics));
    }

    public ActivityMetric? Find(string code)
    {
        var normalized = DomainValue.Code(code, nameof(code));
        return Metrics.SingleOrDefault(metric => metric.Code == normalized);
    }
}

public sealed class ActivitySample
{
    public DateTimeOffset TimestampUtc { get; }
    public IReadOnlyList<ActivityMetric> Metrics { get; }

    public ActivitySample(DateTimeOffset timestampUtc, IEnumerable<ActivityMetric> metrics)
    {
        TimestampUtc = DomainValue.Utc(timestampUtc, nameof(timestampUtc));
        Metrics = ActivityDataValidation.Metrics(metrics, nameof(metrics));
        if (Metrics.Count == 0)
        {
            throw new ArgumentException("A sample must contain at least one metric.", nameof(metrics));
        }
    }
}

public sealed record SegmentType
{
    public static SegmentType Lap { get; } = new("lap");
    public static SegmentType Interval { get; } = new("interval");
    public static SegmentType StrengthSet { get; } = new("strength_set");

    public string Code { get; }

    private SegmentType(string code)
    {
        Code = DomainValue.Code(code, nameof(code));
    }

    public static SegmentType FromCode(string code) => new(code);
}

public sealed class ActivitySegment
{
    public int Index { get; }
    public SegmentType Type { get; }
    public ActivityTimeRange TimeRange { get; }
    public ActivitySummary Summary { get; }

    public ActivitySegment(
        int index,
        SegmentType type,
        ActivityTimeRange timeRange,
        ActivitySummary summary
    )
    {
        if (index < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Segment index must be non-negative.");
        }

        Index = index;
        Type = type ?? throw new ArgumentNullException(nameof(type));
        TimeRange = timeRange ?? throw new ArgumentNullException(nameof(timeRange));
        Summary = summary ?? throw new ArgumentNullException(nameof(summary));
    }
}

internal static class ActivityDataValidation
{
    public static IReadOnlyList<ActivityMetric> Metrics(
        IEnumerable<ActivityMetric>? metrics,
        string parameterName
    )
    {
        var copied = DomainValue.Copy(metrics, parameterName);
        if (copied.Any(metric => metric is null))
        {
            throw new ArgumentException("Metrics must not contain null values.", parameterName);
        }

        var duplicate = copied
            .GroupBy(metric => metric.Code, StringComparer.Ordinal)
            .FirstOrDefault(group => group.Count() > 1);
        if (duplicate is not null)
        {
            throw new ArgumentException(
                $"Metric code '{duplicate.Key}' occurs more than once.",
                parameterName
            );
        }

        return copied;
    }
}
