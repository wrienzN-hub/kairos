namespace Kairos.Domain.Activities;

public sealed class Activity
{
    public Guid Id { get; }
    public ActivityType Type { get; }
    public ActivitySource Source { get; }
    public ActivityTimeRange TimeRange { get; }
    public ActivitySummary Summary { get; }
    public IReadOnlyList<ActivitySample> Samples { get; }
    public IReadOnlyList<ActivitySegment> Segments { get; }
    public ActivityQuality Quality { get; }

    public Activity(
        Guid id,
        ActivityType type,
        ActivitySource source,
        ActivityTimeRange timeRange,
        ActivitySummary summary,
        IEnumerable<ActivitySample>? samples = null,
        IEnumerable<ActivitySegment>? segments = null,
        ActivityQuality? quality = null
    )
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Activity identifier must not be empty.", nameof(id));
        }

        Id = id;
        Type = type ?? throw new ArgumentNullException(nameof(type));
        Source = source ?? throw new ArgumentNullException(nameof(source));
        TimeRange = timeRange ?? throw new ArgumentNullException(nameof(timeRange));
        Summary = summary ?? throw new ArgumentNullException(nameof(summary));
        Samples = ValidateSamples(samples ?? [], timeRange);
        Segments = ValidateSegments(segments ?? [], timeRange);
        Quality = quality ?? new ActivityQuality();
    }

    private static IReadOnlyList<ActivitySample> ValidateSamples(
        IEnumerable<ActivitySample> samples,
        ActivityTimeRange activityRange
    )
    {
        var copied = samples.ToArray();
        for (var index = 0; index < copied.Length; index++)
        {
            var sample = copied[index]
                ?? throw new ArgumentException("Samples must not contain null values.", nameof(samples));
            if (!activityRange.Contains(sample.TimestampUtc))
            {
                throw new ArgumentException(
                    "Every sample timestamp must be inside the activity time range.",
                    nameof(samples)
                );
            }

            if (index > 0 && copied[index - 1].TimestampUtc >= sample.TimestampUtc)
            {
                throw new ArgumentException(
                    "Samples must be strictly ordered by UTC timestamp.",
                    nameof(samples)
                );
            }
        }

        return Array.AsReadOnly(copied);
    }

    private static IReadOnlyList<ActivitySegment> ValidateSegments(
        IEnumerable<ActivitySegment> segments,
        ActivityTimeRange activityRange
    )
    {
        var copied = segments.ToArray();
        if (copied.Any(segment => segment is null))
        {
            throw new ArgumentException("Segments must not contain null values.", nameof(segments));
        }

        if (copied.Select(segment => segment.Index).Distinct().Count() != copied.Length)
        {
            throw new ArgumentException("Segment indexes must be unique.", nameof(segments));
        }

        if (copied.Any(segment => !activityRange.Contains(segment.TimeRange)))
        {
            throw new ArgumentException(
                "Every segment must be inside the activity time range.",
                nameof(segments)
            );
        }

        return Array.AsReadOnly(copied.OrderBy(segment => segment.Index).ToArray());
    }
}
