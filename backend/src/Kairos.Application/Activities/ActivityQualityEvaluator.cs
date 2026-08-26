using Kairos.Domain.Activities;

namespace Kairos.Application.Activities;

public sealed class ActivityQualityEvaluator
{
    public Activity Evaluate(Activity activity)
    {
        ArgumentNullException.ThrowIfNull(activity);
        var findings = activity.Quality.Findings.ToList();
        var metrics = activity.Samples.SelectMany(sample => sample.Metrics).ToArray();
        var metricCodes = metrics
            .Select(metric => metric.Code)
            .ToHashSet(StringComparer.Ordinal);

        AddMissingStreamFindings(findings, metricCodes);
        AddImplausibleValueFindings(findings, activity, metrics);

        return new Activity(
            activity.Id,
            activity.Type,
            activity.Source,
            activity.TimeRange,
            activity.Summary,
            activity.Samples,
            activity.Segments,
            new ActivityQuality(
                findings
                    .GroupBy(finding => finding.Code, StringComparer.Ordinal)
                    .Select(group => group.First())
            )
        );
    }

    private static void AddMissingStreamFindings(
        ICollection<QualityFinding> findings,
        IReadOnlySet<string> metricCodes
    )
    {
        AddMissing(
            findings,
            metricCodes,
            "power",
            "missing_power_stream",
            QualitySeverity.Warning,
            "Die Aktivität enthält keine Leistungsdaten. Leistungsbasierte Analysen sind eingeschränkt."
        );
        AddMissing(
            findings,
            metricCodes,
            "cadence",
            "missing_cadence_stream",
            QualitySeverity.Warning,
            "Die Aktivität enthält keine Trittfrequenzdaten. Trittfrequenzanalysen sind nicht verfügbar."
        );
        AddMissing(
            findings,
            metricCodes,
            "heart_rate",
            "missing_heart_rate_stream",
            QualitySeverity.Warning,
            "Die Aktivität enthält keine Herzfrequenzdaten. Belastungsanalysen sind eingeschränkt."
        );

        if (!metricCodes.Contains("latitude") || !metricCodes.Contains("longitude"))
        {
            findings.Add(
                new QualityFinding(
                    "missing_position_stream",
                    QualitySeverity.Warning,
                    "Die Aktivität enthält keine vollständigen Positionsdaten. Streckenanalysen sind nicht verfügbar.",
                    ["latitude", "longitude"]
                )
            );
        }
    }

    private static void AddMissing(
        ICollection<QualityFinding> findings,
        IReadOnlySet<string> metricCodes,
        string metricCode,
        string findingCode,
        QualitySeverity severity,
        string message
    )
    {
        if (!metricCodes.Contains(metricCode))
        {
            findings.Add(new QualityFinding(findingCode, severity, message, [metricCode]));
        }
    }

    private static void AddImplausibleValueFindings(
        ICollection<QualityFinding> findings,
        Activity activity,
        IEnumerable<ActivityMetric> sampleMetrics
    )
    {
        var metrics = sampleMetrics.ToArray();
        AddOutsideRange(
            findings,
            metrics,
            "speed",
            0,
            50,
            "implausible_speed",
            "Mindestens ein Geschwindigkeitswert liegt außerhalb des unterstützten Bereichs von 0 bis 50 m/s."
        );
        AddOutsideRange(
            findings,
            metrics,
            "heart_rate",
            25,
            240,
            "implausible_heart_rate",
            "Mindestens ein Herzfrequenzwert liegt außerhalb des unterstützten Bereichs von 25 bis 240 bpm."
        );
        AddOutsideRange(
            findings,
            metrics,
            "cadence",
            0,
            250,
            "implausible_cadence",
            "Mindestens ein Trittfrequenzwert liegt außerhalb des unterstützten Bereichs von 0 bis 250 rpm."
        );
        AddOutsideRange(
            findings,
            metrics,
            "power",
            0,
            2500,
            "implausible_power",
            "Mindestens ein Leistungswert liegt außerhalb des unterstützten Bereichs von 0 bis 2500 Watt."
        );
        AddOutsideRange(
            findings,
            metrics,
            "latitude",
            -90,
            90,
            "implausible_latitude",
            "Mindestens ein Breitengrad liegt außerhalb des gültigen Bereichs."
        );
        AddOutsideRange(
            findings,
            metrics,
            "longitude",
            -180,
            180,
            "implausible_longitude",
            "Mindestens ein Längengrad liegt außerhalb des gültigen Bereichs."
        );
        AddOutsideRange(
            findings,
            metrics,
            "distance",
            0,
            decimal.MaxValue,
            "negative_distance",
            "Mindestens ein Distanzwert ist negativ."
        );

        if (activity.TimeRange.Duration <= TimeSpan.Zero)
        {
            findings.Add(
                new QualityFinding(
                    "non_positive_duration",
                    QualitySeverity.Error,
                    "Die Aktivität besitzt keine positive Dauer und kann nicht für Belastungsanalysen verwendet werden.",
                    ["duration"]
                )
            );
        }
    }

    private static void AddOutsideRange(
        ICollection<QualityFinding> findings,
        IEnumerable<ActivityMetric> metrics,
        string metricCode,
        decimal minimum,
        decimal maximum,
        string findingCode,
        string message
    )
    {
        if (
            metrics.Any(metric =>
                metric.Code == metricCode
                && (metric.Value < minimum || metric.Value > maximum)
            )
        )
        {
            findings.Add(
                new QualityFinding(
                    findingCode,
                    QualitySeverity.Error,
                    message,
                    [metricCode]
                )
            );
        }
    }
}
