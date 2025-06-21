namespace BloodBank.Core.Repositories;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}
