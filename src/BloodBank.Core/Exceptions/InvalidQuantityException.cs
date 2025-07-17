namespace BloodBank.Core.Exceptions;

public class InvalidQuantityException : DomainException
{
    public InvalidQuantityException()
        : base("A quantidade deve ser maior que zero.") { }
}
