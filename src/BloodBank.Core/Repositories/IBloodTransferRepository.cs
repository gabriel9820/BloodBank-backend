using BloodBank.Core.Entities;
using BloodBank.Core.Models;

namespace BloodBank.Core.Repositories;

public interface IBloodTransferRepository
{
    Task AddAsync(BloodTransfer bloodTransfer);
    void Delete(BloodTransfer bloodTransfer);
    Task<PagedResult<BloodTransfer>> GetAllAsync(BloodTransferPagedRequest request);
    Task<BloodTransfer?> GetByIdAsync(int id);
}
