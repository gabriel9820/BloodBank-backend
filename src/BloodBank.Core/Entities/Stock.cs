using BloodBank.Core.Enums;
using BloodBank.Core.Exceptions;

namespace BloodBank.Core.Entities;

public class Stock(
    BloodType bloodType,
    RhFactor rhFactor,
    int quantityML) : BaseEntity
{
    public BloodType BloodType { get; private set; } = bloodType;
    public RhFactor RhFactor { get; private set; } = rhFactor;
    public int QuantityML { get; private set; } = quantityML;

    protected Stock() : this(default, default, default) { }

    public void AddToStock(int quantityML)
    {
        if (quantityML <= 0)
            throw new InvalidQuantityException();

        QuantityML += quantityML;
    }

    public void RemoveFromStock(int quantityML)
    {
        if (quantityML <= 0)
            throw new InvalidQuantityException();

        if (quantityML > QuantityML)
            throw new InsufficientStockException();

        QuantityML -= quantityML;
    }
}
