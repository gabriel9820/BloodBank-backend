using BloodBank.Core.DomainEvents;
using BloodBank.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Infrastructure.Persistence;

public class BloodBankDbContext(
    DbContextOptions<BloodBankDbContext> options,
    IDomainEventDispatcher dispatcher) : DbContext(options)
{
    private readonly IDomainEventDispatcher _dispatcher = dispatcher;

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

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var now = DateTime.UtcNow;
        var domainEvents = new List<IDomainEvent>();

        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is BaseEntity baseEntity)
            {
                if (baseEntity.DomainEvents.Count > 0)
                {
                    domainEvents.AddRange(baseEntity.DomainEvents);
                    baseEntity.ClearDomainEvents();
                }

                switch (entry.State)
                {
                    case EntityState.Added:
                        baseEntity.SetCreatedAt(now);
                        baseEntity.SetUpdatedAt(now);
                        break;
                    case EntityState.Modified:
                        baseEntity.SetUpdatedAt(now);
                        break;
                }
            }
        }

        var result = await base.SaveChangesAsync(cancellationToken);

        if (domainEvents.Count > 0)
            await _dispatcher.DispatchAsync(domainEvents);

        return result;
    }
}
