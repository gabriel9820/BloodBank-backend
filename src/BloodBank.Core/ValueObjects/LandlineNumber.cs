using System.Text.RegularExpressions;

namespace BloodBank.Core.ValueObjects;

public partial class LandlineNumber
{
    [GeneratedRegex(@"^\(\d{2}\) \d{4}-\d{4}$")]
    private static partial Regex LandlineNumberRegex();

    public string Value { get; init; }

    public LandlineNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !LandlineNumberRegex().IsMatch(value))
            throw new ArgumentException("Número de telefone fixo inválido. Formato esperado: (XX) XXXX-XXXX");

        Value = value;
    }

    public override bool Equals(object? obj) => obj is LandlineNumber other && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value;

    public static bool IsValid(string value)
        => !string.IsNullOrWhiteSpace(value) && LandlineNumberRegex().IsMatch(value);
}
