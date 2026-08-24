using Dynastream.Fit;
using Kairos.Application.ActivityImports;
using Kairos.Domain.Activities;
using DomainActivity = Kairos.Domain.Activities.Activity;
using DomainActivityType = Kairos.Domain.Activities.ActivityType;

namespace Kairos.Infrastructure.ActivityImports;

public sealed class GarminFitActivityParser : IFitActivityParser
{
    private const double SemicirclesPerDegree = 2147483648d / 180d;
    private static readonly MeasurementUnit Degrees = MeasurementUnit.From("degrees", "°");

    public DomainActivity Parse(FitActivityFile file)
    {
        ArgumentNullException.ThrowIfNull(file);
        if (file.Content is null || file.Content.Length == 0)
        {
            throw new FitParseException("empty_fit_file", "The FIT file is empty.");
        }

        try
        {
            var decoded = Decode(file.Content);
            EnsureActivityFile(decoded.FileId);

            var samples = CreateSamples(decoded.Records);
            var session = decoded.Sessions.OrderBy(SessionStart).FirstOrDefault();
            var start = session is null ? samples.FirstOrDefault()?.TimestampUtc : ToUtc(session.GetStartTime());
            var end = session is null ? samples.LastOrDefault()?.TimestampUtc : ToUtc(session.GetTimestamp());

            start ??= samples.FirstOrDefault()?.TimestampUtc;
            end ??= samples.LastOrDefault()?.TimestampUtc;
            if (start is null || end is null)
            {
                throw new FitParseException(
                    "missing_activity_time",
                    "The FIT activity has no usable start and end timestamp."
                );
            }

            var timeRange = TimeRange(start.Value, end.Value);
            var boundedSamples = samples
                .Where(sample => timeRange.Contains(sample.TimestampUtc))
                .ToArray();

            return new DomainActivity(
                file.UploadId,
                MapActivityType(session?.GetSport(), session?.GetSubSport()),
                new ActivitySource(
                    "fit_file",
                    "file_import",
                    file.UploadedAtUtc,
                    externalIdentifier: file.UploadId.ToString(),
                    originalFileName: file.OriginalFileName,
                    contentHashSha256: file.Sha256
                ),
                timeRange,
                new ActivitySummary(session is null ? [] : SessionMetrics(session)),
                boundedSamples,
                CreateSegments(decoded.Laps, timeRange)
            );
        }
        catch (FitParseException)
        {
            throw;
        }
        catch (Exception exception)
        {
            throw new FitParseException(
                "invalid_fit_structure",
                "The FIT file could not be decoded as an activity.",
                exception
            );
        }
    }

    private static DecodedFit Decode(byte[] content)
    {
        var decoded = new DecodedFit();
        var decoder = new Decode();
        var broadcaster = new MesgBroadcaster();

        decoder.MesgEvent += broadcaster.OnMesg;
        decoder.MesgDefinitionEvent += broadcaster.OnMesgDefinition;
        broadcaster.FileIdMesgEvent += (_, eventArgs) => decoded.FileId = (FileIdMesg)eventArgs.mesg;
        broadcaster.SessionMesgEvent += (_, eventArgs) => decoded.Sessions.Add((SessionMesg)eventArgs.mesg);
        broadcaster.LapMesgEvent += (_, eventArgs) => decoded.Laps.Add((LapMesg)eventArgs.mesg);
        broadcaster.RecordMesgEvent += (_, eventArgs) => decoded.Records.Add((RecordMesg)eventArgs.mesg);

        using var stream = new MemoryStream(content, writable: false);
        if (!decoder.Read(stream))
        {
            throw new FitParseException("invalid_fit_structure", "The FIT decoder rejected the file.");
        }

        return decoded;
    }

    private static void EnsureActivityFile(FileIdMesg? fileId)
    {
        if (fileId?.GetType() != Dynastream.Fit.File.Activity)
        {
            throw new FitParseException(
                "unsupported_fit_file_type",
                "Only FIT activity files are supported."
            );
        }
    }

    private static DomainActivityType MapActivityType(Sport? sport, SubSport? subSport) =>
        (sport, subSport) switch
        {
            (Sport.Cycling, _) => DomainActivityType.Cycling,
            (_, SubSport.StrengthTraining) => DomainActivityType.StrengthTraining,
            (Sport.Rowing, _) or (_, SubSport.IndoorRowing) => DomainActivityType.Rowing,
            _ => DomainActivityType.FromCode((sport?.ToString() ?? "unknown").ToLowerInvariant()),
        };

    private static IReadOnlyList<ActivitySample> CreateSamples(IEnumerable<RecordMesg> records)
    {
        return records
            .Select(record => (Timestamp: ToUtc(record.GetTimestamp()), Metrics: RecordMetrics(record)))
            .Where(entry => entry.Timestamp is not null && entry.Metrics.Count > 0)
            .GroupBy(entry => entry.Timestamp!.Value)
            .OrderBy(group => group.Key)
            .Select(group => new ActivitySample(
                group.Key,
                group.SelectMany(entry => entry.Metrics)
                    .GroupBy(metric => metric.Code, StringComparer.Ordinal)
                    .Select(metrics => metrics.Last())
            ))
            .ToArray();
    }

    private static IReadOnlyList<ActivitySegment> CreateSegments(
        IEnumerable<LapMesg> laps,
        ActivityTimeRange activityRange
    )
    {
        return laps
            .Select(lap => new
            {
                Lap = lap,
                Start = ToUtc(lap.GetStartTime()),
                End = ToUtc(lap.GetTimestamp()),
            })
            .Where(entry => entry.Start is not null && entry.End is not null)
            .Where(entry => entry.Start <= entry.End)
            .Where(entry => activityRange.Contains(entry.Start!.Value) && activityRange.Contains(entry.End!.Value))
            .OrderBy(entry => entry.Start)
            .Select((entry, index) => new ActivitySegment(
                index,
                SegmentType.Lap,
                TimeRange(entry.Start!.Value, entry.End!.Value),
                new ActivitySummary(LapMetrics(entry.Lap))
            ))
            .ToArray();
    }

    private static IReadOnlyList<ActivityMetric> SessionMetrics(SessionMesg session)
    {
        var metrics = new List<ActivityMetric>();
        Add(metrics, "duration", session.GetTotalTimerTime(), MeasurementUnit.Seconds, "session.total_timer_time", "s", true);
        Add(metrics, "distance", session.GetTotalDistance(), MeasurementUnit.Meters, "session.total_distance", "m", true);
        Add(metrics, "average_speed", session.GetAvgSpeed(), MeasurementUnit.MetersPerSecond, "session.avg_speed", "m/s", true);
        Add(metrics, "maximum_speed", session.GetMaxSpeed(), MeasurementUnit.MetersPerSecond, "session.max_speed", "m/s", true);
        Add(metrics, "average_heart_rate", session.GetAvgHeartRate(), MeasurementUnit.BeatsPerMinute, "session.avg_heart_rate", "bpm", true);
        Add(metrics, "maximum_heart_rate", session.GetMaxHeartRate(), MeasurementUnit.BeatsPerMinute, "session.max_heart_rate", "bpm", true);
        Add(metrics, "average_cadence", session.GetAvgCadence(), MeasurementUnit.RevolutionsPerMinute, "session.avg_cadence", "rpm", true);
        Add(metrics, "maximum_cadence", session.GetMaxCadence(), MeasurementUnit.RevolutionsPerMinute, "session.max_cadence", "rpm", true);
        Add(metrics, "average_power", session.GetAvgPower(), MeasurementUnit.Watts, "session.avg_power", "W", true);
        Add(metrics, "maximum_power", session.GetMaxPower(), MeasurementUnit.Watts, "session.max_power", "W", true);
        Add(metrics, "calories", session.GetTotalCalories(), MeasurementUnit.Count, "session.total_calories", "kcal", true);
        return metrics;
    }

    private static IReadOnlyList<ActivityMetric> LapMetrics(LapMesg lap)
    {
        var metrics = new List<ActivityMetric>();
        Add(metrics, "duration", lap.GetTotalTimerTime(), MeasurementUnit.Seconds, "lap.total_timer_time", "s", true);
        Add(metrics, "distance", lap.GetTotalDistance(), MeasurementUnit.Meters, "lap.total_distance", "m", true);
        Add(metrics, "average_speed", lap.GetAvgSpeed(), MeasurementUnit.MetersPerSecond, "lap.avg_speed", "m/s", true);
        Add(metrics, "maximum_speed", lap.GetMaxSpeed(), MeasurementUnit.MetersPerSecond, "lap.max_speed", "m/s", true);
        Add(metrics, "average_heart_rate", lap.GetAvgHeartRate(), MeasurementUnit.BeatsPerMinute, "lap.avg_heart_rate", "bpm", true);
        Add(metrics, "maximum_heart_rate", lap.GetMaxHeartRate(), MeasurementUnit.BeatsPerMinute, "lap.max_heart_rate", "bpm", true);
        Add(metrics, "average_cadence", lap.GetAvgCadence(), MeasurementUnit.RevolutionsPerMinute, "lap.avg_cadence", "rpm", true);
        Add(metrics, "maximum_cadence", lap.GetMaxCadence(), MeasurementUnit.RevolutionsPerMinute, "lap.max_cadence", "rpm", true);
        Add(metrics, "average_power", lap.GetAvgPower(), MeasurementUnit.Watts, "lap.avg_power", "W", true);
        Add(metrics, "maximum_power", lap.GetMaxPower(), MeasurementUnit.Watts, "lap.max_power", "W", true);
        return metrics;
    }

    private static IReadOnlyList<ActivityMetric> RecordMetrics(RecordMesg record)
    {
        var metrics = new List<ActivityMetric>();
        Add(metrics, "latitude", ToDegrees(record.GetPositionLat()), Degrees, "record.position_lat", "semicircles");
        Add(metrics, "longitude", ToDegrees(record.GetPositionLong()), Degrees, "record.position_long", "semicircles");
        Add(metrics, "altitude", record.GetAltitude(), MeasurementUnit.Meters, "record.altitude", "m");
        Add(metrics, "distance", record.GetDistance(), MeasurementUnit.Meters, "record.distance", "m");
        Add(metrics, "speed", record.GetSpeed(), MeasurementUnit.MetersPerSecond, "record.speed", "m/s");
        Add(metrics, "heart_rate", record.GetHeartRate(), MeasurementUnit.BeatsPerMinute, "record.heart_rate", "bpm");
        Add(metrics, "cadence", record.GetCadence(), MeasurementUnit.RevolutionsPerMinute, "record.cadence", "rpm");
        Add(metrics, "power", record.GetPower(), MeasurementUnit.Watts, "record.power", "W");
        Add(metrics, "temperature", record.GetTemperature(), MeasurementUnit.Celsius, "record.temperature", "°C");
        return metrics;
    }

    private static void Add(
        ICollection<ActivityMetric> metrics,
        string code,
        IConvertible? value,
        MeasurementUnit unit,
        string sourceField,
        string sourceUnit,
        bool summary = false
    )
    {
        if (value is null)
        {
            return;
        }

        metrics.Add(new ActivityMetric(
            code,
            Convert.ToDecimal(value, System.Globalization.CultureInfo.InvariantCulture),
            unit,
            summary
                ? DataProvenance.ImportedSummary(sourceField, sourceUnit)
                : DataProvenance.Measured(sourceField, sourceUnit)
        ));
    }

    private static double? ToDegrees(int? semicircles) => semicircles / SemicirclesPerDegree;

    private static DateTimeOffset? ToUtc(Dynastream.Fit.DateTime? timestamp)
    {
        if (timestamp is null)
        {
            return null;
        }

        return new DateTimeOffset(System.DateTime.SpecifyKind(timestamp.GetDateTime(), DateTimeKind.Utc));
    }

    private static DateTimeOffset? SessionStart(SessionMesg session) => ToUtc(session.GetStartTime());

    private static ActivityTimeRange TimeRange(DateTimeOffset start, DateTimeOffset end) =>
        new(
            new ActivityTimestamp(start, "Etc/UTC", TimeSpan.Zero),
            new ActivityTimestamp(end, "Etc/UTC", TimeSpan.Zero)
        );

    private sealed class DecodedFit
    {
        public FileIdMesg? FileId { get; set; }
        public List<SessionMesg> Sessions { get; } = [];
        public List<LapMesg> Laps { get; } = [];
        public List<RecordMesg> Records { get; } = [];
    }
}
