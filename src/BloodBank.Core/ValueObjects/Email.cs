using System.Text.RegularExpressions;

namespace BloodBank.Core.ValueObjects;

public partial class Email
{
    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial Regex EmailRegex();

    public string Value { get; init; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !EmailRegex().IsMatch(value))
            throw new ArgumentException("E-mail inválido.");

        Value = value;
    }

    public override bool Equals(object? obj) => obj is Email other && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value;
}
