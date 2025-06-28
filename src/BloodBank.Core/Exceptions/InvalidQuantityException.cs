namespace BloodBank.Core.Exceptions;

public class InvalidQuantityException : Exception
{
    public InvalidQuantityException()
        : base("A quantidade deve ser maior que zero.") { }
}
