using BloodBank.Core.Entities;
using BloodBank.Core.Models;
using BloodBank.Core.Repositories;
using BloodBank.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Infrastructure.Persistence.Repositories;

public class BloodTransferRepository(
    BloodBankDbContext dbContext) : IBloodTransferRepository
{
    private readonly BloodBankDbContext _dbContext = dbContext;

    public async Task AddAsync(BloodTransfer bloodTransfer)
    {
        await _dbContext.BloodTransfers.AddAsync(bloodTransfer);
    }

    public void Delete(BloodTransfer bloodTransfer)
    {
        _dbContext.BloodTransfers.Remove(bloodTransfer);
    }

    public async Task<PagedResult<BloodTransfer>> GetAllAsync(BloodTransferPagedRequest request)
    {
        var query = _dbContext.BloodTransfers.AsNoTracking();

        query = query
            .Include(bt => bt.Hospital)
            .OrderByDescending(bt => bt.TransferDate);

        return await query.ToPagedResultAsync(request.PageNumber, request.PageSize);
    }

    public async Task<BloodTransfer?> GetByIdAsync(int id)
    {
        return await _dbContext.BloodTransfers.SingleOrDefaultAsync(bt => bt.Id == id);
    }
}
