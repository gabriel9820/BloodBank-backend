using BloodBank.Core.Constants;

namespace BloodBank.Core.Entities;

public class Donation(
    DateTime donationDate,
    int quantityML,
    Donor donor) : BaseEntity
{
    public DateTime DonationDate { get; private set; } = donationDate;
    public int QuantityML { get; private set; } = ValidateQuantity(quantityML);

    /* Foreign Keys */
    public Donor Donor { get; private set; } = donor;
    public int DonorId { get; private set; } = donor.Id;

    protected Donation() : this(default, default, default!) { }

    private static int ValidateQuantity(int quantityML)
    {
        if (quantityML < DonationRules.MIN_DONATION_QUANTITY_ML || quantityML > DonationRules.MAX_DONATION_QUANTITY_ML)
            throw new ArgumentException(DonationRules.DONATION_QUANTITY_OUT_OF_RANGE_MESSAGE);

        return quantityML;
    }
}
