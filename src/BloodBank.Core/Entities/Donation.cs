using BloodBank.Core.Constants;
using BloodBank.Core.Exceptions;

namespace BloodBank.Core.Entities;

public class Donation(
    DateTime donationDate,
    int quantityML,
    Donor donor) : BaseEntity
{
    public DateTime DonationDate { get; private set; } = ValidateDonationDate(donationDate);
    public int QuantityML { get; private set; } = ValidateQuantity(quantityML);

    /* Foreign Keys */
    public Donor Donor { get; private set; } = donor;
    public int DonorId { get; private set; } = donor.Id;

    protected Donation() : this(default, default, default!) { }

    private static DateTime ValidateDonationDate(DateTime donationDate)
    {
        if (donationDate > DateTime.UtcNow)
            throw new FutureDateNotAllowedException();

        return donationDate;
    }

    private static int ValidateQuantity(int quantityML)
    {
        if (quantityML < DonationRules.MIN_DONATION_QUANTITY_ML || quantityML > DonationRules.MAX_DONATION_QUANTITY_ML)
            throw new DonationQuantityOutOfRangeException();

        return quantityML;
    }
}
