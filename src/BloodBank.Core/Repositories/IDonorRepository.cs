using BloodBank.Core.Entities;
using BloodBank.Core.Models;

namespace BloodBank.Core.Repositories;

public interface IDonorRepository
{
    Task AddAsync(Donor donor);
    void DeleteAsync(Donor donor);
    Task<PagedResult<Donor>> GetAllAsync(DonorPagedRequest request);
    Task<Donor?> GetByIdAsync(int id);
    Task<bool> IsCellPhoneNumberInUseAsync(string cellPhoneNumber);
    Task<bool> IsEmailInUseAsync(string email);
}
