using BloodBank.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Infrastructure.Persistence;

public class BloodBankDbContext(
    DbContextOptions<BloodBankDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Donor> Donors { get; set; }
    public DbSet<Donation> Donations { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Hospital> Hospitals { get; set; }
    public DbSet<BloodTransfer> BloodTransfers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BloodBankDbContext).Assembly);
    }
}
