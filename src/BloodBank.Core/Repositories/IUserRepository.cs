using BloodBank.Core.Entities;
using BloodBank.Core.Models;

namespace BloodBank.Core.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User?> GetByEmailAsync(string email);
    Task<IEnumerable<UserNotificationDTO>> GetUsersToNotifyLowStockAsync();
    Task<bool> IsCellPhoneNumberInUseAsync(string cellPhoneNumber);
    Task<bool> IsEmailInUseAsync(string email);
}
