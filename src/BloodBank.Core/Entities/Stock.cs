using BloodBank.Core.DomainEvents;
using BloodBank.Core.Enums;
using BloodBank.Core.Exceptions;

namespace BloodBank.Core.Entities;

public class Stock : BaseEntity
{
    public BloodType BloodType { get; private set; }
    public RhFactor RhFactor { get; private set; }
    public int QuantityML { get; private set; }

    protected Stock() { }

    public Stock(BloodType bloodType, RhFactor rhFactor, int quantityML)
    {
        BloodType = bloodType;
        RhFactor = rhFactor;
        QuantityML = ValidateInitialQuantity(quantityML);
    }

    private static int ValidateInitialQuantity(int quantityML)
    {
        if (quantityML < 0)
            throw new InvalidQuantityException();

        return quantityML;
    }

    public void AddToStock(int quantityML)
    {
        if (quantityML <= 0)
            throw new InvalidQuantityException();

        QuantityML += quantityML;
    }

    public void RemoveFromStock(int quantityML, int? lowStockThresholdML = null)
    {
        if (quantityML <= 0)
            throw new InvalidQuantityException();

        if (quantityML > QuantityML)
            throw new InsufficientStockException();

        QuantityML -= quantityML;

        if (lowStockThresholdML.HasValue && QuantityML < lowStockThresholdML.Value)
        {
            AddDomainEvent(new LowStockDomainEvent(BloodType, RhFactor, QuantityML));
        }
    }
}
