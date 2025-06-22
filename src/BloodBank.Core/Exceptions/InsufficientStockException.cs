namespace BloodBank.Core.Exceptions;

public class InsufficientStockException : Exception
{
    public InsufficientStockException()
        : base("Estoque insuficiente para remover a quantidade solicitada.") { }
}
