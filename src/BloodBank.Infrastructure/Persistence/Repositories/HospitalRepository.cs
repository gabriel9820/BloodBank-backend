using BloodBank.Core.Entities;
using BloodBank.Core.Models;
using BloodBank.Core.Repositories;
using BloodBank.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Infrastructure.Persistence.Repositories;

public class HospitalRepository(
    BloodBankDbContext dbContext) : IHospitalRepository
{
    private readonly BloodBankDbContext _dbContext = dbContext;

    public async Task AddAsync(Hospital hospital)
    {
        await _dbContext.Hospitals.AddAsync(hospital);
    }

    public void Delete(Hospital hospital)
    {
        _dbContext.Hospitals.Remove(hospital);
    }

    public async Task<PagedResult<Hospital>> GetAllAsync(HospitalPagedRequest request)
    {
        var query = _dbContext.Hospitals.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(h => EF.Functions.ILike(h.Name, $"%{request.Name}%"));

        query = query.OrderBy(h => h.Name);

        return await query.ToPagedResultAsync(request.PageNumber, request.PageSize);
    }

    public async Task<Hospital?> GetByIdAsync(int id)
    {
        return await _dbContext.Hospitals.SingleOrDefaultAsync(h => h.Id == id);
    }
}
