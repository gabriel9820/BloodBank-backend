namespace BloodBank.Core.Exceptions;

public class DonorAgeOutOfRangeException : Exception
{
    public DonorAgeOutOfRangeException()
        : base("O doador deve ter pelo menos 18 anos.") { }
}
