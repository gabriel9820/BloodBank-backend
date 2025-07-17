namespace BloodBank.Core.Exceptions;

public class DonorAgeOutOfRangeException : DomainException
{
    public DonorAgeOutOfRangeException()
        : base("O doador deve ter pelo menos 18 anos.") { }
}
