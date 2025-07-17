namespace BloodBank.Core.Exceptions;

public class DonationQuantityOutOfRangeException : DomainException
{
    public DonationQuantityOutOfRangeException()
        : base("A quantidade de sangue deve estar entre 420 e 470 ml.") { }
}
