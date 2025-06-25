using BloodBank.Core.Entities;

namespace BloodBank.Core.Repositories;

public interface IDonorRepository
{
    Task AddAsync(Donor donor);
    void DeleteAsync(Donor donor);
    Task<IEnumerable<Donor>> GetAllAsync();
    Task<Donor?> GetByIdAsync(int id);
    Task<bool> IsCellPhoneNumberInUseAsync(string cellPhoneNumber);
    Task<bool> IsEmailInUseAsync(string email);
}
