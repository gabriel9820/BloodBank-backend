using BloodBank.Core.Entities;
using BloodBank.Core.Models;
using BloodBank.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Infrastructure.Persistence.Repositories;

public class UserRepository(
    BloodBankDbContext dbContext) : IUserRepository
{
    private readonly BloodBankDbContext _dbContext = dbContext;

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(u => u.Email.Value == email);
    }

    public async Task<IEnumerable<UserNotificationDTO>> GetUsersToNotifyLowStockAsync()
    {
        return await _dbContext.Users
            .AsNoTracking()
            .Where(u => u.IsActive && u.IsLowStockNotificationEnabled)
            .Select(u => new UserNotificationDTO { Id = u.Id, Email = u.Email.Value })
            .ToListAsync();
    }

    public async Task<bool> IsCellPhoneNumberInUseAsync(string cellPhoneNumber)
    {
        return await _dbContext.Users.AnyAsync(u => u.CellPhoneNumber.Value == cellPhoneNumber);
    }

    public async Task<bool> IsEmailInUseAsync(string email)
    {
        return await _dbContext.Users.AnyAsync(u => u.Email.Value == email);
    }
}
