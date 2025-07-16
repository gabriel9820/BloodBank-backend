using BloodBank.Core.Entities;
using BloodBank.Core.Enums;

namespace BloodBank.Core.Repositories;

public interface IStockRepository
{
    Task AddAsync(Stock stock);
    Task<Stock?> GetByBloodTypeAsync(BloodType bloodType, RhFactor rhFactor);
}
