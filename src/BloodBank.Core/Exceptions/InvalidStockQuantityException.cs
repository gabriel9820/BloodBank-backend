namespace BloodBank.Core.Exceptions;

public class InvalidStockQuantityException : Exception
{
    public InvalidStockQuantityException()
        : base("A quantidade deve ser maior que zero.") { }
}
