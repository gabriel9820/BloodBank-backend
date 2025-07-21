using BloodBank.Core.Enums;

namespace BloodBank.Core.DomainEvents;

public class LowStockDomainEvent(
    BloodType bloodType,
    RhFactor rhFactor,
    int quantityML) : IDomainEvent
{
    public BloodType BloodType { get; private set; } = bloodType;
    public RhFactor RhFactor { get; private set; } = rhFactor;
    public int QuantityML { get; private set; } = quantityML;
}
