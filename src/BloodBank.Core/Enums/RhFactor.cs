namespace BloodBank.Core.Enums;

public enum RhFactor
{
    Positive,
    Negative
}

public static class RhFactorExtensions
{
    public static string ToDisplayString(this RhFactor rhFactor)
    {
        return rhFactor == RhFactor.Positive ? "+" : "-";
    }
}
