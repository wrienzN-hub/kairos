namespace Kairos.Domain.Activities;

internal static class DomainValue
{
    public static string Required(string? value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Value must not be empty.", parameterName);
        }

        return value.Trim();
    }

    public static string Code(string? value, string parameterName)
    {
        var normalized = Required(value, parameterName).ToLowerInvariant();
        if (
            !char.IsAsciiLetter(normalized[0])
            || normalized.Any(character =>
                !char.IsAsciiLetterOrDigit(character) && character is not '_' and not '-' and not '.'
            )
        )
        {
            throw new ArgumentException(
                "Code must start with an ASCII letter and contain only letters, digits, '.', '_' or '-'.",
                parameterName
            );
        }

        return normalized;
    }

    public static DateTimeOffset Utc(DateTimeOffset value, string parameterName)
    {
        if (value.Offset != TimeSpan.Zero)
        {
            throw new ArgumentException("Timestamp must be normalized to UTC.", parameterName);
        }

        return value;
    }

    public static IReadOnlyList<T> Copy<T>(IEnumerable<T>? values, string parameterName)
    {
        ArgumentNullException.ThrowIfNull(values, parameterName);
        return Array.AsReadOnly(values.ToArray());
    }
}
