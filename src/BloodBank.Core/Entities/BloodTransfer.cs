using BloodBank.Core.Enums;

namespace BloodBank.Core.Entities;

public class BloodTransfer(
    DateTime sentDate,
    BloodType bloodType,
    RhFactor rhFactor,
    int quantityML,
    Hospital hospital
   ) : BaseEntity
{
    public DateTime SentDate { get; private set; } = sentDate;
    public BloodType BloodType { get; private set; } = bloodType;
    public RhFactor RhFactor { get; private set; } = rhFactor;
    public int QuantityML { get; private set; } = quantityML;

    /* Foreign Keys */
    public Hospital Hospital { get; private set; } = hospital;
    public int HospitalId { get; private set; } = hospital.Id;
}
