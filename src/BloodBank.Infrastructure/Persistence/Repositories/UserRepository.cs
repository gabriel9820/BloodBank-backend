using BloodBank.Core.Entities;
using BloodBank.Core.Repositories;

namespace BloodBank.Infrastructure.Persistence.Repositories;

public class UserRepository(
    BloodBankDbContext dbContext) : IUserRepository
{
    private readonly BloodBankDbContext _dbContext = dbContext;

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }
}
