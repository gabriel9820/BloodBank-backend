using BloodBank.Core.Entities;

namespace BloodBank.Core.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<bool> IsCellPhoneNumberInUseAsync(string cellPhoneNumber);
    Task<bool> IsEmailInUseAsync(string email);
}
