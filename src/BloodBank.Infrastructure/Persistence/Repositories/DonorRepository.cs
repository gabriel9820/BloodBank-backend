using BloodBank.Core.Entities;
using BloodBank.Core.Models;
using BloodBank.Core.Repositories;
using BloodBank.Infrastructure.Persistence.Extensions;
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

    public async Task<PagedResult<Donor>> GetAllAsync(DonorPagedRequest request)
    {
        var query = _dbContext.Donors.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.FullName))
            query = query.Where(d => EF.Functions.ILike(d.FullName, $"%{request.FullName}%"));

        query = query.OrderBy(d => d.FullName);

        return await query.ToPagedResultAsync(request.PageNumber, request.PageSize);
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
