namespace BloodBank.Core.Exceptions;

public class DonationIntervalOutOfRangeException : DomainException
{
    public DonationIntervalOutOfRangeException()
        : base("O intervalo entre doações deve ser de pelo menos 60 dias para homens e 90 dias para mulheres.") { }
}
