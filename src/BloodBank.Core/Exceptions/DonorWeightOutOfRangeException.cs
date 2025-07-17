namespace BloodBank.Core.Exceptions;

public class DonorWeightOutOfRangeException : DomainException
{
    public DonorWeightOutOfRangeException()
        : base("O doador deve pesar pelo menos 50 kg.") { }
}
