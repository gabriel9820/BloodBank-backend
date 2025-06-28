namespace BloodBank.Core.Exceptions;

public class FutureDateNotAllowedException : Exception
{
    public FutureDateNotAllowedException()
        : base("Datas futuras não são permitidas.") { }
}