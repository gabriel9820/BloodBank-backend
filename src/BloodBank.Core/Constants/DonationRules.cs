namespace BloodBank.Core.Constants;

public static class DonationRules
{
    public const int MIN_DONATION_QUANTITY_ML = 420;
    public const int MAX_DONATION_QUANTITY_ML = 470;
    public const int MIN_DONOR_AGE = 18;
    public const int MIN_DONOR_WEIGHT_KG = 50;
    public const int MIN_INTERVAL_BETWEEN_DONATIONS_DAYS_FEMALE = 90;
    public const int MIN_INTERVAL_BETWEEN_DONATIONS_DAYS_MALE = 60;

    public const string DONATION_QUANTITY_OUT_OF_RANGE_MESSAGE = "A quantidade de sangue deve estar entre 420 e 470 ml.";
    public const string DONOR_AGE_OUT_OF_RANGE_MESSAGE = "O doador deve ter pelo menos 18 anos.";
    public const string DONOR_WEIGHT_OUT_OF_RANGE_MESSAGE = "O doador deve pesar pelo menos 50 kg.";
    public const string DONATION_INTERVAL_OUT_OF_RANGE_MESSAGE = "O intervalo entre doações deve ser de pelo menos 60 dias para homens e 90 dias para mulheres.";
}
