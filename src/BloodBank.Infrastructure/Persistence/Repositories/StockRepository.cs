using BloodBank.Core.Entities;
using BloodBank.Core.Enums;
using BloodBank.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Infrastructure.Persistence.Repositories;

public class StockRepository(
    BloodBankDbContext dbContext) : IStockRepository
{
    private readonly BloodBankDbContext _dbContext = dbContext;

    public async Task AddAsync(Stock stock)
    {
        await _dbContext.Stocks.AddAsync(stock);
    }

    public async Task<IEnumerable<Stock>> GetAllAsync()
    {
        return await _dbContext.Stocks
            .AsNoTracking()
            .OrderBy(s => s.BloodType)
            .ThenBy(s => s.RhFactor)
            .ToListAsync();
    }

    public async Task<Stock?> GetByBloodTypeAsync(BloodType bloodType, RhFactor rhFactor)
    {
        return await _dbContext.Stocks
            .SingleOrDefaultAsync(s => s.BloodType == bloodType && s.RhFactor == rhFactor);
    }
}
