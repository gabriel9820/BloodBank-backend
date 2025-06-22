namespace BloodBank.Core.Exceptions;

public class DonorWeightOutOfRangeException : Exception
{
    public DonorWeightOutOfRangeException()
        : base("O doador deve pesar pelo menos 50 kg.") { }
}
