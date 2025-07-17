using BloodBank.Core.Constants;
using BloodBank.Core.Exceptions;

namespace BloodBank.Core.Entities;

public class Donation : BaseEntity
{
    public DateTime DonationDate { get; private set; }
    public int QuantityML { get; private set; }

    /* Foreign Keys */
    public Donor Donor { get; private set; }
    public int DonorId { get; private set; }

    protected Donation() { }

    public Donation(DateTime donationDate, int quantityML, Donor donor)
    {
        DonationDate = ValidateDonationDate(donationDate);
        QuantityML = ValidateQuantity(quantityML);
        Donor = donor;
        DonorId = donor.Id;
    }

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
