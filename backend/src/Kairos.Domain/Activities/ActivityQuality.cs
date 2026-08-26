namespace Kairos.Domain.Activities;

public enum QualitySeverity
{
    Information,
    Warning,
    Error,
}

public sealed class QualityFinding
{
    public string Code { get; }
    public QualitySeverity Severity { get; }
    public string Message { get; }
    public IReadOnlyList<string> AffectedMetricCodes { get; }

    public QualityFinding(
        string code,
        QualitySeverity severity,
        string message,
        IEnumerable<string>? affectedMetricCodes = null
    )
    {
        Code = DomainValue.Code(code, nameof(code));
        if (!Enum.IsDefined(severity))
        {
            throw new ArgumentOutOfRangeException(nameof(severity));
        }

        Severity = severity;
        Message = DomainValue.Required(message, nameof(message));
        AffectedMetricCodes = Array.AsReadOnly(
            (affectedMetricCodes ?? [])
                .Select(metricCode => DomainValue.Code(metricCode, nameof(affectedMetricCodes)))
                .Distinct(StringComparer.Ordinal)
                .ToArray()
        );
    }
}

public sealed class ActivityQuality
{
    public IReadOnlyList<QualityFinding> Findings { get; }
    public bool HasErrors => Findings.Any(finding => finding.Severity == QualitySeverity.Error);
    public bool IsAnalysisRestricted => Findings.Any(finding =>
        finding.Severity is QualitySeverity.Warning or QualitySeverity.Error
    );
    public string AnalysisStatus => HasErrors
        ? "blocked"
        : IsAnalysisRestricted
            ? "limited"
            : "eligible";

    public ActivityQuality(IEnumerable<QualityFinding>? findings = null)
    {
        var copied = (findings ?? []).ToArray();
        if (copied.Any(finding => finding is null))
        {
            throw new ArgumentException("Quality findings must not contain null values.", nameof(findings));
        }

        Findings = Array.AsReadOnly(copied);
    }
}
