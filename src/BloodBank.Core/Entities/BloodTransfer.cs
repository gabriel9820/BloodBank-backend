using BloodBank.Core.Enums;
using BloodBank.Core.Exceptions;

namespace BloodBank.Core.Entities;

public class BloodTransfer(
    DateTime transferDate,
    BloodType bloodType,
    RhFactor rhFactor,
    int quantityML,
    Hospital hospital
   ) : BaseEntity
{
    public DateTime TransferDate { get; private set; } = ValidateTransferDate(transferDate);
    public BloodType BloodType { get; private set; } = bloodType;
    public RhFactor RhFactor { get; private set; } = rhFactor;
    public int QuantityML { get; private set; } = ValidateQuantity(quantityML);

    /* Foreign Keys */
    public Hospital Hospital { get; private set; } = hospital;
    public int HospitalId { get; private set; } = hospital.Id;

    protected BloodTransfer() : this(default, default, default, default, default!) { }

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
