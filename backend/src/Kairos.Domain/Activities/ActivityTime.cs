namespace Kairos.Domain.Activities;

public sealed record ActivityTimestamp
{
    private static readonly TimeSpan MaximumUtcOffset = TimeSpan.FromHours(14);

    public DateTimeOffset InstantUtc { get; }
    public string TimeZoneId { get; }
    public TimeSpan ObservedUtcOffset { get; }

    public ActivityTimestamp(
        DateTimeOffset instantUtc,
        string timeZoneId,
        TimeSpan observedUtcOffset
    )
    {
        InstantUtc = DomainValue.Utc(instantUtc, nameof(instantUtc));
        TimeZoneId = DomainValue.Required(timeZoneId, nameof(timeZoneId));

        if (
            observedUtcOffset < -MaximumUtcOffset
            || observedUtcOffset > MaximumUtcOffset
            || observedUtcOffset.Ticks % TimeSpan.TicksPerMinute != 0
        )
        {
            throw new ArgumentOutOfRangeException(
                nameof(observedUtcOffset),
                "Observed UTC offset must use whole minutes and be between -14 and +14 hours."
            );
        }

        ObservedUtcOffset = observedUtcOffset;
    }

    public DateTimeOffset ToObservedLocalTime() => InstantUtc.ToOffset(ObservedUtcOffset);
}

public sealed record ActivityTimeRange
{
    public ActivityTimestamp Start { get; }
    public ActivityTimestamp End { get; }
    public TimeSpan Duration => End.InstantUtc - Start.InstantUtc;

    public ActivityTimeRange(ActivityTimestamp start, ActivityTimestamp end)
    {
        Start = start ?? throw new ArgumentNullException(nameof(start));
        End = end ?? throw new ArgumentNullException(nameof(end));

        if (end.InstantUtc < start.InstantUtc)
        {
            throw new ArgumentException("Activity end must not precede its start.", nameof(end));
        }
    }

    public bool Contains(DateTimeOffset timestampUtc)
    {
        var normalized = DomainValue.Utc(timestampUtc, nameof(timestampUtc));
        return normalized >= Start.InstantUtc && normalized <= End.InstantUtc;
    }

    public bool Contains(ActivityTimeRange other)
    {
        ArgumentNullException.ThrowIfNull(other);
        return Contains(other.Start.InstantUtc) && Contains(other.End.InstantUtc);
    }
}
