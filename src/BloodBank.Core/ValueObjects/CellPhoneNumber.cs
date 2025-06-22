using System.Text.RegularExpressions;

namespace BloodBank.Core.ValueObjects;

public partial class CellPhoneNumber
{
    [GeneratedRegex(@"^\(\d{2}\) 9\d{4}-\d{4}$")]
    private static partial Regex CellPhoneNumberRegex();

    public string Value { get; init; }

    public CellPhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !CellPhoneNumberRegex().IsMatch(value))
            throw new ArgumentException("Número de celular inválido. Formato esperado: (XX) 9XXXX-XXXX");

        Value = value;
    }

    public override bool Equals(object? obj) => obj is CellPhoneNumber other && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value;

    public static bool IsValid(string value)
        => !string.IsNullOrWhiteSpace(value) && CellPhoneNumberRegex().IsMatch(value);
}
