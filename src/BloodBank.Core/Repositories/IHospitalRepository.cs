using BloodBank.Core.Entities;
using BloodBank.Core.Models;

namespace BloodBank.Core.Repositories;

public interface IHospitalRepository
{
    Task AddAsync(Hospital hospital);
    void Delete(Hospital hospital);
    Task<PagedResult<Hospital>> GetAllAsync(HospitalPagedRequest request);
    Task<Hospital?> GetByIdAsync(int id);
}
