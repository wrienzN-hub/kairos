namespace Kairos.Domain.Activities;

public enum DataOrigin
{
    Measured,
    ImportedSummary,
    UserEntered,
    Derived,
}

public sealed record Derivation
{
    public string Method { get; }
    public string Version { get; }
    public IReadOnlyList<string> InputMetricCodes { get; }

    public Derivation(string method, string version, IEnumerable<string> inputMetricCodes)
    {
        Method = DomainValue.Code(method, nameof(method));
        Version = DomainValue.Required(version, nameof(version));
        InputMetricCodes = Array.AsReadOnly(
            DomainValue.Copy(inputMetricCodes, nameof(inputMetricCodes))
                .Select(code => DomainValue.Code(code, nameof(inputMetricCodes)))
                .Distinct(StringComparer.Ordinal)
                .ToArray()
        );

        if (InputMetricCodes.Count == 0)
        {
            throw new ArgumentException(
                "A derived value must name at least one input metric.",
                nameof(inputMetricCodes)
            );
        }
    }
}

public sealed record DataProvenance
{
    public DataOrigin Origin { get; }
    public string? SourceField { get; }
    public string? SourceUnit { get; }
    public Derivation? Derivation { get; }
    public bool IsDerived => Origin == DataOrigin.Derived;

    private DataProvenance(
        DataOrigin origin,
        string? sourceField,
        string? sourceUnit,
        Derivation? derivation
    )
    {
        Origin = origin;
        SourceField = sourceField;
        SourceUnit = sourceUnit;
        Derivation = derivation;
    }

    public static DataProvenance Measured(string sourceField, string? sourceUnit = null) =>
        FromSource(DataOrigin.Measured, sourceField, sourceUnit);

    public static DataProvenance ImportedSummary(
        string sourceField,
        string? sourceUnit = null
    ) => FromSource(DataOrigin.ImportedSummary, sourceField, sourceUnit);

    public static DataProvenance UserEntered() =>
        new(DataOrigin.UserEntered, null, null, null);

    public static DataProvenance Derived(
        string method,
        string version,
        IEnumerable<string> inputMetricCodes
    ) => new(DataOrigin.Derived, null, null, new Derivation(method, version, inputMetricCodes));

    private static DataProvenance FromSource(
        DataOrigin origin,
        string sourceField,
        string? sourceUnit
    ) =>
        new(
            origin,
            DomainValue.Required(sourceField, nameof(sourceField)),
            string.IsNullOrWhiteSpace(sourceUnit) ? null : sourceUnit.Trim(),
            null
        );
}
