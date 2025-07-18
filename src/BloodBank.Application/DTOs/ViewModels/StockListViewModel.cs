using BloodBank.Core.Entities;
using BloodBank.Core.Enums;

namespace BloodBank.Application.DTOs.ViewModels;

public class StockListViewModel(
    BloodType bloodType,
    RhFactor rhFactor,
    int quantityML)
{
    public BloodType BloodType { get; private set; } = bloodType;
    public RhFactor RhFactor { get; private set; } = rhFactor;
    public int QuantityML { get; private set; } = quantityML;
}

public static partial class StockExtensions
{
    public static StockListViewModel ToListViewModel(this Stock stock)
    {
        return new StockListViewModel(stock.BloodType, stock.RhFactor, stock.QuantityML);
    }
}
