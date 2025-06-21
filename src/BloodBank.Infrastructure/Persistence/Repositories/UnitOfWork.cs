using BloodBank.Core.Repositories;

namespace BloodBank.Infrastructure.Persistence.Repositories;

public class UnitOfWork(
    BloodBankDbContext dbContext) : IUnitOfWork
{
    private readonly BloodBankDbContext _dbContext = dbContext;

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}
