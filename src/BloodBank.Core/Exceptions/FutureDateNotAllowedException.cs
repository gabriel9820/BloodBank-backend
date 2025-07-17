namespace BloodBank.Core.Exceptions;

public class FutureDateNotAllowedException : DomainException
{
    public FutureDateNotAllowedException()
        : base("Datas futuras não são permitidas.") { }
}