namespace Kairos.Domain.Activities;

public sealed record ActivityType
{
    public static ActivityType Cycling { get; } = new("cycling");
    public static ActivityType StrengthTraining { get; } = new("strength_training");
    public static ActivityType Rowing { get; } = new("rowing");

    public string Code { get; }

    private ActivityType(string code)
    {
        Code = DomainValue.Code(code, nameof(code));
    }

    public static ActivityType FromCode(string code) => new(code);

    public override string ToString() => Code;
}
