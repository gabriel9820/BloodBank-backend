using BloodBank.Core.Enums;

namespace BloodBank.Core.Entities;

public class BloodTransfer(
    DateTime transferDate,
    BloodType bloodType,
    RhFactor rhFactor,
    int quantityML,
    Hospital hospital
   ) : BaseEntity
{
    public DateTime TransferDate { get; private set; } = transferDate;
    public BloodType BloodType { get; private set; } = bloodType;
    public RhFactor RhFactor { get; private set; } = rhFactor;
    public int QuantityML { get; private set; } = quantityML;

    /* Foreign Keys */
    public Hospital Hospital { get; private set; } = hospital;
    public int HospitalId { get; private set; } = hospital.Id;

    protected BloodTransfer() : this(default, default, default, default, default!) { }
}
