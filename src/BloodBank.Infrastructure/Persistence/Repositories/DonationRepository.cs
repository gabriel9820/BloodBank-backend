using BloodBank.Core.Entities;
using BloodBank.Core.Models;
using BloodBank.Core.Repositories;
using BloodBank.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Infrastructure.Persistence.Repositories;

public class DonationRepository(
    BloodBankDbContext dbContext) : IDonationRepository
{
    private readonly BloodBankDbContext _dbContext = dbContext;

    public async Task AddAsync(Donation donation)
    {
        await _dbContext.Donations.AddAsync(donation);
    }

    public void Delete(Donation donation)
    {
        _dbContext.Donations.Remove(donation);
    }

    public async Task<PagedResult<Donation>> GetAllAsync(DonationPagedRequest request)
    {
        var query = _dbContext.Donations.AsNoTracking();

        query = query.OrderByDescending(x => x.DonationDate);

        return await query.ToPagedResultAsync(request.PageNumber, request.PageSize);
    }

    public async Task<Donation?> GetByIdAsync(int id)
    {
        return await _dbContext.Donations
            .Include(d => d.Donor)
            .SingleOrDefaultAsync(d => d.Id == id);
    }

    public async Task<DateTime?> GetLastDonationDateByDonorIdAsync(int donorId)
    {
        return await _dbContext.Donations
            .AsNoTracking()
            .Where(d => d.DonorId == donorId)
            .OrderByDescending(d => d.DonationDate)
            .Select(d => d.DonationDate)
            .FirstOrDefaultAsync();
    }
}
