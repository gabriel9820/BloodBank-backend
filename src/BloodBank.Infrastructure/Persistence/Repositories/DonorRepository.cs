using BloodBank.Core.Entities;
using BloodBank.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Infrastructure.Persistence.Repositories;

public class DonorRepository(
    BloodBankDbContext dbContext) : IDonorRepository
{
    private readonly BloodBankDbContext _dbContext = dbContext;

    public async Task AddAsync(Donor donor)
    {
        await _dbContext.Donors.AddAsync(donor);
    }

    public void DeleteAsync(Donor donor)
    {
        _dbContext.Donors.Remove(donor);
    }

    public async Task<IEnumerable<Donor>> GetAllAsync()
    {
        return await _dbContext.Donors.ToListAsync();
    }

    public async Task<Donor?> GetByIdAsync(int id)
    {
        return await _dbContext.Donors.SingleOrDefaultAsync(d => d.Id == id);
    }

    public async Task<bool> IsCellPhoneNumberInUseAsync(string cellPhoneNumber)
    {
        return await _dbContext.Donors.AnyAsync(d => d.CellPhoneNumber.Value == cellPhoneNumber);
    }

    public async Task<bool> IsEmailInUseAsync(string email)
    {
        return await _dbContext.Donors.AnyAsync(d => d.Email.Value == email);
    }
}
