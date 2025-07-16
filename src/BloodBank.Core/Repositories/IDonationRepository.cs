using BloodBank.Core.Entities;
using BloodBank.Core.Models;

namespace BloodBank.Core.Repositories;

public interface IDonationRepository
{
    Task AddAsync(Donation donation);
    void Delete(Donation donation);
    Task<PagedResult<Donation>> GetAllAsync(DonationPagedRequest request);
    Task<DateTime?> GetLastDonationDateByDonorIdAsync(int donorId);
}
