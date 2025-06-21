using BloodBank.Core.Entities;

namespace BloodBank.Core.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user);
}
