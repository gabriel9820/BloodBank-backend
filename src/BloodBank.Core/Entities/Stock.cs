using BloodBank.Core.Enums;

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
            throw new ArgumentException("A quantidade a ser adicionada deve ser maior que zero.");

        QuantityML += quantityML;
    }

    public void RemoveFromStock(int quantityML)
    {
        if (quantityML <= 0)
            throw new ArgumentException("A quantidade a ser removida deve ser maior que zero.");

        if (quantityML > QuantityML)
            throw new ArgumentException("Estoque insuficiente para remover a quantidade solicitada.");

        QuantityML -= quantityML;
    }
}
