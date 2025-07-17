namespace BloodBank.Core.Exceptions;

public class InsufficientStockException : DomainException
{
    public InsufficientStockException()
        : base("Estoque insuficiente para remover a quantidade solicitada.") { }
}
