using BloodBank.Core.Enums;
using BloodBank.Core.Exceptions;

namespace BloodBank.Core.Entities;

public class BloodTransfer : BaseEntity
{
    public DateTime TransferDate { get; private set; }
    public BloodType BloodType { get; private set; }
    public RhFactor RhFactor { get; private set; }
    public int QuantityML { get; private set; }

    /* Foreign Keys */
    public Hospital Hospital { get; private set; }
    public int HospitalId { get; private set; }

    protected BloodTransfer() { }

    public BloodTransfer(
        DateTime transferDate,
        BloodType bloodType,
        RhFactor rhFactor,
        int quantityML,
        Hospital hospital)
    {
        TransferDate = ValidateTransferDate(transferDate);
        BloodType = bloodType;
        RhFactor = rhFactor;
        QuantityML = ValidateQuantity(quantityML);
        Hospital = hospital;
        HospitalId = hospital.Id;
    }

    private static DateTime ValidateTransferDate(DateTime transferDate)
    {
        if (transferDate > DateTime.UtcNow)
            throw new FutureDateNotAllowedException();

        return transferDate;
    }

    private static int ValidateQuantity(int quantityML)
    {
        if (quantityML <= 0)
            throw new InvalidQuantityException();

        return quantityML;
    }
}
