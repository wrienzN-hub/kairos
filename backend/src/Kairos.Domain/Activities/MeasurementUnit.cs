namespace Kairos.Domain.Activities;

public sealed record MeasurementUnit
{
    public static MeasurementUnit Seconds { get; } = new("seconds", "s");
    public static MeasurementUnit Meters { get; } = new("meters", "m");
    public static MeasurementUnit MetersPerSecond { get; } = new("meters_per_second", "m/s");
    public static MeasurementUnit BeatsPerMinute { get; } = new("beats_per_minute", "bpm");
    public static MeasurementUnit RevolutionsPerMinute { get; } = new(
        "revolutions_per_minute",
        "rpm"
    );
    public static MeasurementUnit Watts { get; } = new("watts", "W");
    public static MeasurementUnit Kilograms { get; } = new("kilograms", "kg");
    public static MeasurementUnit Count { get; } = new("count", "count");
    public static MeasurementUnit Celsius { get; } = new("celsius", "°C");

    public string Code { get; }
    public string Symbol { get; }

    private MeasurementUnit(string code, string symbol)
    {
        Code = DomainValue.Code(code, nameof(code));
        Symbol = DomainValue.Required(symbol, nameof(symbol));
    }

    public static MeasurementUnit From(string code, string symbol) => new(code, symbol);

    public override string ToString() => Symbol;
}
