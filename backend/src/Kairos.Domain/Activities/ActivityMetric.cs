namespace Kairos.Domain.Activities;

public sealed record ActivityMetric
{
    public string Code { get; }
    public decimal Value { get; }
    public MeasurementUnit Unit { get; }
    public DataProvenance Provenance { get; }

    public ActivityMetric(
        string code,
        decimal value,
        MeasurementUnit unit,
        DataProvenance provenance
    )
    {
        Code = DomainValue.Code(code, nameof(code));
        Value = value;
        Unit = unit ?? throw new ArgumentNullException(nameof(unit));
        Provenance = provenance ?? throw new ArgumentNullException(nameof(provenance));
    }
}
